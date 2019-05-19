using System;

namespace Nasa.MarsRover.Exceptions
{
    public class CommandParseException : Exception
    {
        public CommandParseException(string message) : base(message)
        {

        }
    }
}
