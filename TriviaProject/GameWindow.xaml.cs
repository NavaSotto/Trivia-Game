using System;
using System.IO;
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
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Threading;
using System.Security.Policy;
using System.Data;
using System.Windows.Media.Animation;
using System.Threading;
using System.Windows.Interop;
using System.Reflection;

namespace TriviaProject
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        //========================================================================================================

        //data from quiz file
        public string quizFilePath;
        private int totalScore;
        private int totalAnswers;
        private int totalSeconds;
        private int questionScore;
        private int questionIndex;
        private int currentQuestionTime;
        private string correctAnswer;
        private Excel.Application excelApp;
        private Excel.Worksheet worksheet;
        private Excel.Workbook workbook;

        //private List<string> currentPlayerAns = new List<string>();
        private string questionStartImage;
        private string questionStartVideo;
        private string statusQuestion;
        private bool isEveryoneVoted = false;
        private bool isLastPLayerVoted=false;


        //list of scores data
        public Dictionary<string, int> scores;//מאותחל ע"י מסך סחירת השחקנים

        //timer
        private DispatcherTimer timer;//טיימר להקשת תשובות
        private DispatcherTimer clock;//טיימר לשעון משחק

        private DispatcherTimer ImgQ = new DispatcherTimer();//טיימר לתצוגת תמונה
        private DispatcherTimer normalQ = new DispatcherTimer();//טיימר לתצוגת שאלה
        private int displayImgTime = 10;
        private int displayQTime = 10;
        private string turn;
        private bool isNormalResolution = true;

        private System.Windows.Media.MediaPlayer clickSound = new System.Windows.Media.MediaPlayer();

        private System.Windows.Media.MediaPlayer answerSound = new System.Windows.Media.MediaPlayer();


        //flag
        private bool isVideoOrImageQ = false;
        private bool isTimeToAns = false;
        private bool isEndGame = false;
        private bool isCurrentPlayerAns = false;
        List<int> list = new List<int>() { };

        //========================================================================================================



        public GameWindow()
        {
            InitializeComponent();
            try
            {

                clickSound.Open(new Uri("sounds/s16.wav", UriKind.Relative));
                clickSound.MediaEnded += new EventHandler(Media_Ended16);

                answerSound.Open(new Uri("sounds/s13.wav", UriKind.Relative));

            }

            catch (Exception ex)
            {
                ExceptionDetails.WriteException(ex);
                MessageBox.Show("(אנו מתנצלים, אך לא ניתן לטעון את קבצי המדיה (4"+ex.ToString());
                //System.Diagnostics.Process.GetCurrentProcess().Kill();
                //Application.Current.Shutdown();
            }


            //טיימר תצוגת זמן משחק
            //טיימר כללי להקשת תשובות
            clock = new DispatcherTimer();
            clock.Tick += clock_Tick;
            clock.Interval = TimeSpan.FromMilliseconds(1000);

            clock.Start();

            //טיימר כללי להקשת תשובות
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromMilliseconds(1000);

            CheckR();



        }

        public void LoadWin(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                //Visible
                MessageBox.Show("ukjhb");
            }
            else
            {
                //Not Visible
            }
        }
        //***************************************************************************************************
        private void clock_Tick(object sender, EventArgs e)
        {
            totalGameTimeLabel.Content = new TimeSpan(0, 0, ++totalSeconds).ToString(@"hh\:mm\:ss");

        }



        //***************************************************************************************************
        private void CheckR()

        {
            double width = System.Windows.SystemParameters.PrimaryScreenWidth;
            double height = System.Windows.SystemParameters.PrimaryScreenHeight;
            if (width != 1920 || height != 1080)
            {
                isNormalResolution = false;
                questionLabel.FontSize = 20;
                answerLabel1.FontSize = 18;
                answerLabel2.FontSize = 18;
                answerLabel3.FontSize = 18;
                answerLabel4.FontSize = 18;
                index1.FontSize = 18;
                index2.FontSize = 18;
                index3.FontSize = 18;
                index3.FontSize = 18;

                //for correct answer1
                answear1GridanswerLabel1.FontSize = 17;
                answear1GridanswerLabel2.FontSize = 17;
                answear1GridanswerLabel3.FontSize = 17;
                answear1GridanswerLabel4.FontSize = 17;

                answear1Gridindex1.FontSize = 17;
                answear1Gridindex2.FontSize = 17;
                answear1Gridindex3.FontSize = 17;
                answear1Gridindex4.FontSize = 17;
                //for correct answer2
                answear2GridanswerLabel1.FontSize = 17;
                answear2GridanswerLabel2.FontSize = 17;
                answear2GridanswerLabel3.FontSize = 17;
                answear2GridanswerLabel4.FontSize = 17;

                answear2Gridindex1.FontSize = 17;
                answear2Gridindex2.FontSize = 17;
                answear2Gridindex3.FontSize = 17;
                answear2Gridindex4.FontSize = 17;
                //for correct answer3
                answear3GridanswerLabel1.FontSize = 17;
                answear3GridanswerLabel2.FontSize = 17;
                answear3GridanswerLabel3.FontSize = 17;
                answear3GridanswerLabel4.FontSize = 17;

                answear3Gridindex1.FontSize = 17;
                answear3Gridindex2.FontSize = 17;
                answear3Gridindex3.FontSize = 17;
                answear3Gridindex4.FontSize = 17;
                //for correct answer4
                answear4GridanswerLabel1.FontSize = 17;
                answear4GridanswerLabel2.FontSize = 17;
                answear4GridanswerLabel3.FontSize = 17;
                answear4GridanswerLabel4.FontSize = 17;

                answear4Gridindex1.FontSize = 17;
                answear4Gridindex2.FontSize = 17;
                answear4Gridindex3.FontSize = 17;
                answear4Gridindex4.FontSize = 17;
                //**************************************************

                numberQ.FontSize = 25;
                totalGameTimeLabel.FontSize = 25;
                scoreQ.FontSize = 25;
                totalScoreLabel.FontSize = 25;
                top1.FontSize = 22;
                top2.FontSize = 22;
                top3.FontSize = 22;
                top1S.FontSize = 25;
                top2S.FontSize = 25;
                top3S.FontSize = 25;
                playerLabel.FontSize = 22;
                ans.FontSize = 20;

                questionTimeLabel.FontSize = 60;
               
                questionLabel2.FontSize = 20;
                Canvas.SetTop(ts, -2.5);
                //ts.VerticalContentAlignment = VerticalAlignment.Top;


                // answerLabel1.Margin = new Thickness(1022, 370, 183, 616);




            }



        }

        //***************************************************************************************************

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.IsVisibleChanged += LoadWin;


            if (isNormalResolution)
                {
                    ts.FontSize = 50;
                }
                else
                {
                    ts.FontSize = 35;
                }
            try
            {
                excelApp = new Excel.Application();
                excelApp.DisplayAlerts = false;
                 workbook = excelApp.Workbooks.Open(Path.Combine(Directory.GetCurrentDirectory(), quizFilePath), ReadOnly: true, Password: SecurityHelper.GetFilesCode());
                worksheet = workbook.ActiveSheet;
            }


            catch (Exception ex)
            {
                ExceptionDetails.WriteException(ex);
                MessageBox.Show(" אנו מתנצלים, אך לא ניתן לטעון את מסך הטריוויה (6), אנא פנה לתמיכה התכנית בטלפון :02-5344559"+ex.ToString());

                System.Diagnostics.Process.GetCurrentProcess().Kill();
                Application.Current.Shutdown();
            }



            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);

            StartGame();



        }



        //***************************************************************************************************

        private void StartGame()
        {
            //Let's look at something else that can change in terms of display-list players

            if (!isNormalResolution)
            {
                dg.FontSize = scores.Count < 7 ? 25 : 16;
                dgScore.FontSize = scores.Count < 7 ? 25 : 16; 
            }
            else
            {
                dg.FontSize = scores.Count < 7 ? 55 : 35;
                dgScore.FontSize = scores.Count < 7 ? 55 : 35; 
            }





            questionIndex = 1;
            numberQ.Content = "1";
            questionScore = 0;
            totalAnswers = 0;
            totalSeconds = 0;
            totalScore = 0;
            playerLabel.Content = "";
           
            ans.Content = "";
            totalScoreLabel.Content = "";

            for (int i = 0; i < scores.Count; i++)
            {
                list.Add(0);
            }
            LoadQuestion(questionIndex);
            this.KeyDown += new KeyEventHandler(KeyDownEvent);
        }



        //***************************************************************************************************


        private void LoadQuestion(int questionIndex)
        {

            numberQ.Content = questionIndex;

            //נושא קריאה מקובץ
            string typeGame = GetCellValue(1, questionIndex);
            string lofoImg = GetCellValue(2, questionIndex);
            //update type of game
            Ans.Type = typeGame;
            


            //משורה 3 מתחילות השאלות וכל פעם נקרא שורה=שאלה נוספת
            questionIndex += 2;
            string theme = GetCellValue(1, questionIndex);

            //אם תא הנושא ריק סימן שלא נותרה עוד שורה של שאלה לקרא והמשחק יסתיים
            if (theme.Length == 0)
            {
                workbook.Close(SaveChanges: false);
                //נגמרו השאלות בטריוויה
                StartScoreboardScreen();
                isEndGame = true;
                return;

            }
            else if (theme.Length > 0)   //תא הנושא מכיל משהו-כלומר יש עוד שאלה

            {



                //קריאה מקובץ -ניקוד שאלה
                questionScore = int.Parse(GetCellValue(2, questionIndex));
                //ניקוד שאלה
                scoreQ.Content = questionScore.ToString();
                //זמן שאלה קריאה מקובץ
                currentQuestionTime = int.Parse(GetCellValue(3, questionIndex));
                //תצוגת טיימר שאלה
                questionTimeLabel.Content = currentQuestionTime.ToString();



                //שאלה קריאה מקובץ
                string question = GetCellValue(4, questionIndex);

                // קריאה מקובץ תמונה לפני שאלה
                questionStartImage = GetCellValue(9, questionIndex);
                //סרטון לפני שאלהקריאה מקובץ 
                questionStartVideo = GetCellValue(10, questionIndex);

                string[] answers = new string[4];
                List<string> answersList = new List<string>();

                for (int i = 0; i < answers.Length; i++)
                {
                    // קריאה מקובץ תשובות אופציונאליות
                    answers[i] = GetCellValue(i + 5, questionIndex);

                    if (answers[i].Length > 0)
                    {
                        answers[i] = answers[i];
                        //רשימת תשובות לשאלה נוכחית(המערך נמחק אחר כל שאלה)
                        answersList.Add(answers[i]);
                    }
                }

                //תצוגת שאלה
                questionLabel.Content = question;
                questionLabel2.Content = question;//למסך תמונה או סרטון

                //תשובה נכונה לשאלה נוכחית
                correctAnswer = answers[0];

                //*************************************************************
                //תצוגת תשובות
                Random random = new Random();
                int index;

                if (answersList.Count > 0)
                {
                    index = random.Next(answersList.Count);
                    answerLabel1.Content = /*"1. " +*/ answersList[index];
                    answersList.RemoveAt(index);
                }
                else
                {
                    answerLabel1.Content = "";
                }

                if (answersList.Count > 0)
                {
                    index = random.Next(answersList.Count);
                    answerLabel2.Content = /*"2. " +*/ answersList[index];
                    answersList.RemoveAt(index);
                }
                else
                {
                    answerLabel2.Content = "";
                }

                if (answersList.Count > 0)
                {
                    index = random.Next(answersList.Count);
                    answerLabel3.Content = /*"3. " +*/ answersList[index];
                    answersList.RemoveAt(index);
                }
                else
                {
                    answerLabel3.Content = "";
                }

                if (answersList.Count > 0)
                {
                    index = random.Next(answersList.Count);
                    answerLabel4.Content = /*"4. " +*/ answersList[index];
                    answersList.RemoveAt(index);
                }
                else
                {
                    answerLabel4.Content = "";
                }
                //*************************************************************
                CheckTypeuestion();
            }
        }


        //***************************************************************************************************
        private void CheckTypeuestion()
        {
            //בדיקה איזו סוג שאלה זו-רגילה/תמונה/וידאו

            if (questionStartImage.Length + questionStartVideo.Length != 0)
            {


                if (questionStartVideo.Length > 0)
                {
                    //תצוגת וידאו או תמונה


                    try
                    {

                        string videoPath = "questionsMedia/" + questionStartVideo;

                        QueMedia.BeginInit();
                        QueMedia.Source = new Uri(System.IO.Path.Combine(Directory.GetCurrentDirectory(), videoPath));
                        QueMedia.EndInit();


                    }

                    catch
                    {

                        System.Diagnostics.Process.GetCurrentProcess().Kill();
                        Application.Current.Shutdown();
                    }
                    statusQuestion = "video";


                }
                else if (questionStartImage.Length > 0)//So it's image
                {



                    try
                    {

                        string imagePath = "questionsMedia/" + questionStartImage;
                        QueImg.BeginInit();
                        QueImg.Source = new BitmapImage(new Uri(System.IO.Path.Combine(Directory.GetCurrentDirectory(), imagePath)));

                        QueImg.EndInit();







                    }
                    catch
                    {

                        System.Diagnostics.Process.GetCurrentProcess().Kill();
                        Application.Current.Shutdown();
                    }
                    statusQuestion = "image";



                }



            }

            else if (questionStartImage.Length + questionStartVideo.Length == 0)//אם זו שאלה רגילה-לא וידאו או תמונה
            {


                statusQuestion = "normal";


            }

            QuestionView();



        }




        //***************************************************************************************************
        private void QuestionView()
        {
            switch (statusQuestion)
            {
                case "normal":
                    {
                        //טיימר לתצוגת שאלה
                        initTriviaScreen();
                        inQ.Visibility = Visibility.Visible;
                        StartTimerTypedAnswers();


                        ////LoadSound.player2.Play();
                        //////LoadSound.player2.MediaEnded += new EventHandler(Media_Ended2);

                        ////normalQ.Tick += finishQuestionView;

                        ////normalQ.Interval = TimeSpan.FromSeconds(0);//6   //טיימר לתצוגת שאלה
                        ////normalQ.Start();
                    }
                    break;
                case "image":
                    {
                        inQ.Visibility = Visibility.Hidden;
                        beforeQ.Visibility = Visibility.Visible;

                        //טיימר לתצוגת תמונה
                        ImgQ.Tick += finishImage;
                        ImgQ.Interval = TimeSpan.FromSeconds(4);//15
                        ImgQ.Start();//טיימר לתמונה


                    }
                    break;
                case "video":
                    {
                        inQ.Visibility = Visibility.Hidden;

                        beforeQ.Visibility = Visibility.Visible;

                        QueMedia.MediaEnded += finishVideo;
                        QueMedia.LoadedBehavior = MediaState.Manual;
                        QueMedia.Play();//טיימר לסרטון כאורך הסרטון

                    }
                    break;
                default:
                    break;

            }


        }



        //***************************************************************************************************

        private void finishQuestionView(object sender, EventArgs e)
        {
            LoadSound.player2.Stop();

            normalQ.Stop();//מסתיימת תצוגת השאלה
            StartTimerTypedAnswers();

            normalQ.Tick -= finishQuestionView;
        }
        //***************************************************************************************************

        private void finishVideo(object sender, RoutedEventArgs e)
        {


            QueMedia.Stop();
            QueMedia.Close();
            QueMedia.Source = null;
            QueMedia.Source = null;
            beforeQ.Visibility = Visibility.Hidden;
            //QueMedia.Visibility = Visibility.Hidden;
            //questionLabel2.Visibility = Visibility.Hidden;

            statusQuestion = "normal";
            QueMedia.MediaEnded -= finishVideo;
            QuestionView();

        }
        //***************************************************************************************************


        private void finishImage(object sender, EventArgs e)
        {
            ImgQ.Stop();
            QueImg.Source = null;
            beforeQ.Visibility = Visibility.Hidden;


            ImgQ.Tick -= finishImage;
            statusQuestion = "normal";
            QuestionView();




        }
        //***************************************************************************************************

        private void StartTimerTypedAnswers()
        {
            playerLabel.Visibility = Visibility.Visible;
            ans.Visibility = Visibility.Visible;


            playerLabel.Content = GetCurrentPlayer();

            //טיימר להקשת תשובות מופעל
            LoadSound.player3.Play();
            LoadSound.player3.MediaEnded += endTypeAns;
            isTimeToAns = true;

            timer.Start();





        }

        //***************************************************************************************************

        private void endTypeAns(object sender, EventArgs e)
        {
            LoadSound.player3.Position = TimeSpan.Zero;
            LoadSound.player3.Play();
        }

        //***************************************************************************************************

        private void Timer_Tick(object sender, EventArgs e)
        {
            //מצבים אפשריים בזמן שטיימר הקשת התשובות פועל:
            //לא נגמר הזמן-השעון ממשיך לספור לאוחר
            //נגמר הזמן(וכולם ענו/שלא כולם ענו):
            //נאיר תשובה+נחשב מובילים+נעבור למסך תוצאות ביניים במקרה מסוים+נטען שאלה הבאה




            if (currentQuestionTime > 0)
            {
                //אתחול טיימר שאלה
                currentQuestionTime--;

                //תצוגת טיימר שאלה
                questionTimeLabel.Content = currentQuestionTime;
            }
            if (currentQuestionTime == 0 || isEveryoneVoted == true)
            {
               
                timer.Stop();
                resultQ();
                
            }




        }
        //***************************************************************************************************

        private void resultQ()
        {
            isTimeToAns = false;
            totalAnswers = 0;
            isEveryoneVoted = false;
            ShowCorrectAnswers();//נאיר את התשובה הנכונה
            playerLabel.Content = "";
            ans.Content = "";
            CalculateScore();//נחשב מחדש את 3 המובילים
        }

        //***************************************************************************************************
        //פונקציה המטפלת בארוע מסוג הקלקה במקלדת-קלט יתקבל רק בזמן הקשת תשובות
        public void KeyDownEvent(object sender, KeyEventArgs e)
        {

            if (isTimeToAns)
            {
                if (IsGameKey(e.Key))
                {


                    //If one of the keys is pressed 1 2 3 4
                    //                    if (isCurrentPlayerAns == false && !e.IsRepeat)
                    if (!e.IsRepeat)
                    {

                        ans.Content = "*";
                        try
                        {
                            clickSound.Play();

                        }

                        catch (Exception ex)
                        {
                            ExceptionDetails.WriteException(ex);
                            MessageBox.Show("(אנו מתנצלים, אך לא ניתן לטעון את קבצי המדיה (5" + ex.ToString());
                            //System.Diagnostics.Process.GetCurrentProcess().Kill();
                            //Application.Current.Shutdown();
                        }




                        //playerLabel.Content = scores.Keys.ElementAt(totalAnswers % scores.Count);                        
                        isCurrentPlayerAns = true;

                        int answerIndex = int.Parse(e.Key.ToString().Replace("D", "").Replace("NumPad", ""));



                        //בדיקה אם התשובה שהוקשה נכונה
                        if (GetAnswer(answerIndex) == correctAnswer)
                        {

                            //Check if a score has been added to this player already in the current question
                            //totalScore = 0;
                            int indexCurrentPlayer = totalAnswers % scores.Count;
                           
                            if ( scores[GetCurrentPlayer()] != list[indexCurrentPlayer] + questionScore && !isEveryoneVoted)
                            {
                                //עדכון ניקוד שחקן
                                totalScore += questionScore;
                                //עדכון ניקוד לשחקן
                                scores[GetCurrentPlayer()] += questionScore;

                            }





                        }


                        if (++totalAnswers % scores.Count == 0)
                        {
                            playerLabel.Visibility = Visibility.Hidden;
                            ans.Visibility = Visibility.Hidden;
                            isEveryoneVoted = true;
                            
                              
                            
                           


                        }
                        else
                        {

                            playerLabel.Content = GetCurrentPlayer();


                        }
                        isCurrentPlayerAns = true;
                    }

                }

            }
            



        }

        //***************************************************************************************************


        private void Media_Ended16(object sender, EventArgs e)
        {

            clickSound.Stop();
           

            ans.Content = "";
            isCurrentPlayerAns = false;
            




        }


        //***************************************************************************************************
        private bool IsGameKey(Key key)
        {
            //LoadSound.player16.Play();
            //מחזיר טרו אם הכפתור שנלחץ הוא מ4 המספרים
            if (key == Key.D1 || key == Key.D2 || key == Key.D3 || key == Key.D4 || key == Key.NumPad1 || key == Key.NumPad2 || key == Key.NumPad3 || key == Key.NumPad4  /*|| key == Key.D5 || key == Key.D6*/)
            {

                return true;
            }
            else
                return false;

        }

        //***************************************************************************************************

        //-->
        private void ShowCorrectAnswers()
        {
           
            statusQuestion = "showCorrectAnswer";

            LoadSound.player3.Stop();

            this.KeyDown -= new KeyEventHandler(KeyDownEvent);
            //טיימר להארת תשובה נכונה

            var index = CheckIndexOfCorrectAnswer();
            answearRegularGrid.Visibility = Visibility.Hidden;

            switch (index)
            {
                case 1:
                    imageB.ImageSource = imgc1.Source;
                    answear1GridanswerLabel1.Content = answerLabel1.Content;
                    answear1GridanswerLabel2.Content = answerLabel2.Content;
                    answear1GridanswerLabel3.Content = answerLabel3.Content;
                    answear1GridanswerLabel4.Content = answerLabel4.Content;
                    answear1Grid.Visibility = Visibility.Visible;
                    if(isNormalResolution)
                    {
                        answear1GridanswerLabel1.FontSize = 40;
                        answear1Gridindex1.FontSize = 40;
                    }
                       
                    else
                    {
                        answear1GridanswerLabel1.FontSize = 21;
                        answear1Gridindex1.FontSize = 21;

                    }

                  
                    break;
                case 2:
                    imageB.ImageSource = imgc2.Source;
                    answear2GridanswerLabel1.Content = answerLabel1.Content;
                    answear2GridanswerLabel2.Content = answerLabel2.Content;
                    answear2GridanswerLabel3.Content = answerLabel3.Content;
                    answear2GridanswerLabel4.Content = answerLabel4.Content;
                    answear2Grid.Visibility = Visibility.Visible;
                   

                   

                    if (isNormalResolution)
                    {
                        answear2GridanswerLabel2.FontSize = 40;
                        answear2Gridindex2.FontSize = 40;
                    }

                    else
                    {
                        answear2GridanswerLabel2.FontSize = 21;
                        answear2Gridindex2.FontSize = 21;

                    }

                   

                    break;
                case 3:
                    imageB.ImageSource = imgc3.Source;
                    answear3GridanswerLabel1.Content = answerLabel1.Content;
                    answear3GridanswerLabel2.Content = answerLabel2.Content;
                    answear3GridanswerLabel3.Content = answerLabel3.Content;
                    answear3GridanswerLabel4.Content = answerLabel4.Content;
                    answear3Grid.Visibility = Visibility.Visible;

                    if (isNormalResolution)
                    {
                        answear3GridanswerLabel3.FontSize = 40;
                        answear3Gridindex3.FontSize = 40;
                    }

                    else
                    {
                        answear3GridanswerLabel3.FontSize = 21;
                        answear3Gridindex3.FontSize = 21;

                    }
                  

                    break;
                case 4:
                    imageB.ImageSource = imgc4.Source;
                    answear4GridanswerLabel1.Content = answerLabel1.Content;
                    answear4GridanswerLabel2.Content = answerLabel2.Content;
                    answear4GridanswerLabel3.Content = answerLabel3.Content;
                    answear4GridanswerLabel4.Content = answerLabel4.Content;
                    answear4Grid.Visibility = Visibility.Visible;

                    if (isNormalResolution)
                    {
                        answear4GridanswerLabel4.FontSize = 40;
                        answear4Gridindex4.FontSize = 40;
                    }

                    else
                    {
                        answear4GridanswerLabel4.FontSize = 21;
                        answear4Gridindex4.FontSize = 21;

                    }

                  
                    break;
            }

            answerSound.Play();

            answerSound.MediaEnded += new EventHandler(Media_EndedAnswerSound);

            playerLabel.Visibility = Visibility.Hidden;
            ans.Visibility = Visibility.Hidden;
            questionTimeLabel.Visibility = Visibility.Hidden;




        }




        //***************************************************************************************************
        //<--

        private void Media_EndedAnswerSound(object sender, EventArgs e)

        {

            answerSound.Stop();
            //לאחר סיום הצגת תשובה נראה תוצאות או נעבור לשאלה הבאה
            int q = Int32.Parse(numberQ.Content.ToString());
            initTriviaScreen();
            if (q % 5 == 0)
            {
                ShowCurrentResult();//נראה מסך תוצאות ביניים

            }
            else
            {
                LoadQuestion(++questionIndex);
                this.KeyDown += new KeyEventHandler(KeyDownEvent);
            }
        }
        //***************************************************************************************************
        //-->
        private void ShowCurrentResult()
        {
            inQ.Visibility = Visibility.Hidden;
            beforeQ.Visibility = Visibility.Hidden;
            afterQ.Visibility = Visibility.Visible;
            imageB.ImageSource = imgAfterQ.Source;

            this.KeyDown -= new KeyEventHandler(KeyDownEvent);
            LoadSound.player13.Stop();
            LoadSound.player14.Play();
            LoadSound.player14.MediaEnded += new EventHandler(finishShowResultList);



            var resultSort = scores.OrderByDescending(pair => pair.Value).Take(scores.Count).ToDictionary(pair => pair.Key, pair => pair.Value);

              

            //ansCon.Text = st.ToString();
            StringBuilder st1 = new StringBuilder();
            StringBuilder st2 = new StringBuilder();

            st1 = new StringBuilder();
            for (int i = 0; i < resultSort.Count; i++)
            {
                st1.Append(resultSort.Keys.ElementAt(i) + '\n');
                //st.Append(players[i].ToString() + ": " + scorePlayers[i] + " נקודות" + '\n');
            }
            st1.Append('\n');
            dg.Content = st1.ToString();

            st2 = new StringBuilder();
            for (int i = 0; i < resultSort.Count; i++)
            {
                st2.Append(resultSort.Values.ElementAt(i));
                st2.Append('\n');
                //st.Append(players[i].ToString() + ": " + scorePlayers[i] + " נקודות" + '\n');
            }
            st2.Append('\n');
            dgScore.Content = st2.ToString();



            int total = 0;
            foreach (var p in resultSort)
                total += p.Value;
            ts.Content = total + "  " + "נקודות";




        }

        //***************************************************************************************************
        //<--
        private void finishShowResultList(object sender, EventArgs e)
        {
            LoadSound.player14.Stop();
            LoadQuestion(++questionIndex);
            this.KeyDown += new KeyEventHandler(KeyDownEvent);
            afterQ.Visibility = Visibility.Hidden;
            imageB.ImageSource = imgN.Source;

        }


        //***************************************************************************************************
        //פונקציה המעדכנת מיקומי אוביקטים במסך הטריוויה אחרי שהוזזו לצורך הצגת התשובה הנכונה
        private void initTriviaScreen()
        {


            //כדי להוריד משהו למטה צריך להעלות את הפרמטר השני ולהוריד בהתאם ברביעי
            imageB.ImageSource = imgN.Source;
            answearRegularGrid.Visibility = Visibility.Visible;
            answear1Grid.Visibility = Visibility.Hidden;
            answear2Grid.Visibility = Visibility.Hidden;
            answear3Grid.Visibility = Visibility.Hidden;
            answear4Grid.Visibility = Visibility.Hidden;
           
            int f = isNormalResolution ? 30 : 18;

            answerLabel1.FontSize = f;
            answerLabel2.FontSize = f;
            answerLabel3.FontSize = f;
            answerLabel4.FontSize = f;

            index1.FontSize = f;
            index2.FontSize = f;
            index3.FontSize = f;
            index4.FontSize = f;



            playerLabel.Visibility = Visibility.Visible;
            ans.Visibility = Visibility.Visible;
            questionTimeLabel.Visibility = Visibility.Visible;


        }



        //***************************************************************************************************
        //פונקציה המחזירה תוכן תא ספציפי בקובץ אקסל שהמכיל את השאלון

        private string GetCellValue(int col, int row)
        {
            //מחזיר תא בקובץ השאלות
            if (worksheet.Cells[row, col].Value == null)
            {
                return "";
            }

            return worksheet.Cells[row, col].Value.ToString();
        }
        //***************************************************************************************************


        //פונקציה המעדכנת את רשימת המובילים בדף הטריוויה

        private void CalculateScore()
        {
            
            

                for (int i=0; i < scores.Count;i++)
            {
                list[i] = scores.ElementAt(i).Value;
            }
            //מחשב את הניקוד הכללי וניקוד שלושת המובילים
            totalScoreLabel.Content = totalScore;

            var dictSort = from objDict in scores orderby objDict.Value descending select objDict;

            switch (scores.Count)
            {
                case 0:
                    break;
                case 1:
                    {
                        top1.Content = dictSort.ElementAt(0).Key;
                        top1S.Content = dictSort.ElementAt(0).Value;

                        break;
                    }
                case 2:
                    {
                        top1.Content = dictSort.ElementAt(0).Key;
                        top1S.Content = dictSort.ElementAt(0).Value;

                        top2.Content = dictSort.ElementAt(1).Key;
                        top2S.Content = dictSort.ElementAt(1).Value;
                        break;
                    }

                case 3:
                    {
                        top1.Content = dictSort.ElementAt(0).Key;
                        top1S.Content = dictSort.ElementAt(0).Value;

                        top2.Content = dictSort.ElementAt(1).Key;
                        top2S.Content = dictSort.ElementAt(1).Value;

                        top3.Content = dictSort.ElementAt(2).Key;
                        top3S.Content = dictSort.ElementAt(2).Value;

                        break;

                    }
                default: //more than 3 players
                    top1.Content = dictSort.ElementAt(0).Key;
                    top1S.Content = dictSort.ElementAt(0).Value;

                    top2.Content = dictSort.ElementAt(1).Key;
                    top2S.Content = dictSort.ElementAt(1).Value;

                    top3.Content = dictSort.ElementAt(2).Key;
                    top3S.Content = dictSort.ElementAt(2).Value;

                    break;

            }


        }


        //***************************************************************************************************
        //פונקציה המחזירה את האינדקס של תשובה נכונה בשאלה נוכחית

        public int CheckIndexOfCorrectAnswer()
        {
            int index = 0;


            if (answerLabel1.Content.ToString() == correctAnswer)
                index = 1;
            else if (answerLabel2.Content.ToString() == correctAnswer)
                index = 2;
            else if (answerLabel3.Content.ToString() == correctAnswer)
                index = 3;
            else if (answerLabel4.Content.ToString() == correctAnswer)
                return 4;

            return index;
        }
        //***************************************************************************************************
        //פונקציה שמחזירה את השחקן שתורו כרגע לשחק
        private string GetCurrentPlayer()
        {
           
            //מספר התשובות שנענו עד כה ביחס למספר המשתתפים יתן לי את מספר השחקן הנוכחי
            var index = totalAnswers % scores.Count;
           
            return scores.Keys.ElementAt(index);
        }
        //***************************************************************************************************


        //פונקציה המחשבת ומחזירה את תוכן התשובה של אותו כפתור שנלחץ


        private string GetAnswer(int index)
        {
            string answer = "";

            switch (index)
            {
                case 1:
                    answer = (string)answerLabel1.Content;
                    break;
                case 2:
                    answer = (string)answerLabel2.Content;
                    break;
                case 3:
                    answer = (string)answerLabel3.Content;
                    break;
                case 4:
                    answer = (string)answerLabel4.Content;
                    break;

            }

            return answer;

        }


        //***************************************************************************************************


        private void StartScoreboardScreen()
        {
            StopSound();

            Ans.PlayersNum = scores.Count;

            Ans.Time = totalGameTimeLabel.Content.ToString();
            Ans.PlayersNum = scores.Count;
            Ans.Points = totalScore;

            Ans.playersResult = scores;

            //מוספק הטיימר ועוברים למסך תוצאות
            timer.Stop();
            clock.Stop();



          
            //הדרך הטובה למעבר בין מסכים
            ScoreboardWindow w = new ScoreboardWindow();

            this.KeyDown -= KeyDownEvent;

            //w.InitializeComponent();

            
            w.Show();
            this.Close();

        }
        //***************************************************************************************************



        

        private void StopSound()
        {

            LoadSound.player3.Stop();
            clickSound.Close();
            answerSound.Close();

            LoadSound.player2.Stop();



            LoadSound.player14.Stop();




        }


        //***************************************************************************************************


        private void Media_Ended2(object sender, EventArgs e)
        {
            LoadSound.player2.Position = TimeSpan.Zero;
            LoadSound.player2.Play();
        }
        //***************************************************************************************************

        private void closePageClick(object sender, RoutedEventArgs e)
        {

            StopSound();
            clickSound.Play();

            clickSound.MediaEnded += new EventHandler(Media_Ended16);



            ExitScreen w = new ExitScreen();

            w.InitializeComponent();
            w.ShowDialog();
        }



        //***************************************************************************************************

        private void HandleEsc(object sender, KeyEventArgs e)
        {


            if (e.Key == Key.Escape)
            {

                this.WindowState = WindowState.Minimized;
            }

        }
        //***************************************************************************************************


        private void leaveX(object sender, MouseEventArgs e)
        {
            XButton.ImageSource = normalX.Source;

        }
        //***************************************************************************************************

        private void enterX(object sender, MouseEventArgs e)
        {
            XButton.ImageSource = changeX.Source;

        }



        //***************************************************************************************************


    }
}
