using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NFT_recovery_grabber
{
    /// <summary>
    /// Предоставляет набор классов для записи логов.
    /// </summary>
    public class FileLogger
    {
        protected static string FileDirectory = $"{Environment.CurrentDirectory}\\Log";
        protected static string FileName = $"Log.txt";

        /// <summary>
        /// Записать сообщение в лог-файл. Файл создаётся в файле проекта по следующему пути Log\Log.txt
        /// </summary>
        /// <param name="message">Сообщение, которое будет записано</param>
        public static void Log(string message)
        {
            try
            {
                string Timestamp = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");

                if (!Directory.Exists(FileDirectory))
                {
                    Directory.CreateDirectory(FileDirectory);
                }

                using (StreamWriter streamWriter = new StreamWriter($"{FileDirectory}\\{FileName}", true))
                {
                    streamWriter.WriteLine($"@{Timestamp} || {message}");
                    streamWriter.Close();
                }
            }
            catch
            {
                Console.WriteLine($"@ Error on writing to log file");
            }
        }





    }
}
