using SFML.Graphics;
using SFML.System;
using static DeveloperRPG.ResourceManager;

namespace DeveloperRPG
{
    public class Ground : IEntity
    {
        private readonly Sprite _sprite;
        private readonly Vector2f _tilePosition;

        public Ground(Vector2f tilePosition)
        {
            _tilePosition = tilePosition;
            _sprite = new Sprite(LoadTexture(GroundPath));
            _sprite.Position = tilePosition * 16;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            _sprite.Draw(target, states);
        }

        public void Update(Time elapsed)
        {
        }
    }
}