using SFML.Graphics;
using SFML.System;

namespace DeveloperRPG
{
    public interface IEntity : Drawable
    {
        public void Update(Time elapsed);
    }
}