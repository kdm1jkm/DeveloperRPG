using System;
using SFML.System;

namespace DeveloperRPG
{
    public static class Utility
    {
        public static double Distance(Vector2f sub)
        {
            return Math.Sqrt(Math.Pow(sub.X, 2) + Math.Pow(sub.Y, 2));
        }
    }
}