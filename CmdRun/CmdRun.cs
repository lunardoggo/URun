using System.IO;
using OpenTK;
using CmdRun.Engine.Rendering;
using CmdRun.Engine;

namespace CmdRun
{
    public class CmdRun
    {
        private static void Main(string[] args)
        {
            Logger = new Logger(Path.Combine(Path.GetTempPath(), "CmdRun"));
#if DEBUG   
            Logger.CurrentVerbosityLevel = VerbosityLevel.Debug;
#endif

            Window = new AppWindow(800, 600);

            Window.Run(1, 1);

            Logger.Log("Application successfully exited", VerbosityLevel.ImportantInfo);
        }

        public static Logger Logger { get; private set; }
        public static GameWindow Window { get; private set; }
    }
}
