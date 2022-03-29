using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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
    /// Interaction logic for ScoreboardWindow.xaml
    /// </summary>
    public partial class ScoreboardWindow : Window
    {


        //========================================================================================================



        private const string programPurchasedPath = @"HKEY_CURRENT_USER\Software\Trivia\";


        private string name;
        private string city;
        private string type;
        private string totalTime;
        private string score;
        private string tell;
        private string mail;
        private string code;
        private string numOfPlayers;
        private string codeGame;
        private bool isFirstTime = true;

        private Dictionary<string,int> playersScore;
        private RegistryKey regKey;


        private System.Windows.Media.MediaPlayer clickSound1 = new System.Windows.Media.MediaPlayer();


        public KeyEventHandler Closing { get; private set; }


        //========================================================================================================



        public ScoreboardWindow()
        {
            LoadSound.player8.Play();

            InitializeComponent();


            CheckR();

            var currentGame = programPurchasedPath+Ans.currentCodeGame + '\\';
            regKey = Registry.CurrentUser.OpenSubKey(currentGame,true);
            UpdateData();


            clickSound1.Open(new Uri("sounds/s16.wav", UriKind.Relative));


            
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);

            checkStatus();



        }

        //************************************************************************************************************

        private void checkStatus()
        {

            
            //נבדוק ששילם על השעשועון 
            object value1 = regKey.GetValue("codeGame");
            //RegistryKey search = Registry.LocalMachine.OpenSubKey(programPurchasedPath);
            object value2 = regKey.GetValue("sendResult");


            //כבר שלח תוצאות לשעשועון זה
            if (value1.ToString() == codeGame && value2.ToString()=="True")
            {

                autoButton.Visibility = Visibility.Hidden;
                autoButton.IsEnabled = false;
                downloadButton.Visibility = Visibility.Visible;
                downloadButton.IsEnabled = true;
                isFirstTime = false;

            }
            else //עוד לא שלח תוצאות לשעשעון זה

            {
               
            }

        }
        //************************************************************************************************************

        private void UpdateData()
        {
            //מעדכן פרטים למקרה שיצא אחר ששילם ובכניסה חוזרת דילג על פרטים
            Ans.Code= regKey.GetValue("code").ToString();
           
           // Ans.Code =
            Ans.PrivateName = regKey.GetValue("PrivateName").ToString();

            Ans.City = regKey.GetValue("city").ToString();

            Ans.PhoneNum = regKey.GetValue("PhoneNum").ToString();

            Ans.Email = regKey.GetValue("Email").ToString();



            //ועדכון כל הפרטים פה לגישה נחה יותר


            name = Ans.PrivateName;
            city = Ans.City;
            tell = Ans.PhoneNum;
            mail = Ans.Email;
            type = Ans.Type.ToString();
            numOfPlayers = Ans.PlayersNum.ToString();
            code = Ans.Code;
            codeGame = Ans.currentCodeGame;
            totalTime = Ans.Time.ToString();
            score = Ans.Points.ToString();
            playersScore = Ans.playersResult;
          



            timeScoreStr.Content = "ניקוד כללי : " + score + "     " + "|" + "     " + "זמן משחק : " + totalTime;

            var dictSort = from objDict in playersScore orderby objDict.Value descending select objDict;

            switch (playersScore.Count)
            {
                case 0:
                    break;
                case 1:
                    {
                        firstPlaceLabel.Content = dictSort.ElementAt(0).Key;
                        firstS.Content = dictSort.ElementAt(0).Value;
                        break;
                    }
                case 2:
                    {
                        firstPlaceLabel.Content = dictSort.ElementAt(0).Key;
                        firstS.Content = dictSort.ElementAt(0).Value;
                        secondPlaceLabel.Content = dictSort.ElementAt(1).Key;
                        secondS.Content = dictSort.ElementAt(1).Value;
                        break;
                    }

                case 3:
                    firstPlaceLabel.Content = dictSort.ElementAt(0).Key;
                    firstS.Content = dictSort.ElementAt(0).Value;
                    secondPlaceLabel.Content = dictSort.ElementAt(1).Key;
                    secondS.Content = dictSort.ElementAt(1).Value;
                    thirdPlaceLabel.Content = dictSort.ElementAt(2).Key;
                    thirdS.Content = dictSort.ElementAt(2).Value;


                    break;
                default://more than 3 players
                    firstPlaceLabel.Content = dictSort.ElementAt(0).Key;
                    firstS.Content = dictSort.ElementAt(0).Value;
                    secondPlaceLabel.Content = dictSort.ElementAt(1).Key;
                    secondS.Content = dictSort.ElementAt(1).Value;
                    thirdPlaceLabel.Content = dictSort.ElementAt(2).Key;
                    thirdS.Content = dictSort.ElementAt(2).Value;
                    break;

            }



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


        private void CheckR()
    {
        var x = SystemParameters.PrimaryScreenWidth;
        var y = SystemParameters.PrimaryScreenHeight;

        if (x != 1920 || y != 1080)
        {
                timeScoreStr.FontSize = 20;
                firstPlaceLabel.FontSize = 20;
                secondPlaceLabel.FontSize = 20;
                thirdPlaceLabel.FontSize = 20;
                firstS.FontSize = 20;
                secondS.FontSize = 20;
                thirdS.FontSize = 20;
                downloadResultsLink.FontSize = 25;

            }

        }



        //************************************************************************************************************

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

           

           

        }
        //************************************************************************************************************


        private void autoButton_Click(object sender, RoutedEventArgs e)
        {

            clickSound1.Play();

            clickSound1.MediaEnded += new EventHandler(Media_EndedClickSound1);
            try
            {
                //נרשם ששלחת תוצאות
                regKey.SetValue("sendResult","True");
                var d=regKey.GetValue("sendResult").ToString();
                ;
                //regKey.Close();


                try
                {
                    string encodedInfo = EncodeInfo();

                    Clipboard.SetText(encodedInfo);

                    //open txt
                   // var temp = System.IO.Path.GetTempFileName();
                    //System.IO.File.WriteAllText(temp, Clipboard.GetText());
                    //Process.Start("Notepad.exe", temp);



                }
                catch (Exception ex)
                {
                    ExceptionDetails.WriteException(ex);
                    MessageBox.Show(" אנו מתנצלים, אך לא ניתן לחשב תוצאות, אנא פנה לתמיכה התכנית בטלפון :02-5344559"+ex.ToString());

                }
              



            }
            catch (Exception ex)
            {
            }
            


            sendM.Visibility = Visibility.Visible;
            downloadResultsLink.Visibility = Visibility.Visible;
            closeMsg.Visibility = Visibility.Visible;
            closeMsg.IsEnabled = true;
            //timeScoreStr.Visibility = Visibility.Hidden;
            closePage.Visibility = Visibility.Hidden;
            closePage.IsEnabled = true;
            firstPlaceLabel.Visibility = Visibility.Hidden;
            secondPlaceLabel.Visibility = Visibility.Hidden;
            thirdPlaceLabel.Visibility = Visibility.Hidden;
            firstS.Visibility = Visibility.Hidden;
            secondS.Visibility = Visibility.Hidden;
            thirdS.Visibility = Visibility.Hidden;
            timeScoreStr.Visibility = Visibility.Hidden;
            autoButton.Visibility = Visibility.Hidden;
            autoButton.IsEnabled = false;




        }
        //************************************************************************************************************


        private void results()
        {

            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "xls";
            saveFileDialog.Filter = "Excel files (*.xls)|*.xls |All files (*.*)|*.*";

            //Start Excel and get Application object.
            var oXL = new Microsoft.Office.Interop.Excel.Application();
            oXL.DisplayAlerts = false;
            oXL.Visible = true;

            //Get a new workbook.
            var oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
            var oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;


            try
            {
                //Add table headers going cell by cell.



                oSheet.Cells[1, 1] = "שם פרטי";
                oSheet.Cells[1, 2] = "עיר";
                oSheet.Cells[1, 3] = "טלפון";
                oSheet.Cells[1, 4] = "מייל";
                oSheet.Cells[1, 5] = "מספר משתתפים";
                oSheet.Cells[1, 6] = "ניקוד";
                oSheet.Cells[1, 7] = "זמן";
                oSheet.Cells[1, 8] = "סוג משחק";
               

                oSheet.Cells[2, 1] = name;
                oSheet.Cells[2, 2] = city;
                oSheet.Cells[2, 3] = tell;
                oSheet.Cells[2, 4] = mail;
                oSheet.Cells[2, 5] = numOfPlayers;
                oSheet.Cells[2, 6] = score;
                oSheet.Cells[2, 7] = totalTime;
                oSheet.Cells[2, 8] = type;



                //רק אם זו הפעם הראשונה נדפיס את החישוב המשוקלל כי לא נרצה שיששלח אותו במצב אחר
                if (isFirstTime)
                {
                    oSheet.Cells[1, 9] = "קוד תוצאות לשליחה";

                    oSheet.Cells[2, 9] = EncodeInfo();
                }
                   

                for (int r = 0; r < playersScore.Count; r++)
                {

                    oSheet.Cells[r + 4, 1] = playersScore.Keys.ElementAt(r);
                    oSheet.Cells[r + 4, 2] = playersScore.Values.ElementAt(r);


                }



                if (saveFileDialog.ShowDialog() == true)
                {

                    oSheet.SaveAs(saveFileDialog.FileName); //Save   
                    oWB.Close(SaveChanges:false);
                    oXL.Quit();

                }
            }
            catch (Exception ex)
            {
                ExceptionDetails.WriteException(ex);

                MessageBox.Show(" אנו מתנצלים, אך  לא ניתן להוריד את הקובץ "+ex.ToString());
                //System.Diagnostics.Process.GetCurrentProcess().Kill();
                //Application.Current.Shutdown();
            }


        }
        //************************************************************************************************************



        private string Encode(string input)
        {
            var s=System.Text.Encoding.UTF8.GetBytes(input);
            return System.Convert.ToBase64String(s);
        }
        //************************************************************************************************************


        private string EncodeInfo()
        {

            string r = "%"+ "\n"+ "Name: " + " " + name + "\n" + "city: " + " " + city + "\n" + "phone: " + " " + tell + "\n" + "email:" + mail  +"\n" + "numOfPlayers: " +  numOfPlayers + "\n" + "typeOfGame: " + " " + type + "\n" + "id: " + " " + code + "\n" +"score:" + " " + score + "\n" + "time:" + " " + totalTime+ "\n"+ "codeGame: " + " " + codeGame + "\n" + "%";

            return  Encode(r) ;

        }

        //************************************************************************************************************

        private void closeMsgClick(object sender, RoutedEventArgs e)
        {


            clickSound1.Play();

            clickSound1.MediaEnded += new EventHandler(Media_EndedClickSound1);


            try
            {
                string encodedInfo = EncodeInfo();

                Clipboard.SetText(encodedInfo);
            }
            catch (Exception ex)
            {
                ExceptionDetails.WriteException(ex);
                MessageBox.Show(" אנו מתנצלים, אך לא ניתן לחשב תוצאות, אנא פנה לתמיכה התכנית בטלפון :02-5344559"+ex.ToString());

            }


            autoButton.Visibility = Visibility.Hidden;
            autoButton.IsEnabled = false;

            sendM.Visibility = Visibility.Hidden;
            downloadResultsLink.Visibility = Visibility.Hidden;
            closeMsg.Visibility = Visibility.Hidden;
            closeMsg.IsEnabled = false;

            timeScoreStr.Visibility = Visibility.Visible;
            closePage.Visibility = Visibility.Visible;
            closePage.IsEnabled = true;
            firstPlaceLabel.Visibility = Visibility.Visible;
            secondPlaceLabel.Visibility = Visibility.Visible;
            thirdPlaceLabel.Visibility = Visibility.Visible;
            firstS.Visibility = Visibility.Visible;
            secondS.Visibility = Visibility.Visible;
            thirdS.Visibility = Visibility.Visible;
            timeScoreStr.Visibility = Visibility.Visible;

        }

        //************************************************************************************************************

        private void leave2(object sender, MouseEventArgs e)
        {
            imgButton2.ImageSource = normal2.Source;

        }
        //************************************************************************************************************


        private void enter2(object sender, MouseEventArgs e)
        {
            imgButton2.ImageSource = change2.Source;

        }
        //************************************************************************************************************

        private void leave1(object sender, MouseEventArgs e)
        {
            imgButton1.ImageSource = normal1.Source;

        }
        //************************************************************************************************************

        private void enter1(object sender, MouseEventArgs e)
        {
            imgButton1.ImageSource = change1.Source;

        }
        //************************************************************************************************************

        private void leaveX(object sender, MouseEventArgs e)
        {
            XButton.ImageSource = normalX.Source;

        }
        //************************************************************************************************************


        private void enterX(object sender, MouseEventArgs e)
        {
            XButton.ImageSource = changeX.Source;

        }
        //************************************************************************************************************

        private void closePageClick(object sender, RoutedEventArgs e)
        {
            LoadSound.player8.Stop();


             clickSound1.Play();

            clickSound1.MediaEnded += new EventHandler(Media_EndedClickSound1);

            ExitScreen w = new ExitScreen();
            w.ShowDialog();

            w.InitializeComponent();
               
        }

        //************************************************************************************************************




        private void d_results(object sender, RoutedEventArgs e)
        {
            results();
        }

        //************************************************************************************************************


        private void Media_EndedClickSound1(object sender, EventArgs e)

        {

           
            clickSound1.Stop();
            //await Task.Delay(2000);
        }

        //************************************************************************************************************




        //private void Hyperlink_OnClick(object sender, RoutedEventArgs e)
        //{
        //    string mailto = string.Format("mailto:{0}?Subject={1}&Body={2}", "game@interactive360.co.il", EncodeInfo(), "This is a body of a message");
        //    mailto = Uri.EscapeUriString(mailto);
        //    System.Diagnostics.Process.Start(mailto);
        //}



        //private void SendCode(string emailAddress, string encodedInfo)
        //{
        //    SendEmail(emailAddress, encodedInfo,"");
        //}
        //************************************************************************************************************


    }




}




