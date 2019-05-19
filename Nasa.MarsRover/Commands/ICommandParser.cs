using System.Collections.Generic;

namespace Nasa.MarsRover.Commands
{
    public interface ICommandParser
    {
        ICommand Parse(string commandString);
    }
}
