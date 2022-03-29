using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Excel = Microsoft.Office.Interop.Excel;
using System.Net;
using System.Net.Mail;
using System.Threading;
using Microsoft.Office.Interop.Excel;
using System.IO;
using ExcelDataReader;
using System.Data;
using System.Diagnostics;



namespace TriviaProject
{
    /// <summary>
    /// Interaction logic for dataUser.xaml
    /// </summary>
    public partial class dataUser : System.Windows.Window
    {
        //========================================================================================================
        private string FileName = string.Empty;

        private Process openTermsProc=new Process();
        private System.Windows.Media.MediaPlayer clickSound1 = new System.Windows.Media.MediaPlayer();

        private const string InstrucitonsPath = "files/terms.pdf";
        //========================================================================================================

        public dataUser()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);

            clickSound1.Open(new Uri("sounds/s16.wav", UriKind.Relative));

            CheckR();



        }


        //***********************************************************************************************************
        private void CheckR()
        {
            var x = SystemParameters.PrimaryScreenWidth;
            var y = SystemParameters.PrimaryScreenHeight;

            if (x != 1920 || y != 1080)
            {
                d1.Height = 40;
                d2.Height = 40;
                d3.Height = 40;
                d4.Height = 40;
            }

        }

        //***********************************************************************************************************
        private void InstructionsLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {



            if (!File.Exists(InstrucitonsPath))
            {
                MessageBox.Show("לא נמצא התקנון המלא");
            }
            else
            {
                try
                {
                    openTermsProc = Process.Start(System.IO.Path.Combine(Directory.GetCurrentDirectory(), InstrucitonsPath));

                   
                   
                }
                catch (Exception ex)
                {
                    ExceptionDetails.WriteException(ex);
                    //למקרה שלא מצליח לפתוח את הקובץ
                    MessageBox.Show("אנו מתנצלים אך לא ניתן לפתוח את התקנון המלא"+ex.ToString());

                }

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
        private void CheckData()
        {
            if (d1.Text != "" && d1.Text != "" && d3.Text != "" && d3.Text != "" && checkbox.IsChecked == true)
            {
                NextScreen();
            }
            else
            {
                if (d1.Text == "")
                {
                    d1.BorderBrush = Brushes.Red;
                }
                if (d2.Text == "")
                {
                    d2.BorderBrush = Brushes.Red;
                }
                if (d3.Text == "")
                {
                    d3.BorderBrush = Brushes.Red;
                }
                if (d4.Text == "")
                {
                    d4.BorderBrush = Brushes.Red;
                }
                if (checkbox.IsChecked == false)
                {
                    checkbox.BorderBrush = Brushes.Red;
                }
            }
        }
        //***********************************************************************************************************
       
        private void btnClick(object sender, RoutedEventArgs e)
        {

            clickSound1.Play();

            clickSound1.MediaEnded += new EventHandler(Media_EndedClickSound1);
            CheckData();

        }
        //***********************************************************************************************************
        private void NextScreen()
        {
          
            //סגירת דף התקנון
            //???

            Ans.PrivateName = d1.Text;
            Ans.City = d2.Text;
            Ans.PhoneNum = d3.Text;
            Ans.Email = d4.Text;
            Ans.sentResult = "false";

            LoadSound.player3.Stop();


            InstructionsLabel.Visibility = Visibility.Hidden;
            textChecbox.Visibility = Visibility.Hidden;
            checkbox.Visibility = Visibility.Hidden;
            InstructionsLabel.IsEnabled = false;
            //הדרך הטובה למעבר בין מסכים
            PurchaseWindow w = new PurchaseWindow();
            //System.Windows.Application.Current.MainWindow = purchaseWindow;


            //_mainFrame.Navigate(purchaseWindow);


            //w.InitializeComponent();

           
            w.Show();
            this.Close();


        }

        //***********************************************************************************************************
        private void ReleaseObject(_Worksheet oSheet)
        {
            throw new NotImplementedException();
        }

        //***********************************************************************************************************




        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            checkbox.IsChecked = true;

        }
        //***********************************************************************************************************
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            //(this.Parent as Window).WindowState = WindowState.Maximized;

        }
        //***********************************************************************************************************
        private void enterM(object sender, MouseEventArgs e)
        {
           // img.ImageSource = change.Source;
        }
        //***********************************************************************************************************
        private void leaveM(object sender, MouseEventArgs e)
        {
           // img.ImageSource = normal.Source;
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
            LoadSound.player3.Stop();
            // LoadSound.player15.Stop();
            //LoadSound.player16.Play();

            clickSound1.Play();

            clickSound1.MediaEnded += new EventHandler(Media_EndedClickSound1);

            //הדרך הטובה למעבר בין מסכים
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

        //private void Window_KeyDown(object sender, KeyEventArgs e)
        //{

        //}

    }
}



