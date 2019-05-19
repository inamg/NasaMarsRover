using System.Collections.Generic;
using Nasa.MarsRover.Commands;

namespace Nasa.MarsRover.IO
{
    public interface IInputParser
    {
        IReadOnlyList<ICommand> Parse(string inputString);
    }
}
