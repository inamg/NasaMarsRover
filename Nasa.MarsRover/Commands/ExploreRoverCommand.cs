using System;
using System.Collections.Generic;
using Nasa.MarsRover.Exceptions;
using Nasa.MarsRover.Validators;

namespace Nasa.MarsRover.Commands
{
    /// <summary>
    /// Represents ExploreRoverCommand
    /// </summary>
    public class ExploreRoverCommand : ISetRover
    {
        public IReadOnlyList<Movement> Movements { get; }

        private IRover _rover;

        public ExploreRoverCommand(IReadOnlyList<Movement> movements)
        {
            Movements = movements ?? throw new ArgumentNullException(nameof(movements));
        }

        public void Execute()
        {
            if (_rover == null)
            {
                throw new ExploreRoverException($"Set Rover-{_rover} properly");
            }

            _rover.Move(Movements);
        }

        public void SetRover(IRover rover)
        {
            Check.NotNull(rover, nameof(rover));

            _rover = rover;
        }
    }
}
