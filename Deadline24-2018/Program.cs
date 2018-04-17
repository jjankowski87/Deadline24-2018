using System;
using Deadline24.Core;

namespace Deadline24.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var server = Properties.Settings.Default.Server;
            var port = Properties.Settings.Default.Port;
            var user = Properties.Settings.Default.User;
            var password = Properties.Settings.Default.Password;

            using (var client = new Client(server, port, user, password))
            {
                var game = new WildWildSpace.Game();
                var gameloop = new GameLoop(game, client);

                Console.WriteLine("#### Game started ####");
                gameloop.Start();

                while (Console.ReadKey(true).Key != ConsoleKey.Q) {}

                gameloop.Stop();
                Console.WriteLine("#### Game stopped ####");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}