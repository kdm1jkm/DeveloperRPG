using System;
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
            _game = game;
            _player = new Player();
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
            int x = 0, y = 0;
            if (InputManager.IsKeyHeld(Key.A))
                x -= 1;
            if (InputManager.IsKeyHeld(Key.D))
                x += 1;
            if (InputManager.IsKeyHeld(Key.W))
                y -= 1;
            if (InputManager.IsKeyHeld(Key.S))
                y += 1;

            if (x == 0 && y == 0)
            {
                _player.Velocity = 0;
            }
            else
            {
                _player.Velocity = Player.Speed;
                _player.Direction = (float) Math.Atan2(y, x);
            }
        }
    }
}