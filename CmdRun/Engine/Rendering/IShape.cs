using OpenTK;
using System.Drawing;

namespace CmdRun.Engine.Rendering
{
    public interface IShape
    {
        Vertex[] Verticies { get; }

        void Draw(Vector2d currentLocation, Quaterniond currentRotation);
    }

    public class Vertex
    {
        public double X { get; set; }
        public double Y { get; set; }
        public Color Color { get; set; }
    }
}
