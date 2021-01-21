using System.Collections.Generic;
using DeveloperRPG.GameStates;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace DeveloperRPG
{
    public class Game
    {
        public readonly Queue<IGameState> States = new();
        public readonly RenderWindow Window;
        public uint Width = 1280;
        public uint Height = 720;

        public Game()
        {
            Window = new RenderWindow(new VideoMode(Width, Height), "DeveloperRPG", Styles.Default | Styles.Resize);
            Window.Closed += (sender, _) => ((Window) sender)?.Close();
            Window.KeyPressed += (_, e) => InputManager.PressKey(e.Code);
            Window.KeyReleased += (_, e) => InputManager.ReleaseKey(e.Code);
            Window.Resized += (_, e) =>
            {
                Width = e.Width;
                Height = e.Height;
            };
            Window.SetFramerateLimit(60);

            States.Enqueue(new GameStateMain(this));
        }

        private IGameState CurrentState => States.TryPeek(out var state) ? state : null;

        public void Gameloop()
        {
            var clock = new Clock();
            while (Window.IsOpen)
            {
                Window.DispatchEvents();

                var elapsed = clock.Restart();

                CurrentState.HandleInput();
                CurrentState.Update(elapsed);

                Window.Clear(Color.Black);
                CurrentState.Render();
                Window.Display();
            }
        }
    }
}