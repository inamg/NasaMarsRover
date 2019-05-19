using System;
using System.Drawing;
using Nasa.MarsRover.Commands;
using Nasa.MarsRover.Exceptions;
using NSubstitute;
using Xunit;


namespace Nasa.MarsRover.Tests.Commands
{
    public class DeployRoverCommandTests
    {
        private DeployRoverCommand _deployRoverCommand;

        public DeployRoverCommandTests()
        {
            _deployRoverCommand=new DeployRoverCommand(new Point(2,3),Direction.North );
        }

        [Fact]
        public void Execute_WhenRoverIsNotSet_ShouldThrowException()
        {
            //Assert
            Assert.Throws<DeployRoverException>(()=>_deployRoverCommand.Execute());
        }

        [Fact]
        public void Execute_WhenPlateauIsNotSet_ShouldThrowException()
        {
            //Arrange
            _deployRoverCommand.SetRover(new Rover());

            //Assert
            Assert.Throws<DeployRoverException>(() => _deployRoverCommand.Execute());
        }

        [Fact]
        public void Execute_PositionIsCorrect_ShouldDeployRover()
        {
            //Arrange
            var rover = new Rover();
            _deployRoverCommand.SetRover(rover);
            var plateau = Substitute.For<ILandingPlateau>();
            plateau.IsValidPoint(Arg.Any<Point>()).Returns(true);
            plateau.Size.Returns(new Size(5,5));
            _deployRoverCommand.SetPlateau(plateau);

            //Act
            _deployRoverCommand.Execute();

            //Assert
            Assert.Equal(2,rover.Position.X);
            Assert.Equal(3, rover.Position.Y);
            Assert.Equal(Direction.North, rover.Direction);
        }

    }
}
