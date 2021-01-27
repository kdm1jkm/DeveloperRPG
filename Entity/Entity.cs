using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SFML.Graphics;
using SFML.System;

namespace DeveloperRPG
{
    public class Entity : Drawable
    {
        // ReSharper disable once InconsistentNaming
        public const string CHARACTER_PATH = "Resources/Sprites/Character.json";

        protected readonly Dictionary<string, Animation> Animations;

        public readonly int Height;
        public readonly int Width;
        
        public Vector2f Coordinate
        {
            get => Sprite.Position;
            set => Sprite.Position = value;
        }

        protected readonly Sprite Sprite;
        private Animation _currentAnimation;

        public Entity(string filePath)
        {
            var json = JObject.Load(new JsonTextReader(new StreamReader(filePath)));
            var filename = Path.Join(Path.GetDirectoryName(filePath)!, json["imageFile"]!.ToString());
            // Console.Out.WriteLine("filename = {0}", filename);
            Sprite = new Sprite(new Texture(ResourceManager.LoadTexture(filename)));

            var size = (
                from v in json["size"]!.ToArray()
                select int.Parse(v.ToString())
            ).ToArray();

            Width = size[0];
            Height = size[1];

            Animations = new Dictionary<string, Animation>();

            var queryAnimations =
                from animation in json["animations"]!
                let frames = (
                    from frame in animation["frames"]!
                    let position = (
                        from v in frame["position"].ToArray()
                        select int.Parse(v.ToString())
                    ).ToArray()
                    select new KeyValuePair<IntRect, Time>
                    (
                        new IntRect
                        (
                            new Vector2i
                            (
                                position[0] * Width,
                                position[1] * Height
                            ),
                            new Vector2i(Width, Height)
                        ),
                        Time.FromMilliseconds(int.Parse(frame["duration"]!.ToString())))
                ).ToList()
                let name = animation["name"]!.ToString()
                select new KeyValuePair<string, Animation>(name, new Animation(frames));

            foreach (var (name, animation) in queryAnimations) Animations[name] = animation;

            // Console.Out.WriteLine("Animations = {0}", Animations.ToString());

            CurrentAnimation = Animations.Values.ToArray()[0];
            CurrentAnimation.Reset();
        }

        protected Animation CurrentAnimation
        {
            get => _currentAnimation;
            set
            {
                _currentAnimation = value;
                _currentAnimation.Reset();
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            Sprite.TextureRect = CurrentAnimation.GetCurrent();
            Sprite.Draw(target, states);
        }

        public virtual void Update(Time elapsed)
        {
            CurrentAnimation.Update(elapsed);
        }
    }
}