using System;

using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;
using System.Media;
using System.Windows.Navigation;

namespace TriviaProject
{
    /// <summary>
    /// Interaction logic for AskScreen.xaml
    /// </summary>
    public partial class ExitScreen : Window
    {
        //========================================================================================================
        private bool ans;

        public object JsonSerializer { get; private set; }
        //========================================================================================================


        public ExitScreen()
        {
            

            InitializeComponent();
            //continuationScreenSound.Play();
           

            //LoadSound.player9.Play();
            //LoadSound.player9.MediaEnded += new EventHandler(Media_Ended);

        }
        //***********************************************************************************************************
        private void Media_Ended(object sender, EventArgs e)
        {
            //LoadSound.player9.Position = TimeSpan.Zero;
            //LoadSound.player9.Play();
        }


        //***********************************************************************************************************




        private void Button_Click_yes(object sender, RoutedEventArgs e)
        {
            

            LoadSound.player16.Play();


            NoButton.IsHitTestVisible = false;
            YesButton.IsHitTestVisible = false;
          

            ans = true;
          
            System.Diagnostics.Process.GetCurrentProcess().Kill();
            Application.Current.Shutdown();

        }


        //***********************************************************************************************************
        private void Button_Click_no(object sender, RoutedEventArgs e)
        {

            //LoadSound.player9.Stop();

            LoadSound.player16.Play();

            YesButton.IsHitTestVisible = false;

            NoButton.IsHitTestVisible = false;

            this.Close();

            //***********************************************************************************************************


        }

        //***********************************************************************************************************

        public bool getAns()
        {
            return ans;
        }
        //***********************************************************************************************************
        private void MediaFailedFunc(object sender, ExceptionRoutedEventArgs e)
        {
            Console.WriteLine("Failed:" + ((MediaElement)sender).Source);
        }
        //***********************************************************************************************************
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {


            //(this.Parent as Window).WindowState = WindowState.Maximized;

        }
        //***********************************************************************************************************
    }
}
