using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMD_Run.Engine
{

    public delegate void GameCycleStartedHandler(object sender, EventArgs e);
    public delegate void GameCycleFinishedHandler(object sender, EventArgs e);
    public delegate void GameStartedHandler(object sender, EventArgs e);
    public delegate void GameStoppedHandler(object sender, EventArgs e);
}
