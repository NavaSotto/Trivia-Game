using System;

using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;
using System.Media;
using System.Windows.Navigation;
using System.Windows.Input;

namespace TriviaProject
{
    /// <summary>
    /// Interaction logic for AskScreen.xaml
    /// </summary>
    public partial class ErrorMessageCode : Window
    {
       private  System.Windows.Media.MediaPlayer clickSound = new System.Windows.Media.MediaPlayer();




        public ErrorMessageCode()
        {


           // InitializeComponent();

            //continuationScreenSound.Play();
  

        }

        private void closeErrorClick(object sender, RoutedEventArgs e)
        {

            clickSound.Open(new Uri("sounds/s16.wav", UriKind.Relative));
            clickSound.Play();

            clickSound.MediaEnded += new EventHandler(Media_Ended16);
            this.Close();



        }
        private void Media_Ended16(object sender, EventArgs e)
        {

            clickSound.Stop();


        }
        private void leaveX1(object sender, MouseEventArgs e)
        {
            XButton1.ImageSource = normalX1.Source;

        }

        private void enterX1(object sender, MouseEventArgs e)
        {
            XButton1.ImageSource = changeX1.Source;

        }

    }
}
