using System;
using Nasa.MarsRover.Commands;
using Nasa.MarsRover.Exceptions;
using Xunit;

namespace Nasa.MarsRover.Tests.Commands
{
    public class CommandTypeMatcherTests
    {
        private readonly CommandTypeMatcher _matcher;

        public CommandTypeMatcherTests()
        {
            _matcher = new CommandTypeMatcher();
        }

        [Fact]
        public void GetCommandType_WhenNull_ShouldThrowException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => _matcher.GetCommandType(string.Empty));
        }

        [Theory]
        [InlineData("5 5", CommandType.SetupPlateau)]
        [InlineData("LMLM", CommandType.ExploreRover)]
        [InlineData("1 1 N", CommandType.DeployRover)]

        public void GetCommandType_WhenCommandMatchesRegex_ShouldReturnCommandType(string command, CommandType result)
        {
            //Act
            var commandType = _matcher.GetCommandType(command);

            //Assert
            Assert.Equal(result, commandType);
        }

        [Fact]
        public void GetCommandType_WhenInValidCommand_ShouldThrowException()
        {
            //Assert
            Assert.Throws<CommandParseException>(() => _matcher.GetCommandType("LMUJHY"));
        }
    }
}
