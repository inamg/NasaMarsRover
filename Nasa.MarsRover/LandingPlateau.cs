using System.Drawing;
using Nasa.MarsRover.Validators;

namespace Nasa.MarsRover
{
    public class LandingPlateau : ILandingPlateau
    {
        public Size Size { get; private set; }

        public void SetSize(Size size)
        {
            Check.NotNull(size, nameof(size));
            Size = size;
        }

        public bool IsValidPoint(Point point)
        {
            var isValidX = point.X >= 0 && point.X <= Size.Width;
            var isValidY = point.Y >= 0 && point.Y <= Size.Height;

            return isValidX && isValidY;
        }
    }
}
