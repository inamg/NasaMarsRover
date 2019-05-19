using System;
using System.Drawing;
using Nasa.MarsRover.Exceptions;
using Nasa.MarsRover.Validators;

namespace Nasa.MarsRover.Commands
{
    public class SetupPlateauCommandParser : ICommandParser
    {
        public ICommand Parse(string commandString)
        {
            Check.NotNullOrEmpty(commandString, nameof(commandString));

            //Only adding try catch because class is public. so there is no guarantee commandString will be correct
            try
            {
                var arguments = commandString.Split(' ');
                var width = int.Parse(arguments[0]);
                var height = int.Parse(arguments[1]);
                var size = new Size(width, height);

                return new SetupPlateauCommand(size);
            }
            catch (Exception exp)
            {
                throw new CommandParseException($"Invalid command {commandString} for command type SetupPlateauCommand.");
            }
            
        }
    }
}
