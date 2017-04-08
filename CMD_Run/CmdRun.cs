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
            InputListener = new InputListener();
            InputListener.OnKeyDown += (object sender, KeyTypedEventArgs e) =>
            {
                Console.WriteLine("hit " + e.KeyCode);
            };
            InputListener.Start();
            InputListener.Run();
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
