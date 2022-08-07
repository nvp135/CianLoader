using System;
using System.IO;

namespace CianLogger
{
    public static class Logger
    {
        //readonly string LOG_FILE_PATH;
        /// <summary>
        /// Constructor with path setup
        /// </summary>
        /// <param name="path">Path to log folder</param>
        //public Logger(string path) => this.LOG_FILE_PATH = path;

        /// <summary>
        /// Constructor without path setup (current directory by default)
        /// </summary>
        //public Logger() : this(Path.Combine(Environment.CurrentDirectory, "log.log")) { }

        /// <summary>
        /// Write text to log file
        /// </summary>
        /// <param name="path">Path to log file</param>
        /// <param name="message">Message text</param>
        public static void WriteToLog(string path, string message)
        {
            try
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Returns all text of log file
        /// </summary>
        /// <param name="path">Path of log file</param>
        /// <returns></returns>
        //public string ReadLog(string path = "log.log")
        //{
        //    using (StreamReader sr = File.OpenText(path))
        //    {
        //        return sr.ReadToEnd();
        //    }
        //}
    }
}
