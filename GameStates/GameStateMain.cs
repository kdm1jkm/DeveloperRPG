using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using static SFML.Window.Keyboard;

namespace DeveloperRPG.GameStates
{
    public class GameStateMain : IGameState
    {
        private const float FollowRate = 50 / Player.Speed;
        private const float IdleZoom = 3.0f;
        private const float FarZoom = 1.5f;

        private readonly Game _game;
        // private readonly List<Entity> _grounds;
        private readonly Player _player;
        private readonly View _view;

        public GameStateMain(Game game)
        {
            _game = game;
            _player = new Player();
            _view = new View(new Vector2f(0, 0), new Vector2f(1280, 720));
            // _grounds = new List<Entity>();
            // for (var i = 0; i < 100; i++)
            // for (var j = 0; j < 100; j++)
            // {
                // _grounds.Add(new Entity("Resources/Ground.json"));
                // _grounds[^1].Coordinate = new Vector2f(i, j) * 16;
            // }
        }

        private float Zoom
        {
            get => _game.Width / _view.Size.X;
            set => _view.Size = new Vector2f(_game.Width, _game.Height) / value;
        }

        public void HandleInput()
        {
            HandlePlayer();
        }

        public void Update(Time elapsed)
        {
            UpdateObjects(elapsed);
            UpdateView(elapsed);
        }

        public void Render()
        {
            // foreach (var ground in _grounds) _game.Window.Draw(ground);
            _game.Window.Draw(_player);
        }

        private void UpdateView(Time elapsed)
        {
            var distance = Utility.Distance(_view.Center - _player.Coordinate);
            Zoom = (float) (IdleZoom - (1 - distance / Player.Speed / FollowRate) * (FarZoom - IdleZoom));
            _view.Move(-(_view.Center - _player.Coordinate) / FollowRate * elapsed.AsSeconds());
            // Console.Out.WriteLine($"distance:{distance}");
            _game.Window.SetView(_view);
        }

        private void UpdateObjects(Time elapsed)
        {
            _player.Update(elapsed);
            // foreach (var ground in _grounds) ground.Update(elapsed);
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

            if (InputManager.IsKeyPressed(Key.Space)) _player.Velocity = Player.Speed * 5;

            if (x == 0 && y == 0)
            {
                _player.State = Player.PlayerState.StateIdle;
            }
            else
            {
                _player.State = Player.PlayerState.StateMoving;
                _player.Direction = (float) Math.Atan2(y, x);
            }
        }
    }
}