using System.Drawing;
using Nasa.MarsRover.Exceptions;
using Nasa.MarsRover.Validators;

namespace Nasa.MarsRover.Commands
{
    /// <summary>
    /// Represents DeployRoverCommand
    /// </summary>
    public class DeployRoverCommand : ISetRover, ISetPlateau
    {
        private readonly Point _position;

        private readonly Direction _direction;

        private IRover _rover;

        private ILandingPlateau _plateau;

        public DeployRoverCommand(Point position, Direction direction)
        {
            Check.NotNull(position, nameof(position));
            Check.NotNull(direction, nameof(direction));

            _position = position;
            _direction = direction;
        }

        public void Execute()
        {
            if (_rover == null || _plateau == null)
            {
                throw new DeployRoverException($"Set Rover-{_rover} and Plateau-{_plateau} properly");
            }

            _rover.Deploy(_position, _direction, _plateau);
        }

        public void SetRover(IRover rover)
        {
            Check.NotNull(rover, nameof(rover));

            _rover = rover;
        }

        public void SetPlateau(ILandingPlateau plateau)
        {
            Check.NotNull(plateau, nameof(plateau));

            _plateau = plateau;
        }
    }
}
