namespace Nasa.MarsRover.Commands
{
    public interface ICommand
    {
        void Execute();
    }

    public interface ISetRover : ICommand
    {
        void SetRover(IRover rover);
    }

    public interface ISetPlateau : ICommand
    {
        void SetPlateau(ILandingPlateau plateau);
    }
}