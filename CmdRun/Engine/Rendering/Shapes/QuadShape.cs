using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace CmdRun.Engine.Rendering.Shapes
{
    public class QuadShape : IShape
    {
        public Vertex[] Verticies { get; private set; }

        //TODO: nach Tests entfernen!
        private static Random rnd = new Random();

        public QuadShape(double sideLength)
        {
            if (sideLength <= 0)
            {
                sideLength = 1;
            }
            sideLength /= 2;

            KnownColor[] colors = Enum.GetValues(typeof(KnownColor)).OfType<KnownColor>().ToArray();

            Verticies = new Vertex[]
            {
                new Vertex() { X = -sideLength, Y = -sideLength, Color = Color.FromKnownColor(colors[rnd.Next(colors.Length)]) },
                new Vertex() { X =  sideLength, Y = -sideLength, Color = Color.FromKnownColor(colors[rnd.Next(colors.Length)]) },
                new Vertex() { X =  sideLength, Y =  sideLength, Color = Color.FromKnownColor(colors[rnd.Next(colors.Length)]) },
                new Vertex() { X = -sideLength, Y =  sideLength, Color = Color.FromKnownColor(colors[rnd.Next(colors.Length)]) },
            };
        }

        public void Draw(Vector2d currentLocation, Quaterniond currentRotation)
        {
            GL.Begin(PrimitiveType.Quads);
            foreach(Vertex vertex in Verticies)
            {
                GL.Color3(vertex.Color);
                GL.Vertex2(currentLocation.X + vertex.X, currentLocation.Y + vertex.Y);
            }
            GL.End();
            CmdRun.Logger.Log("Quadshape drawn onto AppWindow", VerbosityLevel.Debug);
        }
    }
}
