using Mercenary.Managers;

namespace Mercenary.Utilities
{
    public interface IGameState
    {
        //interface that defines functions related to game state
        public void OnGameStateChanged(GameState newState);
    }

}
