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
        private static GameTime timer = null;

        private static void Main(string[] args)
        {
            /*
            int width = 4096;
            int height = 10;
            Random rnd = new Random();

            string[,] array = new string[width, height];
            for(int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (y == height - 1)
                    {
                        array[x, y] = "█";
                    }
                    else
                    {
                        array[x, y] = rnd.Next(0, 9) > 7 ? "x" : null;
                    }
                }
            }

            int currentIndex = 0;
            int bufferWidth = Console.BufferWidth;
            while(currentIndex < width - bufferWidth)
            {
                Console.Clear();
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < bufferWidth; x++)
                    {
                        if(!string.IsNullOrEmpty(array[x + currentIndex, y]))
                        {
                            Console.SetCursorPosition(x, y + 16);
                            Console.Write(array[x + currentIndex, y]);
                        }
                    }
                }
                System.Threading.Thread.Sleep(100);
                currentIndex++;
            }
            */
        }

        /// <summary>
        /// Started die Spielschleife
        /// </summary>
        public static void StartGame()
        {
            timer.Start();
        }

        /// <summary>
        /// Stoppt die Spielschleife
        /// </summary>
        public static void StopGame()
        {
            timer.Stop();
        }
    }
}
