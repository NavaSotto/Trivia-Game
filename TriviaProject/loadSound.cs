using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace TriviaProject
{
    
    static class LoadSound
    {
        public static MediaPlayer player1;
        public static MediaPlayer player2;
        public static MediaPlayer player3;
        public static MediaPlayer player5;
       
        public static MediaPlayer player8;
        public static MediaPlayer player13;
        public static MediaPlayer player14;
        public static MediaPlayer player16;



        public static void Load()
        {


            try
            {
                player1 = new System.Windows.Media.MediaPlayer();
                player1.Open(new Uri("sounds/s1.wav", UriKind.Relative));



                player2 = new System.Windows.Media.MediaPlayer();
                player2.Open(new Uri("sounds/s2.wav", UriKind.Relative));

                player3 = new System.Windows.Media.MediaPlayer();
                player3.Open(new Uri("sounds/s3.wav", UriKind.Relative));


               
                player5 = new System.Windows.Media.MediaPlayer();
                player5.Open(new Uri("sounds/s5.wav", UriKind.Relative));




                player8 = new System.Windows.Media.MediaPlayer();
                player8.Open(new Uri("sounds/s8.wav", UriKind.Relative));



                player13 = new System.Windows.Media.MediaPlayer();
                player13.Open(new Uri("sounds/s13.wav", UriKind.Relative));



                player14 = new System.Windows.Media.MediaPlayer();
                player14.Open(new Uri("sounds/s14.wav", UriKind.Relative));



                player16 = new System.Windows.Media.MediaPlayer();
                player16.Open(new Uri("sounds/s16.wav", UriKind.Relative));
                


            }

            catch (Exception ex)
            {
                ExceptionDetails.WriteException(ex);

                MessageBox.Show(" אנו מתנצלים, אך לא ניתן לטעון את קבצי המדיה, אנא פנה לתמיכה התכנית בטלפון :02-5344559"+ex.ToString());
                System.Diagnostics.Process.GetCurrentProcess().Kill();
                Application.Current.Shutdown();
            }





        }
        


    }
}
