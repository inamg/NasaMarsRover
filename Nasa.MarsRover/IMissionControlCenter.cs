using System.Collections.Generic;

namespace Nasa.MarsRover
{
    public interface IMissionControlCenter
    {
        IEnumerable<IRover> ExecuteCommand(string commandStrings);
    }
}
