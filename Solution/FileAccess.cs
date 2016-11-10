namespace CourseraAlgoSolution
{
    using System;
    using System.Diagnostics;
    using System.IO;

    /// <summary>
    /// Access the input and log files
    /// </summary>
    class FileAccess
    {
        /// <summary>
        /// Input file path
        /// </summary>
        private string inputDirectory = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + @"\dataFiles";
        /// <summary>
        /// Log file path
        /// </summary>
        private string logDirectory = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + @"\logs";
        /// <summary>
        /// Access files
        /// </summary>
        internal FileAccess()
        {
            Directory.CreateDirectory(logDirectory);
            StartLog();
        }
        /// <summary>
        /// Access files
        /// </summary>
        /// <param name="inputDirectory">Path for input file</param>
        internal FileAccess(string inputDirectory)
        {
            Directory.CreateDirectory(logDirectory);
            this.inputDirectory = inputDirectory;
            StartLog();
        }

        /// <summary>
        /// Reads the input file as a single string
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string ReadAsText(string fileName)
        {
            // put extension .txt (bt default), if no extension is present
            fileName += fileName.Contains(".") ? "" : ".txt";
            try
            {
                return File.ReadAllText(inputDirectory + "/" + fileName);
            }
            catch {
                LogMessage("File read error.");
                throw;
            }
        }
        /// <summary>
        /// Reads the lines in input file as different strings
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string[] ReadAsArray(string fileName)
        {
            // put extension .txt (bt default), if no extension is present
            fileName += fileName.Contains(".") ? "" : ".txt";
            try
            {
                return File.ReadAllLines(inputDirectory + "/" + fileName);
            }
            catch
            {
                LogMessage("File read error.");
                throw;
            }
        }
        private void StartLog() {
            using (StreamWriter logFile = File.AppendText(logDirectory + "/log" + DateTime.Now.ToString("_yyyy_MM_dd")))
            {   
                logFile.WriteLine(Environment.NewLine);
                logFile.WriteLine("Program run started at: " + DateTime.Now.ToString("hh:mm:ss.ffffff"));
                logFile.Write(Environment.NewLine);
            }
        }
        /// <summary>
        /// Logs text in log file
        /// </summary>
        /// <param name="message">Message to be logged</param>
        /// <returns></returns>
        public bool LogMessage(string message) {
            try
            {
                using (StreamWriter logFile = File.AppendText(logDirectory + "/log" + DateTime.Now.ToString("_yyyy_MM_dd")))
                {
                    logFile.WriteLine(DateTime.Now.ToString("hh:mm:ss.ffffff") + ": " + message);
                }
                return true;
            }
            catch (Exception) {
                return false;
            }
        }
    }
}
