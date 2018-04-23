using System.Drawing;
using System.Windows.Forms;
using Deadline24.ConsoleApp.RocketScience;
using Deadline24.Core;
using Deadline24.Core.Visualization;

namespace Deadline24.ConsoleApp
{
    public partial class VisualizationForm : Form, IVisualizer<RocketScienceGameState>
    {
        private readonly GameLoop<RocketScienceGameState> _gameLoop;

        private int _lastAmount = 0;

        public VisualizationForm()
        {
            InitializeComponent();

            for (int i = 0; i < 10; i++)
            {
                panel.Controls.Add(new CarStatusControl()
                {
                    Location = new Point(10, 40 * i)
                });
            }

            var server = Properties.Settings.Default.Server;
            var port = Properties.Settings.Default.Port;
            var user = Properties.Settings.Default.User;
            var password = Properties.Settings.Default.Password;

            var client = new Client(server, port, user, password);
            var game = new RocketScience.Game();
            _gameLoop = new GameLoop<RocketScienceGameState>(game, client, this);

            _gameLoop.Start();
            Text = $"Rocket Science: {port}";
        }

        public void UpdateGameState(RocketScienceGameState gameState)
        {
            CheckForIllegalCrossThreadCalls = false;

            for (var i = 0; i < gameState.Cars.Count; i++)
            {
                ((CarStatusControl)panel.Controls[i]).UpdateState(gameState.Cars[i]);
            }

            lbRockets.Text = gameState.TimeToEnd.LaunchedRockets.ToString();
            lbTurns.Text = gameState.TimeToEnd.TurnsTillEnd.ToString();
            lbBase.Text = gameState.HomeStats.IDh_HomeCityId.ToString();
            lbMoney.Text = $"{gameState.HomeStats.Ch_Amount}$";
            lbUpgradeCost.Text = $"{gameState.DescribeWorld.Sue_CarUpgradeCost}$";
            lbLaunchCost.Text = $"{gameState.DescribeWorld.Sr_AmountToDepositBeforeLaunch}$";
            lbLaunchTax.Text = $"{gameState.DescribeWorld.Sf_AmountForTaxes}$";
            lbCityId.Text = gameState.RocketInfo?.CityId.ToString() ?? "X";
            lbDeposit.Text = $"{gameState.RocketInfo?.Dt_TotalDeposit ?? 0} $";

            var moneyDiff = gameState.HomeStats.Ch_Amount - _lastAmount;
            if (_lastAmount != gameState.HomeStats.Ch_Amount)
            {
                lbMoneyDiff.Text = $"{moneyDiff}$";

                if (moneyDiff > 0)
                {
                    lbMoneyDiff.ForeColor = Color.DarkGreen;
                }
                else if (moneyDiff < 0)
                {
                    lbMoneyDiff.ForeColor = Color.Red;
                }

                _lastAmount = gameState.HomeStats.Ch_Amount;
            }
            
        }
    }
}
