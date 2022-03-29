using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Reflection;
using System.IO;

namespace TriviaProject
{
    /// <summary>
    /// Interaction logic for AskScreen.xaml
    /// </summary>
    public partial class AnsScreen : Window
    {
        //private List<int> playerAnswers;
        private Dictionary<string, int> scores;
        private bool ans;
        private DispatcherTimer timerShowQ = new DispatcherTimer();
        private int count = 3;
        //private MediaElement videoElement = new MediaElement();

        
        public AnsScreen(string type,string source,string question )
        {
            InitializeComponent();
            questionLabel.Content = question;
            //WidenObject(150, TimeSpan.FromSeconds(15));

            switch (type)
            {
                case "none":
                    videoElementQ.Visibility = Visibility.Hidden;
                    imageQ.Visibility = Visibility.Hidden;
                    timerShowQ.Interval = TimeSpan.FromSeconds(15);
                    timerShowQ.Tick += closeWindow;
                    timerShowQ.Start();
                    break;
                case "video":
                    videoElementQ.Visibility = Visibility.Visible;
                    imageQ.Visibility = Visibility.Hidden;
                    videoElementQ.Source = new Uri(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "v3.mp4"));
                    videoElementQ.Play();
                    videoElementQ.MediaEnded += finishVideo;
                    break;
                case "image":
                    videoElementQ.Visibility = Visibility.Hidden;
                    imageQ.Source = new BitmapImage(new Uri("/Images/" + source, UriKind.Relative));
                    imageQ.Visibility = Visibility.Visible;
                    timerShowQ.Interval = TimeSpan.FromSeconds(15);
                    timerShowQ.Tick += finishImage;
                    timerShowQ.Start();
                    break;

            }
           


        }
        //private void WidenObject(int newWidth, TimeSpan duration)
        //{
        //    //continuationScreenSound.Play();
        //    timerShowQ.Interval = new TimeSpan(0,0,0,7);
        //    timerShowQ.Tick += TimerShowQ;
        //    timerShowQ.Start();
        //}
        //private void timerImage()
        //{
        //    //continuationScreenSound.Play();
        //    timerShowQ.Interval = TimeSpan.FromSeconds(7);
        //    timerShowQ.Tick += TimerShowQ;
        //    timerShowQ.Start();
        //}
        private void closeWindow(object sender, EventArgs e)
        {
            //MessageBox.Show("finish none");
            timerShowQ.Stop();
            Close();
        }



        private void finishImage(object sender, EventArgs e)
        {
            //MessageBox.Show("finish image");
            timerShowQ.Stop();
            Close();


        }
        private void finishVideo(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("finish video");
            this.Close();

        }




        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                //הדרך הטובה למעבר בין מסכים
                ExitScreen w = new ExitScreen();
                //System.Windows.Application.Current.MainWindow = w;
                w.InitializeComponent();
                //this.Hide();
                this.Content = w;
                //w.Show();
                //w.Left = Left;
               // w.Top = Top;
            }
            //if(e.Key== Key. Left)
            //{

            //}
        }
        //private void MediaFailedFunc(object sender, ExceptionRoutedEventArgs e)
        //{
        //    Console.WriteLine("Failed:" + ((MediaElement)sender).Source);
        //}

        
       
    }
}
