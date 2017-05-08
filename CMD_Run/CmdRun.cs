using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMD_Run.Engine;

namespace CMD_Run
{
    public class CmdRun
    {
        [STAThread()]
        private static void Main(string[] args)
        {
            GameTimer = new GameTimer();
            GameTimer.Start();

            InputListener = new InputListener();
            InputListener.Start();
            InputListener.Stop();
        }
        
        /// <summary>
        /// Timer für die Spielschleife
        /// </summary>
        public static GameTimer GameTimer { get; private set; }
        
        /// <summary>
        /// Listener für Tastatureingaben
        /// </summary>
        public static InputListener InputListener { get; private set; }
    }
}
