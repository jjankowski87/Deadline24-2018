using System.Windows.Forms;
using Deadline24.Core;
using Deadline24.Core.Visualization;

namespace Deadline24.ConsoleApp
{
    public partial class VisualizationForm : Form, IVisualizer
    {
        private readonly GameLoop _gameLoop;

        public VisualizationForm()
        {
            InitializeComponent();

            var server = Properties.Settings.Default.Server;
            var port = Properties.Settings.Default.Port;
            var user = Properties.Settings.Default.User;
            var password = Properties.Settings.Default.Password;

            var client = new Client(server, port, user, password);
            var game = new RocketScience.Game();
            _gameLoop = new GameLoop(game, client, this);

            _gameLoop.Start();
        }

        public void DisplayWorld(World world)
        {
            CheckForIllegalCrossThreadCalls = false;

        }
    }
}
