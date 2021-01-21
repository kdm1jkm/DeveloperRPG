﻿using System;
using SFML.Graphics;
using SFML.System;

namespace DeveloperRPG
{
    public class Player : IEntity
    {
        private static readonly Time AnimationInterval = Time.FromSeconds(0.5f);
        private const int FrameCount = 2;
        public const int Speed = 200;

        private readonly Sprite _sprite;
        private int _currentFrame;
        private Time _elapsed = Time.Zero;
        public Vector2f Velocity;

        public Player(Texture texture)
        {
            _sprite = new Sprite(texture) {Scale = new Vector2f(5.0f, 5.0f)};
            _sprite.TextureRect = new IntRect(0, 0, 8, 8);
        }

        public Vector2f Coordinate
        {
            get => _sprite.Position;
            set => _sprite.Position = value;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            _sprite.Draw(target, states);
        }

        private void NextFrame()
        {
            _currentFrame++;
            if (_currentFrame >= FrameCount) _currentFrame = 0;

            _sprite.TextureRect = new IntRect(_currentFrame * 8, 0, 8, 8);
        }

        public void Update(Time elapsed)
        {
            if (Velocity.X != 0 && Velocity.Y != 0)
            {
                Velocity /= 1.4142135623731f;
            }
            Coordinate += Velocity * elapsed.AsSeconds();
            _elapsed += elapsed;
            if (_elapsed > AnimationInterval)
            {
                _elapsed = Time.Zero;
                NextFrame();
            }
        }
    }
}