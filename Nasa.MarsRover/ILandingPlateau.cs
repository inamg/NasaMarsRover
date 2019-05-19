using System.Drawing;

namespace Nasa.MarsRover
{
    public interface ILandingPlateau
    {
        Size Size { get; }
        void SetSize(Size size);
        bool IsValidPoint(Point position);
    }
}
