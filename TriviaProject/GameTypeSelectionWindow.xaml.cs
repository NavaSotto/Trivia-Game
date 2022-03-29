using System;
using System.Collections.Generic;
using System.IO;
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

namespace TriviaProject
{
    /// <summary>
    /// Interaction logic for GameTypeSelectionWindow.xaml
    /// </summary>
    public partial class GameTypeSelectionWindow : Window
    {
        //========================================================================================================
        const string triviaQuestionsFilePath = "files/triviaQuestionsFile.xlsx";
        
        private System.Windows.Media.MediaPlayer clickSound1 = new System.Windows.Media.MediaPlayer();
        //========================================================================================================

        public GameTypeSelectionWindow()
        {

            //LoadSound.player15.Stop();

            InitializeComponent();
            LoadSound.Load();



            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            clickSound1.Open(new Uri("sounds/s16.wav", UriKind.Relative));


            //LoadSound.player7.Play();
            // LoadSound.player7.MediaEnded += new EventHandler(Media_Ended);

        }
        //***********************************************************************************************************
        private void Media_Ended(object sender, EventArgs e)
        {
            //LoadSound.player7.Position = TimeSpan.Zero;
            //LoadSound.player7.Play();
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
        private void KidsModeImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //StartGame(childrenQuizPath);
        }
        //***********************************************************************************************************
        private void AdultsModeImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            StartGame(triviaQuestionsFilePath);
        }
        //***********************************************************************************************************
        private void StartGame(string quizFilePath)
        {
            //LoadSound.player7.Stop();
           
            //הדרך הטובה למעבר בין מסכים
            PlayerCreationWindow w = new PlayerCreationWindow();
           
            //this.Content = w;
            w.quizFilePath = quizFilePath;
            this.KeyDown -= Window_KeyDown;
           

            w.Show();
          
            this.Close();



        }


        //***********************************************************************************************************
       

        private void AdultsModeImage_MouseDown_Click(object sender, RoutedEventArgs e)
        {

            clickSound1.Play();

            clickSound1.MediaEnded += new EventHandler(Media_EndedClickSound1);

           
          
        }
        //***********************************************************************************************************

        private void leaveM(object sender, MouseEventArgs e)
        {



            //imgButton.ImageSource = normal.Source;
        }
        //***********************************************************************************************************
        private void enterM(object sender, MouseEventArgs e)
        {

            //imgButton.ImageSource = change.Source;

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
            // LoadSound.player7.Stop();
            //LoadSound.player16.Play();

            clickSound1.Play();



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
            clickSound1.Stop();
            StartGame(triviaQuestionsFilePath);
        }
        //***********************************************************************************************************

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            // if (e.Key == Key.Left)
            //{
            //    //KidsModeImage.Background = Brushes.White;
            //    //KidsModeImage.Width += 20;
            //    //KidsModeImage.Height += 20;

            //    StartGame(childrenQuizPath);
            //}
            //else if (e.Key == Key.Right)
            //{
            //    //AdultsModeImage.Background = Brushes.White;
            //    //AdultsModeImage.Width += 20;
            //    //AdultsModeImage.Height += 20;
            //    StartGame(adultsQuizPath);
            //}
        }

        //***********************************************************************************************************
        private void KidsModeImage_MouseDown_Click(object sender, RoutedEventArgs e)
        {
            //LoadSound.player16.Play();


            //StartGame(triviaQuestionsFilePath);
        }

        //***********************************************************************************************************



    }
}


