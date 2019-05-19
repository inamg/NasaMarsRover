using System.Collections.Generic;

namespace Nasa.MarsRover.IO
{
    public interface IOutputComposer
    {
        string Compose(IEnumerable<IRover> rovers);
    }
}
