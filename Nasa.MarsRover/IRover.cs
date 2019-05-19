using System.Collections.Generic;
using System.Drawing;

namespace Nasa.MarsRover
{
    public interface IRover
    {
        void Deploy(Point position, Direction direction, ILandingPlateau plateau);
        void Move(IReadOnlyList<Movement> movements);
        Point Position { get; }
        Direction Direction { get; }
    }
}
