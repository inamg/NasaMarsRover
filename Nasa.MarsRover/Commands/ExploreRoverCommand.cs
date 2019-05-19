using System;
using System.Collections.Generic;
using Nasa.MarsRover.Validators;

namespace Nasa.MarsRover.Commands
{
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
            _rover.Move(Movements);
        }

        public void SetRover(IRover rover)
        {
            Check.NotNull(rover, nameof(rover));

            _rover = rover;
        }

        public void SetPlateau(ILandingPlateau plateau)
        {
            throw new NotImplementedException();
        }
    }
}
