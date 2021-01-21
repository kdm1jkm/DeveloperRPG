using SFML.System;

namespace DeveloperRPG.GameStates
{
    public interface IGameState
    {
        public void HandleInput();

        public void Update(Time elapsed);

        public void Render();
    }
}