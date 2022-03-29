using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;

namespace TriviaProject
{
    public class ExceptionDetails
    {



        private static string logfile = "files/logfile.txt";
        
                  




        ///<summary>
        /// Decrypts a file using Rijndael algorithm.
        ///</summary>
        ///<param name="inputFile"></param>
        ///<param name="outputFile"></param>
        public static void WriteException(Exception exception)
        {
          
            try
            {
                


                if (File.Exists(logfile))
                {
                    using (var writer = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(),logfile), true))
                    {
                        writer.WriteLine(
                            "********=>{0} An Error occurred: {1}  Message: {2}{3}",
                            DateTime.Now,
                            exception.StackTrace,
                            exception.Message,
                            Environment.NewLine
                            );
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        
    }
}
