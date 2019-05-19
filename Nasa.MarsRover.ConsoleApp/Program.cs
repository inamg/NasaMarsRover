using System;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nasa.MarsRover.Commands;
using Nasa.MarsRover.IO;

namespace Nasa.MarsRover.ConsoleApp
{
    class Program
    {
        private static ILogger<Program> _logger;
        static void Main(string[] args)
        {

            try
            {
                var serviceProvider = ConfigureServices();
                _logger = serviceProvider.GetService<ILogger<Program>>();
                var missionControlCenter = serviceProvider.GetRequiredService<IMissionControlCenter>();
                var outputComposer = serviceProvider.GetRequiredService<IOutputComposer>();

                var rovers = missionControlCenter.ExecuteCommand(BuildCommandString());

                Console.Write(outputComposer.Compose(rovers));
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong. Please check the logs.");
                _logger.LogError(e.ToString());
            }
            finally
            {
                Console.ReadKey();
            }
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
                .AddLogging(configure => configure.AddConsole())
                .BuildServiceProvider();


            return serviceProvider;
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
    }
}
