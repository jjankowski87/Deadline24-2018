namespace Deadline24.Core.Visualization
{
    public class NullVisualizer<T> : IVisualizer<T>
    {
        public void UpdateGameState(T gameState)
        {
            // Do nothing
        }
    }
}
