using System.Collections.Generic;
using static SFML.Window.Keyboard;

namespace DeveloperRPG
{
    public static class InputManager
    {
        private static readonly HashSet<Key> HeldKeys = new();
        private static readonly HashSet<Key> PressedKeys = new();
        private static readonly HashSet<Key> ReleasedKeys = new();

        public static void NewFrame()
        {
            PressedKeys.Clear();
            ReleasedKeys.Clear();
        }

        public static void PressKey(Key key)
        {
            HeldKeys.Add(key);
            PressedKeys.Add(key);
        }

        public static void ReleaseKey(Key key)
        {
            HeldKeys.Remove(key);
            ReleasedKeys.Add(key);
        }

        public static bool IsKeyHeld(Key key)
        {
            return HeldKeys.Contains(key);
        }

        public static bool IsKeyPressed(Key key)
        {
            return PressedKeys.Contains(key);
        }

        public static bool IsKeyReleased(Key key)
        {
            return ReleasedKeys.Contains(key);
        }
    }
}