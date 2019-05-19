using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Nasa.MarsRover.Commands;
using Nasa.MarsRover.IO;
using Nasa.MarsRover.Validators;

namespace Nasa.MarsRover
{
    public class MissionControlCenter : IMissionControlCenter
    {
        private readonly IInputParser _commandParser;

        private readonly IList<IRover> _rovers;

        private readonly ILandingPlateau _plateau;

        private readonly IOutputComposer _outputComposer;
        public MissionControlCenter(IInputParser commandParser, 
            ILandingPlateau landingPlateau)
        {
            Check.NotNull(commandParser, nameof(commandParser));
            Check.NotNull(landingPlateau, nameof(landingPlateau));
            _commandParser = commandParser;

            _rovers = new List<IRover>();
            _plateau = landingPlateau;
        }

        public IEnumerable<IRover> ExecuteCommand(string commandStrings)
        {
            Check.NotNullOrEmpty(commandStrings, nameof(commandStrings));

            var commands = _commandParser.Parse(commandStrings);

            foreach (var command in commands)
            {
                switch (command)
                {
                    case SetupPlateauCommand plateauCommand:
                        plateauCommand.SetPlateau(_plateau);
                        break;
                    case DeployRoverCommand roverCommand:
                        var rover = new Rover();
                        _rovers.Add(rover);
                        roverCommand.SetRover(rover);
                        roverCommand.SetPlateau(_plateau);
                        break;
                    case ExploreRoverCommand roverCommand:
                        roverCommand.SetRover(_rovers.Last());
                        break;
                }

                command.Execute();
            }

            return _rovers;
        }
    }
}
