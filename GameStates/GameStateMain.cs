using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using static DeveloperRPG.Utility;
using static SFML.Window.Keyboard;

namespace DeveloperRPG.GameStates
{
    public class GameStateMain : IGameState
    {
        private const int FollowRate = 10;
        private const float IdleZoom = 3.0f;
        private const float FarZoom = 2.5f;

        private readonly Game _game;
        private readonly List<Ground> _grounds;
        private readonly Player _player;
        private readonly View _view;
        private Vector2f _viewSpeed;

        public GameStateMain(Game game)
        {
            _game = game;
            _player = new Player();
            _view = new View(new Vector2f(0, 0), new Vector2f(1280, 720));
            _grounds = new List<Ground>();
            _viewSpeed = new Vector2f(0, 0);

            for (var i = 0; i < 100; i++)
            for (var j = 0; j < 100; j++)
                _grounds.Add(new Ground(new Vector2f(i, j)));
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
            UpdateView();
        }

        public void Render()
        {
            foreach (var ground in _grounds) _game.Window.Draw(ground);
            _game.Window.Draw(_player);
        }

        private void UpdateView()
        {
            var distance = Distance(_view.Center - _player.Coordinate);
            var maxDistance = Player.Speed * FollowRate / 60;
            Zoom = (float) (IdleZoom + Math.Pow(distance / maxDistance, 2) * (FarZoom - IdleZoom));
            _view.Move(-(_view.Center - _player.Coordinate) / FollowRate);
            // Console.Out.WriteLine($" length:{distance}, Zoom:{Zoom}");
            _game.Window.SetView(_view);
        }

        private void UpdateObjects(Time elapsed)
        {
            _player.Update(elapsed);
            foreach (var ground in _grounds) ground.Update(elapsed);
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