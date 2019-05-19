using System;
using System.Drawing;
using Nasa.MarsRover.Exceptions;
using Nasa.MarsRover.Validators;

namespace Nasa.MarsRover.Commands
{
    public class DeployRoverCommandParser : ICommandParser
    {
        public ICommand Parse(string commandString)
        {
            Check.NotNullOrEmpty(commandString, nameof(commandString));

            //Only adding try catch because class is public. so there is no guarantee commandString will be correct
            try
            {
                var arguments = commandString.Split(' ');

                var x = int.Parse(arguments[0]);
                var y = int.Parse(arguments[1]);
                var position = new Point(x, y);

                return new DeployRoverCommand(position, GetDirection(arguments[2][0]));
            }
            catch (Exception exp)
            {
                throw new CommandParseException($"Invalid command {commandString} for command type DeployRoverCommand.");
            }

        }

        private static Direction GetDirection(char direction)
        {
            switch (direction)
            {
                case 'N':
                    return Direction.North;
                case 'S':
                    return Direction.South;
                case 'E':
                    return Direction.East;
                case 'W':
                    return Direction.West;
                default:
                    throw new CommandParseException($"Invalid character for direction - {direction}");
            }
        }
    }
}
