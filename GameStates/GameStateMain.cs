using System;
using System.Threading;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using static SFML.Window.Keyboard;

namespace DeveloperRPG.GameStates
{
    public class GameStateMain : IGameState
    {
        private readonly Game _game;
        private readonly Player player;

        public GameStateMain(Game game)
        {
            var manager = new ResourceManager();
            _game = game;
            player = new Player(manager.LoadTexture("Resources/Character.png", () => game.Window.Close()));
        }

        public void HandleInput()
        {
            HandlePlayer();
        }

        private void HandlePlayer()
        {
            player.Velocity.X = 0;
            player.Velocity.Y = 0;

            if (InputManager.IsKeyHeld(Key.A))
                player.Velocity.X = -Player.Speed;
            else if (InputManager.IsKeyHeld(Key.D))
                player.Velocity.X = Player.Speed;

            if (InputManager.IsKeyHeld(Key.W))
                player.Velocity.Y = -Player.Speed;
            else if (InputManager.IsKeyHeld(Key.S))
                player.Velocity.Y = Player.Speed;
        }

        public void Update(Time elapsed)
        {
            player.Update(elapsed);
        }

        public void Render()
        {
            _game.Window.Draw(player);
        }
    }
}