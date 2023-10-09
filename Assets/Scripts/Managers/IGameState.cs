using Mercenary.Managers;

namespace Mercenary.Utilities
{
    public interface IGameState
    {
        public void OnGameStateChanged(GameState newState);
    }

}
