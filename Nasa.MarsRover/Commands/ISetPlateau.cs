namespace Nasa.MarsRover.Commands
{
    public interface ISetPlateau : ICommand
    {
        void SetPlateau(ILandingPlateau plateau);
    }
}
