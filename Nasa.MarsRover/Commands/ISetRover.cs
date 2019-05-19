namespace Nasa.MarsRover.Commands
{
    public interface ISetRover : ICommand
    {
        void SetRover(IRover rover);
    }
}
