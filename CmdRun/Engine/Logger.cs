using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CmdRun.Engine
{
    public class Logger
    {
        /// <summary>
        /// Gibt die aktuelle Ausführlichkeit der Logausgabe zurück oder legt diese fest
        /// </summary>
        public VerbosityLevel CurrentVerbosityLevel { get; set; } = VerbosityLevel.Info;

        /// <summary>
        /// Gibt den Pfad zum aktuellem Log-File zurück
        /// </summary>
        private string filePath = string.Empty;
        /// <summary>
        /// Gibt die Anzahl der Logs vorheriger Programmdurchläufe zurück
        /// </summary>
        private const int LegacyLogCount = 10;

        public Logger(string dirPath)
        {
            if(!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            filePath = Path.Combine(dirPath, $"CmdRun_{DateTime.Now.ToString("dd.MM.yyyy_HH.mm")}.log");
            if(!File.Exists(filePath))
            {
                //Datei erstellen und übergebenen FileStream wieder Schliessen, um StreamWriter in Log(string, VerbosityLevel) nicht zu behindern!
                File.Create(filePath).Close();
            }
            IEnumerable<string> logs = Directory.GetFiles(dirPath).OrderByDescending(_file => File.GetCreationTime(_file));
            for(int i = logs.Count(); i > LegacyLogCount; i--)
            {
                File.Delete(logs.ElementAt(i));
            }
            Log("Logger initialized", VerbosityLevel.ImportantInfo);
        }

        /// <summary>
        /// Schreibt den angegebenen content in die Logdatei, wenn das <see cref="VerbosityLevel"/> des Loggers größer oder gleich dem vorausgesetzten Mindest-<see cref="VerbosityLevel"/> ist
        /// </summary>
        public void Log(string content, VerbosityLevel verbosityLevel)
        {
            if(CurrentVerbosityLevel >= verbosityLevel && verbosityLevel > 0)
            {
                content = $"[{DateTime.Now.ToString("HH:mm:ss")}]: {content}";
                StreamWriter writer = File.AppendText(filePath);
                writer.WriteLine(content, Encoding.UTF8);
                writer.Close();
                Console.WriteLine(content);
            }
        }

        /// <summary>
        /// Schreibt eine Fehlermeldung in die Logdatei und gibt zudem an, ob das Programm beendet wird
        /// </summary>
        /// <param name="ex">Aufgetretener Fehler</param>
        /// <param name="wasFatal">Gibt an, ob der Fehler so schwerwiegend war, dass die Anwendung beendet werden muss</param>
        /// <param name="isTerminating"></param>
        public void Log(Exception ex, bool wasFatal)
        {
            Log(ex.ToString(), wasFatal ? VerbosityLevel.FatalError : VerbosityLevel.RegularError);
            if(wasFatal)
            {
                Log($"CmdRun is terminating due to fatal Application Error: {Environment.ExitCode}", VerbosityLevel.ImportantInfo);
            }
        }
    }

    public enum VerbosityLevel : byte
    {
        None = 0,
        ImportantInfo = 1,
        FatalError = 2,
        RegularError = 3,
        Warning = 4,
        Info = 5,
        Debug = 6
    }
}
