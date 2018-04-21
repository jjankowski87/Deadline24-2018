using System.Drawing;
using System.Windows.Forms;
using Deadline24.ConsoleApp.RocketScience;

namespace Deadline24.ConsoleApp
{
    public partial class CarStatusControl : UserControl
    {
        private int _lastFuel = 0;

        public CarStatusControl()
        {
            InitializeComponent();
        }

        public void UpdateState(CarState carState)
        {
            lbCarId.Text = carState.Id.ToString();
            lbFuel.Text = carState.Fuel.ToString();

            if (carState.Fuel > 75)
            {
                lbFuel.ForeColor = Color.DarkGreen;
            }
            else if (carState.Fuel < 25)
            {
                lbFuel.ForeColor = Color.Red;
            }
            else
            {
                lbFuel.ForeColor = Color.Black;
            }

            if (_lastFuel != carState.Fuel)
            {
                if (_lastFuel < carState.Fuel)
                {
                    lbDiff.Text = $"+{carState.Fuel - _lastFuel}";
                    lbDiff.ForeColor = Color.Green;
                }
                else if (_lastFuel > carState.Fuel)
                {
                    lbDiff.Text = $"-{_lastFuel - carState.Fuel}";
                    lbDiff.ForeColor = Color.Red;
                }

                _lastFuel = carState.Fuel;
            }

            lbRoute.Text = carState.LengthToBase + " | " + string.Join(" -> ", carState.VisitedCities);
            lbUpgrade.Text = carState.Upgrades;
        }
    }
}
