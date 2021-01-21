using SFML.System;
using static SFML.Window.Keyboard;

namespace DeveloperRPG.GameStates
{
    public class GameStateMain : IGameState
    {
        private readonly Game _game;
        private readonly Player _player;

        public GameStateMain(Game game)
        {
            var manager = new ResourceManager();
            _game = game;
            _player = new Player(manager.LoadTexture("Resources/Character.png", () => game.Window.Close()));
        }

        public void HandleInput()
        {
            HandlePlayer();
        }

        public void Update(Time elapsed)
        {
            _player.Update(elapsed);
        }

        public void Render()
        {
            _game.Window.Draw(_player);
        }

        private void HandlePlayer()
        {
            _player.Velocity.X = 0;
            _player.Velocity.Y = 0;

            if (InputManager.IsKeyHeld(Key.A))
                _player.Velocity.X = -Player.Speed;
            else if (InputManager.IsKeyHeld(Key.D))
                _player.Velocity.X = Player.Speed;

            if (InputManager.IsKeyHeld(Key.W))
                _player.Velocity.Y = -Player.Speed;
            else if (InputManager.IsKeyHeld(Key.S))
                _player.Velocity.Y = Player.Speed;
        }
    }
}