using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Nasa.MarsRover.Exceptions;
using Nasa.MarsRover.Validators;

namespace Nasa.MarsRover.Commands
{
    /// <summary>
    /// Matches the commands based on regex.
    /// </summary>
    public class CommandTypeMatcher : ICommandTypeMatcher
    {
        private readonly IDictionary<string, CommandType> _commandTypeRegexs;

        public CommandTypeMatcher()
        {
            _commandTypeRegexs = new Dictionary<string, CommandType>
            {
                { @"^\d+ \d+$", CommandType.SetupPlateau },
                { @"^\d+ \d+ [NSEW]$", CommandType.DeployRover },
                { @"^[LRM]+$", CommandType.ExploreRover }
            };
        }

        /// <summary>
        /// Returns the matched CommandType
        /// </summary>
        /// <param name="command">Command string</param>
        /// <returns>CommandType enum</returns>
        public CommandType GetCommandType(string command)
        {
            Check.NotNullOrEmpty(command, nameof(command));

            try
            {
                var commandType = _commandTypeRegexs.First(item => new Regex(item.Key).IsMatch(command));

                return commandType.Value;
            }
            catch (InvalidOperationException exp)
            {
                throw new CommandParseException($"Invalid command format-{command}");
            }
        }
    }
}
