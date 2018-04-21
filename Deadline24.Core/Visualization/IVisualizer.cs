namespace Deadline24.Core.Visualization
{
    public interface IVisualizer<T>
    {
        void UpdateGameState(T gameState);
    }
}
