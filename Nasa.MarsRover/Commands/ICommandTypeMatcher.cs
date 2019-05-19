using System.Data;

namespace Nasa.MarsRover.Commands
{
    public interface ICommandTypeMatcher
    {
        CommandType GetCommandType(string command);
    }
}
