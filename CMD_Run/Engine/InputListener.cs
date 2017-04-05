using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMD_Run.Engine
{
    public class InputListener
    {
        /// <summary>
        /// Wird aufgerufen, wenn ein Tastendruck erkannt wird
        /// </summary>
        public EventHandler<KeyTypedEventArgs> OnKeyTyped;

        private bool listen = false;

        /// <summary>
        /// Startet das Suchen nach Tastatureingaben
        /// </summary>
        public void Start()
        {
            listen = true;
            while(listen)
            {
                OnKeyTyped?.Invoke(this, new KeyTypedEventArgs(Console.ReadKey(true)));
            }
        }

        /// <summary>
        /// Wartet auf die letzte Tastatureingabe und beendet dann das Suchen nach Tastendrücken
        /// </summary>
        public void Stop()
        {
            listen = false;
        }
    }

    public class KeyTypedEventArgs : EventArgs
    {
        /// <summary>
        /// Für einen EventHandler, der beim Tastendruck ausgelöst wird
        /// </summary>
        /// <param name="keyInfo"></param>
        public KeyTypedEventArgs(ConsoleKeyInfo keyInfo)
        {
            ConsoleKeyInfo = keyInfo;
        }

        /// <summary>
        /// Information über den geschriebenen Key
        /// </summary>
        public ConsoleKeyInfo ConsoleKeyInfo { get; private set; }
    }
}
