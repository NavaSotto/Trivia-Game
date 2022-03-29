using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;




namespace TriviaProject
{
    /// <summary>
    /// Interaction logic for AskScreen.xaml
    /// </summary>
    public partial class InstructionsScreen : Window
    {
        //========================================================================================================

        private const string programPurchasedPath = @"HKEY_CURRENT_USER\Software\Trivia\";
        //private const string programPurchasedValue = "ProgramPurchased";
        private string codeGame;
        private RegistryKey regKey;
        private bool isReg = false;
        private string currentGame;



        private System.Windows.Media.MediaPlayer clickSound1 = new System.Windows.Media.MediaPlayer();




        public KeyEventHandler Closing { get; private set; }

        //========================================================================================================


        public InstructionsScreen()
        {
           InitializeComponent();
            CheckR();
            codeGame = System.IO.File.ReadAllText("files/codeGame.txt");
            currentGame = programPurchasedPath + codeGame + '\\';
            regKey = Registry.CurrentUser.OpenSubKey(currentGame, true);
           
            if (regKey == null) 
            {

                isReg = false;
            }
            else 
            {
                isReg = true;
                GetDataPlayers();
            }

          

            var w = SystemParameters.VirtualScreenWidth;
            var h = SystemParameters.VirtualScreenHeight;

            //this.WindowWidth = w;
            //.WindowHeight = h;
            MainGrid.Width = w;
            MainGrid.Height = h;


            clickSound1.Open(new Uri("sounds/s16.wav", UriKind.Relative));

            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            //LoadSound.player15.Play();
            //LoadSound.player15.MediaEnded += new EventHandler(Media_Ended);

            codeGame = System.IO.File.ReadAllText("files/codeGame.txt");
            Ans.currentCodeGame = codeGame;





        }
        //***********************************************************************************************************

        private void CheckR()
        {
            double width = System.Windows.SystemParameters.PrimaryScreenWidth;
            double height = System.Windows.SystemParameters.PrimaryScreenHeight;
            if (width != 1920 || height != 1080)
            {
                Canvas.SetTop(ScreenZeroContinueButton, -2);
                Canvas.SetLeft(ScreenZeroContinueButton, -71);
                


            }

        }
        //***********************************************************************************************************

        private void GetDataPlayers()
        {
            try
            {
                // isReg = true;
                Ans.currentCodeGame = regKey.GetValue("codeGame").ToString();
                Ans.Code = regKey.GetValue("code").ToString();
                Ans.PrivateName = regKey.GetValue("PrivateName").ToString();
                Ans.City = regKey.GetValue("city").ToString();

                Ans.PhoneNum = regKey.GetValue("PhoneNum").ToString();
                Ans.Email = regKey.GetValue("Email").ToString();
                Ans.sentResult = regKey.GetValue("sendResult").ToString();
                //Ans.isPaid = regKey.GetValue("Paid").ToString();
                object p = regKey.GetValue("Paid");
                if (p == null)
                    Ans.isPaid = "false";
                else
                {
                    Ans.isPaid = "true";
                }
            }
            catch//אם קיים המפתח אך לא מצליח לגשת לנתונים סימן שהחלון נסגר עוד לפני שהתוכנית הספיקה לשמור את נתוני המשתמש ולייצר קוד 
            {
                isReg = false;
            }
          



        }

        //***********************************************************************************************************

        private void Media_Ended(object sender, EventArgs e)
            {
                //LoadSound.player15.Position = TimeSpan.Zero;
                //LoadSound.player15.Play();
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

        private void MediaFailedFunc(object sender, ExceptionRoutedEventArgs e)
            {
                Console.WriteLine("Failed:" + ((MediaElement)sender).Source);
            }
        //***********************************************************************************************************
        private void Window_KeyDown(object sender, KeyEventArgs e)
            {

            }
        //***********************************************************************************************************
        private void Page_Loaded(object sender, RoutedEventArgs e)
            {
                //(this.Parent as Window).WindowState = WindowState.Maximized;
            }

        //***********************************************************************************************************
        private void Button_click(object sender, RoutedEventArgs e)
            {
                // LoadSound.player16.Play();


                clickSound1.Play();

                clickSound1.MediaEnded += new EventHandler(Media_EndedClickSound1);

                // הכניס נתונים ושילם
                if (isProgramPurchased())
                {

               
                    //הדרך הטובה למעבר בין מסכים
                    GameTypeSelectionWindow w = new GameTypeSelectionWindow();
                   
                    
                    this.KeyDown -= HandleEsc;
               // w.InitializeComponent();

                
                w.Show();
                this.Close();




            }
            else if(isReg)//הכניס נתונים ולא שילם
                {
                PurchaseWindow w = new PurchaseWindow();
                //System.Windows.Application.Current.MainWindow = purchaseWindow;

                //_mainFrame.Navigate(purchaseWindow);



               // w.InitializeComponent();

                
                w.Show();
                this.Close();


            }
            else//לא הכניס נתנים ולא שילם
            {
                //הדרך הטובה למעבר בין מסכים
                dataUser w = new dataUser();

                this.KeyDown -= Window_KeyDown;

               // w.InitializeComponent();

               
                w.Show();
                this.Close();
            }
            }


        //***********************************************************************************************************
        private bool isProgramPurchased()
        {


            //object codeVal = regKey.GetValue("codeGame");


            //נבדוק ששילם על השעשועון 



            if (isReg == false)
                return false;
            else
            {
                //object paidVal = regKey.GetValue("Paid");
                if (codeGame == Ans.currentCodeGame && Ans.isPaid.ToString() == "true")
                {
                    //Ans.isPaid = "true";
                    return true;
                }
                    
                else
                    return false;
            }
        }



        //***********************************************************************************************************





        private void leaveM(object sender, MouseEventArgs e)
            {



                imgButton.ImageSource = normal.Source;
            }
        //***********************************************************************************************************
        private void enterM(object sender, MouseEventArgs e)
            {

                imgButton.ImageSource = change.Source;

            }
        //***********************************************************************************************************
        private void exitButtonClick(object sender, RoutedEventArgs e)
            {

                clickSound1.Play();

                clickSound1.MediaEnded += new EventHandler(Media_EndedClickSound1);
                //הדרך הטובה למעבר בין מסכים
                ExitScreen w = new ExitScreen();
                w.InitializeComponent();

                w.ShowDialog();

              

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
        private void _mainFrame_Navigated(object sender, NavigationEventArgs e)
            {

            }
        //***********************************************************************************************************
        private void _mainFrame_NavigationProgress(object sender, NavigationProgressEventArgs e)
            {

            }

        //***********************************************************************************************************

        private void Media_EndedClickSound1(object sender, EventArgs e)

            {

               
                clickSound1.Stop();
            }

        //***********************************************************************************************************



    }
}

