using System;
using System.Collections.Generic;
using Nasa.MarsRover.Commands;
using Nasa.MarsRover.Validators;

namespace Nasa.MarsRover.IO
{
    /// <summary>
    /// Parses the input command
    /// </summary>
    public class InputCommandParser : IInputParser
    {
        private readonly ICommandTypeMatcher _commandTypeMatcher;

        private readonly IDictionary<CommandType, ICommandParser> _commandParsers;

        public InputCommandParser(ICommandTypeMatcher commandTypeMatcher, IServiceProvider serviceProvider)
        {
            Check.NotNull(commandTypeMatcher, nameof(commandTypeMatcher));
            Check.NotNull(serviceProvider, nameof(serviceProvider));

            _commandTypeMatcher = commandTypeMatcher;

            _commandParsers = new Dictionary<CommandType, ICommandParser>
            {
                {CommandType.SetupPlateau, (SetupPlateauCommandParser)serviceProvider.GetService(typeof(SetupPlateauCommandParser))},
                {CommandType.DeployRover, (DeployRoverCommandParser)serviceProvider.GetService(typeof(DeployRoverCommandParser))},
                {CommandType.ExploreRover,(ExploreRoverCommandParser)serviceProvider.GetService(typeof(ExploreRoverCommandParser))}
            };
        }

        public IReadOnlyList<ICommand> Parse(string commandString)
        {
            Check.NotNull(commandString, nameof(commandString));

            var commandsList = new List<ICommand>();
            var commands = commandString.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            foreach (var command in commands)
            {
                var commandType = _commandTypeMatcher.GetCommandType(command);

                commandsList.Add(_commandParsers[commandType].Parse(command));
            }

            return commandsList;
        }
    }
}
