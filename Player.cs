using System;
using SFML.Graphics;
using SFML.System;

namespace DeveloperRPG
{
    public class Player : IEntity
    {
        private const int FrameCount = 2;
        public const int Speed = 200;
        private static readonly Time AnimationInterval = Time.FromSeconds(0.5f);

        private readonly Sprite _sprite;
        private int _currentFrame;
        private Time _elapsed = Time.Zero;
        public float Direction = 0;
        public float Velocity = 0;

        public Player()
        {
            _sprite = new Sprite(ResourceManager.LoadTexture(ResourceManager.CharacterPath))
                {Scale = new Vector2f(5.0f, 5.0f), TextureRect = new IntRect(0, 0, 8, 8)};
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

        public void Update(Time elapsed)
        {
            Coordinate += new Vector2f((float) Math.Cos(Direction), (float) Math.Sin(Direction)) * Velocity *
                          elapsed.AsSeconds();
            _elapsed += elapsed;
            if (_elapsed > AnimationInterval)
            {
                _elapsed = Time.Zero;
                NextFrame();
            }
        }

        private void NextFrame()
        {
            _currentFrame++;
            if (_currentFrame >= FrameCount) _currentFrame = 0;

            _sprite.TextureRect = new IntRect(_currentFrame * 8, 0, 8, 8);
        }
    }
}