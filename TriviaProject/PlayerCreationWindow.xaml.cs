using System;
using System.Collections.Generic;
using System.Linq;
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
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
using System.Diagnostics;
using System.Threading;
using Microsoft.Office.Interop.Excel;

namespace TriviaProject
{
    /// <summary>
    /// Interaction logic for PlayerCreationWindow.xaml
    /// </summary>
    public partial class PlayerCreationWindow : System.Windows.Window
    {

        //========================================================================================================
        public string quizFilePath;

        private List<string> players;
        private const int MINIMUM_SPLASH_TIME = 3000; // Miliseconds  
        private GameWindow w = new GameWindow();



        private List<KeyValuePair<KeyValuePair<string, int>, List<int>>> ScoreList = new List<KeyValuePair<KeyValuePair<string, int>, List<int>>>();

        private System.Windows.Media.MediaPlayer clickSound1 = new System.Windows.Media.MediaPlayer();

        //========================================================================================================
        public PlayerCreationWindow()
        {
            InitializeComponent();


            //LoadSound.Load();
            //LoadSound.player6.Play();


            players = new List<string>();
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            clickSound1.Open(new Uri("sounds/s16.wav", UriKind.Relative));


            playerNameTextbox.Focus();

            



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
        public void StartGame()
        {




            var s = new Dictionary<string, int>();

            foreach (var p in players)
            {

                s.Add(p, 0);


            }
            w.scores = s;
            w.quizFilePath = this.quizFilePath;

            this.KeyDown -= Window_KeyDown;
            this.KeyDown += w.KeyDownEvent;
            //w.InitializeComponent();

            w.Show();
            this.Close();


        }
        //***********************************************************************************************************



        private void AddPlayer(string playerName)
        {
            if (!players.Contains(playerName))
            {
                try
                {
                    players.Add(playerName);

                }
                catch
                {
                    //MessageBox.Show("לא ניתן להוסיף שחקן");
                }

                KeyValuePair<KeyValuePair<string, int>, List<int>> l = new KeyValuePair<KeyValuePair<string, int>, List<int>>(new KeyValuePair<string, int>(playerName, 0), new List<int>());
                ScoreList.Add(l);


                playersListView.Items.Add(playerName);

                okButton.IsEnabled = true;
            }
            else
            {
                playerNameTextbox.Text = "";
                playerNameTextbox.BorderBrush = Brushes.Red;
            }

        }
        //***********************************************************************************************************
        private void playerNameTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            //הוסיף שם של שחקן ולחץ אנטר
            if (e.Key == Key.Enter && playerNameTextbox.Text.Length > 0)
            {
                AddPlayer(playerNameTextbox.Text);
                playerNameTextbox.Text = "";
            }
            //לחץ אנטר אבל לא אחרי שאוסיף שם של שחקן -כלומר רצה להתקדם למסך הבא
            else if (e.Key == Key.Enter && players.Count > 0)
            {
                //okButton.Focus();  //ממסגר את הכפתור
            }
        }
        //***********************************************************************************************************
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_mainFrame.Content == null || _mainFrame.Content.GetType().Name != "GameWindow")
            {
                // LoadSound.player6.Stop();


                LoadSound.player5.Play();

                LoadSound.player5.MediaEnded += new EventHandler(Media_Ended);



            }

        }
        //***********************************************************************************************************
        private void Media_Ended(object sender, EventArgs e)

        {

            //LoadSound.player16.Position = TimeSpan.Zero;
            //LoadSound.player16.Play();
            clickSound1.Stop();
            StartGame();

            // w.Show();
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
            // LoadSound.player16.Play();

            LoadSound.player5.Stop();


            clickSound1.Play();

            clickSound1.MediaEnded += new EventHandler(Media_EndedClickSound1);
            // LoadSound.player6.Stop();


            ExitScreen w = new ExitScreen();

            w.InitializeComponent();
            w.ShowDialog();
        }

        //***********************************************************************************************************


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            //(this.Parent as Window).WindowState = WindowState.Maximized;

        }
        //***********************************************************************************************************

        private void Media_EndedClickSound1(object sender, EventArgs e)

        {

            //LoadSound.player16.Position = TimeSpan.Zero;
            //LoadSound.player16.Play();
            clickSound1.Stop();
        }
        //***********************************************************************************************************
        private void DeleteName(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Delete)
            {
                
                    var index = playersListView.Items.IndexOf(playersListView.SelectedItems[0]);
                    playersListView.Items.RemoveAt(index);
                    players.RemoveAt(index);
                    ScoreList.RemoveAt(index);
                if (ScoreList.Count == 0)
                    okButton.IsEnabled = false; ;
            }


           
        }
        //***********************************************************************************************************


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.Down)
            //{
            //    StartGame();
            //}
        }

        //***********************************************************************************************************


        //private void button1_Click(object sender, EventArgs e)
        //{
        //צריך להגדיר את המייל על רמת אבטחה נמוכה
        //    try
        //    {
        //        MailMessage mail = new MailMessage();
        //        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

        //        mail.From = new MailAddress("your_email_address@gmail.com");
        //        mail.To.Add("to_address");
        //        mail.Subject = "Test Mail";
        //        mail.Body = "This is for testing SMTP mail from GMAIL";

        //        SmtpServer.Port = 587;
        //        SmtpServer.Credentials = new System.Net.NetworkCredential("username", "password");
        //        SmtpServer.EnableSsl = true;

        //        SmtpServer.Send(mail);
        //        MessageBox.Show("mail Send");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //}
        //***********************************************************************************************************
    }
}
