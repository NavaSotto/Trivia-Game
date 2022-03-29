using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace TriviaProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Page
    {
        private const string InstrucitonsPath = "הוראות.pdf";
       
        public MainWindow()
        {
            //this.WindowHeight = 700;
            //this.WindowHeight = 800;

            InitializeComponent();
            checkbox.IsChecked = false;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);

        }


    
    private void HandleEsc(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Escape)
        {
                //הדרך הטובה למעבר בין מסכים
                ExitScreen w = new ExitScreen();
                //System.Windows.Application.Current.MainWindow = w;
                w.InitializeComponent();
                this.Content = w;
                //this.Hide();
                //w.ShowDialog();
                //w.Left = Left;
                //w.Top = Top;
            }
        //if(e.Key== Key. Left)
        //{

        //}
    }

    private void InstructionsLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!File.Exists(InstrucitonsPath))
            {
                MessageBox.Show("לא נמצא התקנון המלא");
            }
            else
            {
                Process.Start(InstrucitonsPath);
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            checkbox.IsChecked = true;

        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                MoveToNextScreen();
            }
        }

        private void ScreenZeroContinueButton_Click(object sender, RoutedEventArgs e)
        {
            if (checkbox.IsChecked == true)
            {
                MoveToNextScreen();

            }
            else
                checkbox.BorderBrush = Brushes.Red; 
        }

        private void MoveToNextScreen()
        {
            //הדרך הטובה למעבר בין מסכים
            dataUser window = new dataUser();
            //Application.Current.MainWindow = window;
            window.InitializeComponent();
            this.KeyDown -= Window_KeyDown;
            _mainFrame.Navigate(window);
            //this.Hide();
            //window.ShowDialog();
            //window.Left = Left;
            //window.Top = Top;

            //NavigationWindow w = new NavigationWindow();
            //w.Content = new dataUser();

    
            //w.ShowDialog();
            //winBchAdmin.ShowDialog();






        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            //(this.Parent as Window).WindowState = WindowState.Maximized;

        }
    }
}
