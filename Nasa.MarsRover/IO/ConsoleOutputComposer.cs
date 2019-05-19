using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Nasa.MarsRover.Extensions;
using Nasa.MarsRover.Validators;

namespace Nasa.MarsRover.IO
{
    /// <summary>
    /// Formats the output for console
    /// </summary>
    public class ConsoleOutputComposer : IOutputComposer
    {
        public string Compose(IEnumerable<IRover> rovers)
        {
            Check.NotNull(rovers, nameof(rovers));

            var output = new StringBuilder();

            foreach (var rover in rovers)
            {
                output.AppendLine(Compose(rover.Position, rover.Direction));
            }

            return output.ToString();
        }

        private static string Compose(Point position, Direction direction)
        {
            return $"{position.X} {position.Y} {direction.GetDescription()}";
        }
    }
}
