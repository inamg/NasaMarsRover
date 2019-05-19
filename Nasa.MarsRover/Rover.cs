using System;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.Extensions.Logging;
using Nasa.MarsRover.Exceptions;
using Nasa.MarsRover.Validators;

namespace Nasa.MarsRover
{
    /// <summary>
    /// Represents a rover exposing present Position and Direction of the rover
    /// </summary>
    public class Rover : IRover
    {
        public Point Position { get; private set; }

        public Direction Direction { get; private set; }

        private ILandingPlateau _plateau;

        private readonly IDictionary<Movement, Action> _movementActions;

        private readonly IDictionary<Direction, Action> _leftMoveActions;

        private readonly IDictionary<Direction, Action> _rightMoveActions;

        private readonly IDictionary<Direction, Action> _forwardMoveActions;

        public Rover()
        {
            _movementActions = new Dictionary<Movement, Action>
            {
                {Movement.Left, () => _leftMoveActions[Direction].Invoke()},
                {Movement.Right, () => _rightMoveActions[Direction].Invoke()},
                {Movement.Forward, () => _forwardMoveActions[Direction].Invoke()}
            };

            _leftMoveActions = new Dictionary<Direction, Action>
            {
                {Direction.North, () => Direction = Direction.West},
                {Direction.East, () => Direction = Direction.North},
                {Direction.South, () => Direction = Direction.East},
                {Direction.West, () => Direction = Direction.South}
            };

            _rightMoveActions = new Dictionary<Direction, Action>
            {
                {Direction.North, () => Direction = Direction.East},
                {Direction.East, () => Direction = Direction.South},
                {Direction.South, () => Direction = Direction.West},
                {Direction.West, () => Direction = Direction.North}
            };

            _forwardMoveActions = new Dictionary<Direction, Action>
            {
                {Direction.North, ForwardNorth},
                {Direction.South, ForwardSouth},
                {Direction.East, ForwardEast},
                {Direction.West, ForwardWest}
            };
        }
        /// <summary>
        /// Deploys rover on passed position and direction.If deployment not possible throws exception
        /// </summary>
        /// <param name="position">Position to put the rover on</param>
        /// <param name="direction">Initial direction of the rover</param>
        /// <param name="plateau">Plateau on which to deploy the rover</param>
        public void Deploy(Point position, Direction direction, ILandingPlateau plateau)
        {
            if (plateau.IsValidPoint(position))
            {
                Position = position;
                Direction = direction;
                _plateau = plateau;
            }
            else
            {
                throw new DeployRoverException(
                    $"Deployment failed for Position-{Position} and Plateau Size-{plateau.Size}");
            }
        }

        /// <summary>
        /// Moves the Rover based on the list of movements passed
        /// </summary>
        /// <param name="movements">List of movements to be allowed</param>
        public void Move(IReadOnlyList<Movement> movements)
        {
            Check.NotNull(movements, nameof(movements));

            foreach (var movement in movements)
            {
                _movementActions[movement].Invoke();
            }
        }

        private void ForwardNorth()
        {
            var newPosition = new Point(Position.X, Position.Y + 1);

            if (_plateau.IsValidPoint(newPosition))
            {
                Position = newPosition;
            }
            else
            {
                throw new ExploreRoverException($"Can't move Rover. Invalid new co-ordinates -{newPosition}");
            }
        }

        private void ForwardSouth()
        {
            var newPosition = new Point(Position.X, Position.Y - 1);

            if (_plateau.IsValidPoint(newPosition))
            {
                Position = newPosition;
            }
            else
            {
                throw new ExploreRoverException($"Can't move Rover. Invalid new co-ordinates -{newPosition}");
            }
        }

        private void ForwardEast()
        {
            var newPosition = new Point(Position.X + 1, Position.Y);

            if (_plateau.IsValidPoint(newPosition))
            {
                Position = newPosition;
            }
            else
            {
                throw new ExploreRoverException($"Can't move Rover. Invalid new co-ordinates -{newPosition}");
            }
        }

        private void ForwardWest()
        {
            var newPosition = new Point(Position.X - 1, Position.Y);

            if (_plateau.IsValidPoint(newPosition))
            {
                Position = newPosition;
            }
            else
            {
                throw new ExploreRoverException($"Can't move Rover. Invalid new co-ordinates -{newPosition}");
            }
        }
    }
}
