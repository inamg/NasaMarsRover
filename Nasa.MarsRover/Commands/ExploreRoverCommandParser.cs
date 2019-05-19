using System;
using System.Drawing;
using System.Linq;
using Nasa.MarsRover.Exceptions;
using Nasa.MarsRover.Validators;

namespace Nasa.MarsRover.Commands
{
    /// <summary>
    /// Parses the string into ExploreRoverCommand
    /// </summary>
    public class ExploreRoverCommandParser : ICommandParser
    {
        public ICommand Parse(string commandString)
        {
            Check.NotNullOrEmpty(commandString, nameof(commandString));

            //Only adding try catch because class is public. so there is no guarantee commandString will be correct
            try
            {
                var arguments = commandString.ToCharArray();

                var movements = arguments.Select(GetMovements).ToList();
                return new ExploreRoverCommand(movements);
            }
            catch (Exception exp)
            {
                throw new CommandParseException($"Invalid command {commandString} for command type ExploreRoverCommand.");
            }

        }

        private static Movement GetMovements(char movement)
        {
            switch (movement)
            {
                case 'L':
                    return Movement.Left;
                case 'R':
                    return Movement.Right;
                case 'M':
                    return Movement.Forward;
                default:
                    throw new CommandParseException($"Invalid character for movement - {movement}");
            }
        }
    }
}
