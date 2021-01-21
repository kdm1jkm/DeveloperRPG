using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using SFML.Graphics;

namespace DeveloperRPG
{
    public class ResourceManager
    {
        private readonly Dictionary<string, Texture> _textures = new();

        public Texture LoadTexture(string filePath, Action runWhenFail )
        {
            if (!_textures.ContainsKey(filePath))
            {
                try
                {
                    _textures[filePath] = new Texture(filePath);
                }
                catch (SFML.LoadingFailedException e)
                {
                    Console.WriteLine(e.Message);
                    new Task(runWhenFail).Start();
                    return null;
                }
            }

            return _textures[filePath];
        }
    }
}