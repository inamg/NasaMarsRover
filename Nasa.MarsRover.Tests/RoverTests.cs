using System;
using System.Collections.Generic;
using System.Drawing;
using Nasa.MarsRover.Exceptions;
using NSubstitute;
using Xunit;


namespace Nasa.MarsRover.Tests
{
    // in real world. I would write many more tests, with ArgumentNullException, multiple movements etc
    public class RoverTests
    {
        private readonly Rover _rover;

        public RoverTests()
        {
            _rover = new Rover();
        }

        [Fact]
        public void Deploy_WhenPositionInNotValid_ShowThrowException()
        {
            //Arrange
            var position = new Point(100, 100);
            var plateau = new LandingPlateau();

            // Assert
            Assert.Throws<DeployRoverException>(() => _rover.Deploy(position, Direction.North, plateau));
        }

        [Theory]
        [InlineData(1, 1, Direction.North)]
        [InlineData(2, 1, Direction.North)]
        [InlineData(2, 1, Direction.South)]
        [InlineData(2, 1, Direction.East)]
        [InlineData(2, 2, Direction.West)]
        public void Deploy_WhenPositionIsValid_ShouldUpdateDirectionPosition(int x, int y, Direction direction)
        {
            //Arrange
            var position = new Point(x, y);
            var plateau = Substitute.For<ILandingPlateau>();
            plateau.IsValidPoint(position).Returns(true);

            // Act
            _rover.Deploy(position, direction, plateau);

            //Assert
            Assert.Equal(direction, _rover.Direction);
            Assert.Equal(position.X, _rover.Position.X);
            Assert.Equal(position.Y, _rover.Position.Y);
        }

        [Theory]
        [InlineData(Direction.South, Movement.Left, Direction.East)]
        [InlineData(Direction.North, Movement.Left, Direction.West)]
        [InlineData(Direction.North, Movement.Right, Direction.East)]
        [InlineData(Direction.South, Movement.Right, Direction.West)]
        public void Move_WhenMovementLeftRight_ShouldUpdateDirection(Direction initial, Movement input, Direction result)
        {
            //Arrange
            var position = new Point(2, 2);
            var plateau = Substitute.For<ILandingPlateau>();
            plateau.IsValidPoint(position).Returns(true);
            _rover.Deploy(position, initial, plateau);

            var movementList = new List<Movement>
            {
                input
            };

            //Act
            _rover.Move(movementList);

            //Assert
            Assert.Equal(result, _rover.Direction);
        }

        [Theory]
        [InlineData(1,1,Direction.North,1,2)]
        [InlineData(2, 2, Direction.South, 2, 1)]
        [InlineData(1, 1, Direction.East, 2, 1)]
        [InlineData(2, 2, Direction.West, 1, 2)]
        public void Move_WhenMovementIsForward_ShouldUpdatePosition(
            int initialX,
            int initialY,
            Direction initial,
            int finalX,
            int finalY)
        {
            //Arrange
            var position = new Point(initialX, initialY);
            var plateau = Substitute.For<ILandingPlateau>();
            plateau.IsValidPoint(Arg.Any<Point>()).Returns(true);
            _rover.Deploy(position, initial, plateau);

            var movementList = new List<Movement>
            {
                Movement.Forward
            };

            //Act
            _rover.Move(movementList);

            //Assert
            Assert.Equal(finalX, _rover.Position.X);
            Assert.Equal(finalY, _rover.Position.Y);
        }
    }
}
