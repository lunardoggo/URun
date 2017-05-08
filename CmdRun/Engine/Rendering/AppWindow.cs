using OpenTK;
using System;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmdRun.Engine.Rendering
{
    public class AppWindow : GameWindow
    {

        public AppWindow(int width, int height) : base (width, height) { }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            CmdRun.Logger.Log("Started new rendering cycle", VerbosityLevel.Debug);
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(Color.DarkGray);

            //Tests -> später mit Liste von zu rendernden IShapes!
            new Shapes.QuadShape(0.5).Draw(Vector2d.Zero, Quaterniond.Identity);

            this.SwapBuffers();
            CmdRun.Logger.Log("Rendering cycle finished", VerbosityLevel.Debug);
        }
    }
}
