using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using Microsoft.Win32;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;




using TriviaProject.Properties;

namespace TriviaProject
{
    /// <summary>
    /// Interaction logic for PurchaseWindow.xaml
    /// </summary>
    public partial class PurchaseWindow : Window
    {


        //========================================================================================================

        private const string programPurchasedPath = @"HKEY_CURRENT_USER\Software\Trivia\";
        //private const string programPurchasedValue = "ProgramPurchased";
        private const string paymentLink = "https://secure.cardcom.solutions/e/xOJZ";
        private const string secretKey = "trivia";
        private const string codesFile = "files/codes.xlsx";
        //private string codeGame;
        private string code;
        private string codeStart;
        private string codeEnd;
        private RegistryKey regKey;
        public bool isCodeCreate;

        private Excel.Application excelApp;
        private Excel.Workbook codesWorkbook;
        private Excel.Worksheet worksheet;





        public object JsonSerializer { get; private set; }

        private const int codesCount = 80985;
        private System.Windows.Media.MediaPlayer clickSound1 = new System.Windows.Media.MediaPlayer();


        public object JsonConvert { get; private set; }



        //========================================================================================================



        public PurchaseWindow()
        {


            InitializeComponent();


            var currentGame = programPurchasedPath + Ans.currentCodeGame + '\\';

            regKey = Registry.CurrentUser.OpenSubKey(currentGame, true);
            //object val = regKey.GetValue("code");
            if (regKey != null)
                isCodeCreate = true;

            //regKey.SetValue("codeGame", codeGame);
            else
            {
                regKey = Registry.CurrentUser.CreateSubKey(currentGame, true);
               
            }


            clickSound1.Open(new Uri("sounds/s16.wav", UriKind.Relative));

            createCode();
            
        }
        //***********************************************************************************************************

        private void createCode()

        {
            if (isCodeCreate)
            {
                code = Ans.Code;
                codeStart = code.Substring(0, 5);
                codeEnd = code.Substring(5, 5);



                codeLabel.Content = codeStart;

                codeTextbox.Focus();
            }
            else//create code
            {
                try
                {
                    this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
                    excelApp = new Excel.Application();
                    codesWorkbook = excelApp.Workbooks.Open(Path.Combine(Directory.GetCurrentDirectory(), codesFile), ReadOnly: true, Password: "5516");
                    worksheet = codesWorkbook.ActiveSheet;
                    excelApp.Visible = false;
                    excelApp.DisplayAlerts = false;

                    Random random = new Random();

                    int codeIndex = 1 + random.Next(0, codesCount);


                    codeStart = GetCellValue(worksheet, 1, codeIndex);
                    codeEnd = GetCellValue(worksheet, 2, codeIndex);

                    code = codeStart + codeEnd;
                    Ans.Code = code;

                    codeLabel.Content = codeStart;

                    codeTextbox.Focus();
                    //סגירת הקובץ
                    //excelApp.Quit();
                    codesWorkbook.Close(SaveChanges: false);
                }
                catch (Exception ex)
                {
                    ExceptionDetails.WriteException(ex);

                    //throw;

                    MessageBox.Show(" אנו מתנצלים, אך לא ניתן לטעון את מסך התשלום (1), אנא פנה לתמיכה התכנית בטלפון :02-5344559" + ex.ToString());
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                    Application.Current.Shutdown();
                }

                SetDataPlayer();
            }

        }
        //***********************************************************************************************************

        private void Media_Ended(object sender, EventArgs e)
    {
        //LoadSound.player12.Position = TimeSpan.Zero;
       // LoadSound.player12.Play();
    }

        //***********************************************************************************************************

        private void SetDataPlayer()

        {
            regKey.SetValue("codeGame", Ans.currentCodeGame);
            regKey.SetValue("code", Ans.Code);
            regKey.SetValue("PrivateName", Ans.PrivateName);
            regKey.SetValue("city", Ans.City);

            regKey.SetValue("PhoneNum", Ans.PhoneNum);
            regKey.SetValue("Email", Ans.Email);
            regKey.SetValue("sendResult", Ans.sentResult);


        }


        //***********************************************************************************************************


        private void ActivateProgram()
        {
          
            regKey.SetValue("Paid", "true");
            Ans.isPaid = "true";
           


            regKey.Close();
        }

        //***********************************************************************************************************



        private string GetCellValue(Excel.Worksheet worksheet, int col, int row)
        {
            if (worksheet.Cells[row, col].Value == null)
            {
                return "";
            }

            return worksheet.Cells[row, col].Value.ToString();
        }
        //***********************************************************************************************************
        private void StartLink()
        {
            Process.Start(paymentLink /*+ codeStart)*/);
        }
        //***********************************************************************************************************
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //LoadSound.player16.Play();

            clickSound1.Play();

            clickSound1.MediaEnded += new EventHandler(Media_EndedClickSound1);
            StartLink();
            codeTextbox.Focus();
        }

        //***********************************************************************************************************

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (codeTextbox.Text.Length != codeTextbox.MaxLength)
            {
                okButton.IsEnabled = false;
            }
            else
            {
                okButton.IsEnabled = true;
                //okButton.Focus();
            }
        }
        //***********************************************************************************************************
        private string Md5(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder sBuilder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                return sBuilder.ToString().Substring(0, 5);
            }
        }

        //***********************************************************************************************************

        private void codeTextbox_KeyDown(object sender, KeyEventArgs e)
        {

        }
        //***********************************************************************************************************

        private void okButton_Click(object sender, RoutedEventArgs e)
        {

         


            //LoadSound.player16.Play();



            clickSound1.Play();

            clickSound1.MediaEnded += new EventHandler(Media_EndedClickSound1);

            string userCode = codeTextbox.Text;
            //if (userCode.Equals(Md5(code + secretKey)))
            //אם קוד ההמשך שהוקש לא תקין-נציג הודעת שגיאה
            if(!userCode.Equals(codeEnd))
            {
                codeTextbox.Text = "";

                //הדרך הטובה למעבר בין מסכים
                ErrorMessageCode w = new ErrorMessageCode();

                //  System.Windows.Application.Current.MainWindow = w;
                w.InitializeComponent();
                w.ShowDialog();

               

            }
            else//אם הכניס את הקוד הנכון
            {
                // LoadSound.player12.Stop();

               
                ActivateProgram();

                GameTypeSelectionWindow w = new GameTypeSelectionWindow();

               // w.InitializeComponent();

               
                w.Show();
                this.Close();


            }
        }


        //***********************************************************************************************************



        private void HandleEsc(object sender, KeyEventArgs e)
        {


            if (e.Key == Key.Escape)
            {

                this.WindowState = WindowState.Minimized;
            }

        }

        //***********************************************************************************************************

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

           // (this.Parent as Window).WindowState = WindowState.Maximized;

        }


        //***********************************************************************************************************


        private void leaveM2(object sender, MouseEventArgs e)
        {
            imgButton2.ImageSource = normal.Source;
        }
        //***********************************************************************************************************
        private void enterM2(object sender, MouseEventArgs e)
        {

            imgButton2.ImageSource = change.Source;

        }
        //***********************************************************************************************************



        private void leaveX(object sender, MouseEventArgs e)
        {
            XButton.ImageSource = normalX.Source;

        }
        //***********************************************************************************************************
        private void enterX(object sender, MouseEventArgs e)
        {
            XButton.ImageSource = changeX.Source;

        }
        //***********************************************************************************************************
        private void closePageClick(object sender, RoutedEventArgs e)
        {
           // LoadSound.player12.Stop();
            LoadSound.player16.Play();


            ExitScreen w = new ExitScreen();

            w.InitializeComponent();
            w.ShowDialog();
        }
        //***********************************************************************************************************
        private void Media_EndedClickSound1(object sender, EventArgs e)

        {

            //LoadSound.player16.Position = TimeSpan.Zero;
            //LoadSound.player16.Play();
            clickSound1.Stop();
        }


        //***********************************************************************************************************



    }


}

