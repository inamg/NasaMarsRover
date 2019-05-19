using System.Drawing;
using Nasa.MarsRover.Validators;

namespace Nasa.MarsRover
{
    /// <summary>
    /// Represents a plateau, exposing Size of the plateau
    /// </summary>
    public class LandingPlateau : ILandingPlateau
    {
        public Size Size { get; private set; }

        public void SetSize(Size size)
        {
            Check.NotNull(size, nameof(size));
            Size = size;
        }

        /// <summary>
        /// Checks if the point is valid.
        /// </summary>
        /// <param name="point">Point to be checked</param>
        /// <returns>true or false</returns>
        public bool IsValidPoint(Point point)
        {
            var isValidX = point.X >= 0 && point.X <= Size.Width;
            var isValidY = point.Y >= 0 && point.Y <= Size.Height;

            return isValidX && isValidY;
        }
    }
}
