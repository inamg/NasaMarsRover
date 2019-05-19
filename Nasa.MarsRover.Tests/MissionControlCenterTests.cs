using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Nasa.MarsRover.Commands;
using Nasa.MarsRover.IO;
using NSubstitute;
using NSubstitute.Core;
using Xunit;

namespace Nasa.MarsRover.Tests
{
    // In real world I would write many more unit tests
    public class MissionControlCenterTests
    {
        private readonly IMissionControlCenter _missionControlCenter;
        private readonly IInputParser _parser;

        public MissionControlCenterTests()
        {
            _parser = Substitute.For<IInputParser>();
            ILandingPlateau plateau = new LandingPlateau();
            plateau.SetSize(new Size(2, 2));

            _missionControlCenter = new MissionControlCenter(_parser, plateau);
        }

        [Fact]
        public void ExecuteCommand_WhenStringCommandIsEmpty_ShouldThrowException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => _missionControlCenter.ExecuteCommand(null));
        }

        [Fact]
        public void ExecuteCommand_WhenCommandsAreValid_ShouldReturnCorrectRovers()
        {
            //Arrange
            var commands = new List<ICommand>
            {
                new SetupPlateauCommand(new Size(5, 5)),
                new DeployRoverCommand(new Point(1, 2), Direction.North),
                new ExploreRoverCommand(new List<Movement>
                {
                    Movement.Left,
                    Movement.Forward
                })
            };

            var input = new StringBuilder();

            input.AppendLine("5 5");
            input.AppendLine("2 2 N");
            input.AppendLine("LM");

            _parser.Parse(input.ToString()).Returns(commands);

            //Act
            var rovers = _missionControlCenter.ExecuteCommand(input.ToString()).ToList();

            //Assert
            Assert.NotNull(rovers);
            Assert.Single(rovers);
            Assert.Equal(Direction.West, rovers[0].Direction);
            Assert.Equal(0, rovers[0].Position.X);
            Assert.Equal(2, rovers[0].Position.Y);
        }

        [Fact]
        public void ExecuteCommand_WhenNoCommand_ShouldReturnEmptyRoverList()
        {
            //Arrange
            var commands = new List<ICommand>();

            var input = new StringBuilder();
            input.AppendLine("5 5");

            _parser.Parse(input.ToString()).Returns(commands);

            //Act
            var rovers = _missionControlCenter.ExecuteCommand(input.ToString()).ToList();
            Assert.Empty(rovers);
        }
    }
}
