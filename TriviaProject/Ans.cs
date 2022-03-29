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
using System.Windows.Controls;
using System.Windows.Input;

namespace TriviaProject
{
    
    static class Ans
    {
        public static string isPaid = "false";
        public static string currentCodeGame;
        public static string PrivateName;
        public static string City;
        public static string PhoneNum;
        public static string Email;
        public static string Code;

        public static int PlayersNum;
        public static int Points;
        public static string Time;
        public static Dictionary<string, int> playersResult;
        public static string Type;
        public static string sentResult;






        public static void Save(string name,
            string city,
            string phone,
            string code,
            string mail,
            int pNum,
            int points,
            string time,
            Dictionary<string, int> players)
        {
            PrivateName = name;
            City = city;
            PhoneNum = phone;
            Code = code;
            Email = mail;
            PlayersNum = pNum;
            Points = points;
            Time = time;
            playersResult = players;
        }

       
    }
}
