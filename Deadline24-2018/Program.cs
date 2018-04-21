using System;
using System.Threading;
using System.Windows.Forms;
using Deadline24.Core;
using Deadline24.Core.Visualization;

namespace Deadline24.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (Properties.Settings.Default.UseVisualization)
            {
                Application.EnableVisualStyles();
                Application.Run(new VisualizationForm());
            }
            else
            {
                RunConsole(new NullVisualizer());
            }
        }

        private static void RunConsole(IVisualizer visualizer)
        {
            var server = Properties.Settings.Default.Server;
            var port = Properties.Settings.Default.Port;
            var user = Properties.Settings.Default.User;
            var password = Properties.Settings.Default.Password;

            using (var client = new Client(server, port, user, password))
            {
                var game = new RocketScience.Game();
                var gameloop = new GameLoop(game, client, visualizer);

                Console.WriteLine($"#### Game started at port {port} ####");
                gameloop.Start();

                while (Console.ReadKey(true).Key != ConsoleKey.Q) { Thread.Sleep(500); }

                gameloop.Stop();
                Console.WriteLine("#### Game stopped ####");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}