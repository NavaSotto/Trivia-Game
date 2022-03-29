using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace TriviaProject
{
    /// <summary>
    /// Interaction logic for WelcomeWindow.xaml
    /// </summary>
    public partial class WelcomeWindow : Window
    {
        public WelcomeWindow()
        {
            InitializeComponent();
            //CheckR();
           
            LoadSound.Load();



            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
           
            LoadSound.player1.Play();
            LoadSound.player1.MediaEnded += new EventHandler(Media_Ended);


        }



        //************************************************************************************************************
        private void Media_Ended(object sender, EventArgs e)
        {
            LoadSound.player1.Position = TimeSpan.Zero;
            LoadSound.player1.Play();
        }

        //************************************************************************************************************



        private void MoveToNextWindow()
        {
            LoadSound.player1.Stop();

            //player.Stop();
            //הדרך הטובה למעבר בין מסכים
            InstructionsScreen w = new InstructionsScreen();

            // _mainFrame.Navigate(w);
            //w.InitializeComponent();

           
            w.Show();
            this.Close();



        }

        //************************************************************************************************************




        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoadSound.player16.Play();
            MoveToNextWindow();
        }
        //************************************************************************************************************

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

        }
        //************************************************************************************************************

        private void Instructions(object sender, RoutedEventArgs e)
        {

        }
        //************************************************************************************************************

        private void HandleEsc(object sender, KeyEventArgs e)
        {


            if (e.Key == Key.Escape)
            {
             
                this.WindowState = WindowState.Minimized;
            }

        }
        //************************************************************************************************************




    }
}
