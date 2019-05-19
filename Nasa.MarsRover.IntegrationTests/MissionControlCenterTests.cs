using System;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Nasa.MarsRover.Commands;
using Nasa.MarsRover.IO;
using Xunit;

namespace Nasa.MarsRover.IntegrationTests
{
    // In real life i Would write many more integration Tests
    public class MissionControlCenterTests
    {
        private MissionControlCenter _missionControlCenter;

        public MissionControlCenterTests()
        {
            var commandMatcher = new CommandTypeMatcher();
            var provider = ConfigureServices();
            var commandParser = new InputCommandParser(commandMatcher, provider);

            _missionControlCenter = new MissionControlCenter(commandParser, new LandingPlateau());
        }

        [Fact]
        public void ExecuteCommand_WhenCommandsAreValid_ShouldReturnCorrectRovers()
        {
            //Act
            var rovers = _missionControlCenter.ExecuteCommand(BuildCommandString()).ToList();

            //Assert
            Assert.Equal(2,rovers.Count);
            Assert.Equal(1,rovers[0].Position.X);
            Assert.Equal(3, rovers[0].Position.Y);
            Assert.Equal(Direction.North, rovers[0].Direction);
            Assert.Equal(5, rovers[1].Position.X);
            Assert.Equal(1, rovers[1].Position.Y);
            Assert.Equal(Direction.East, rovers[1].Direction);
        }

        private static string BuildCommandString()
        {
            var commands = new StringBuilder();
            commands.AppendLine("5 5");
            commands.AppendLine("1 2 N");
            commands.AppendLine("LMLMLMLMM");
            commands.AppendLine("3 3 E");
            commands.Append("MMRMMRMRRM");

            return commands.ToString();
        }

        private static IServiceProvider ConfigureServices()
        {
            //setup DI
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IMissionControlCenter, MissionControlCenter>()
                .AddSingleton<IInputParser, InputCommandParser>()
                .AddSingleton<ICommandTypeMatcher, CommandTypeMatcher>()
                .AddSingleton<IOutputComposer, ConsoleOutputComposer>()
                .AddTransient<SetupPlateauCommandParser>()
                .AddTransient<DeployRoverCommandParser>()
                .AddTransient<ExploreRoverCommandParser>()
                .AddTransient<ILandingPlateau, LandingPlateau>()
                .BuildServiceProvider();


            return serviceProvider;
        }
    }
}
