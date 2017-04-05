using System;
using System.Threading;

namespace CMD_Run.Engine
{
    public class GameTime
    {
        private int cycleLength = 300;

        /// <summary>
        /// Wird aufgerufen, wenn der aktuelle Spieldurchlauf beendet ist
        /// </summary>
        public event GameCycleFinishedHandler OnGameCycleFinished;
        /// <summary>
        /// Wird aufgerufen, wenn der aktuelle Spieldurchlauf beendet ist
        /// </summary>
        public event GameCycleStartedHandler OnGameCycleStarted;
        /// <summary>
        /// Wird aufgerufen, wenn die Spielschleife gestartet wird
        /// </summary>
        public event GameStartedHandler OnGameStarted;
        /// <summary>
        /// Wird aufgerufen, wenn die Spielschleife gestoppt wird
        /// </summary>
        public event GameStoppedHandler OnGameStopped;

        /// <summary>
        /// Länge der Sleep-Dauer jedes Spieldurchlaufs in Milliseknuden
        /// </summary>
        public int GameCycleLength
        {
            get { return cycleLength; }
            set
            {
                if(cycleLength > 0)
                {
                    cycleLength = value;
                }
            }
        }

        /// <summary>
        /// Gibt an, ob die Spielschleife nochmals ausgeführt werden soll
        /// </summary>
        public bool RunGameCycle { get; set; }

        /// <summary>
        /// Startet den Timer für die Spielschleife
        /// </summary>
        public void Start()
        {
            RunGameCycle = true;
            OnGameStarted?.Invoke(this, new EventArgs());
            while(RunGameCycle)
            {
                OnGameCycleStarted?.Invoke(this, new EventArgs());
                Thread.Sleep(cycleLength);
                OnGameCycleFinished?.Invoke(this, new EventArgs());
            }
            OnGameStopped?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Lässt den letzten Spieldurchlauf durchlaufen und beendet die Spielschleife
        /// </summary>
        public void Stop()
        {
            RunGameCycle = false;
        }
    }
}
