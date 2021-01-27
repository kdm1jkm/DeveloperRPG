using System;
using System.Collections.Generic;
using SFML;
using SFML.Graphics;

namespace DeveloperRPG
{
    public static class ResourceManager
    {
        private static readonly Dictionary<string, Texture> Textures = new();

        public static Texture LoadTexture(string filePath)
        {
            if (!Textures.ContainsKey(filePath))
                try
                {
                    Textures[filePath] = new Texture(filePath);
                }
                catch (LoadingFailedException e)
                {
                    Console.WriteLine(e.Message);
                    Environment.Exit(-1);
                    return null;
                }

            return Textures[filePath];
        }
    }
}