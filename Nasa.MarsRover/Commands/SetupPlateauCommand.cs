using System;
using System.Drawing;
using Nasa.MarsRover.Validators;

namespace Nasa.MarsRover.Commands
{
    public class SetupPlateauCommand : ISetPlateau
    {
        private readonly Size _size;
        private ILandingPlateau _plateau;

        public SetupPlateauCommand(Size size)
        {
            Check.NotNull(size, nameof(size));

            if (size.Height <= 0 || size.Width <= 0)
            {
                throw new ArgumentException($"Invalid value for height {size.Height} or Width {size.Width}");
            }

            _size = size;
        }

        public void SetPlateau(ILandingPlateau plateau)
        {
            Check.NotNull(plateau, nameof(plateau));

            _plateau = plateau;
        }

        public void Execute()
        {
            _plateau.SetSize(_size);
        }
    }
}
