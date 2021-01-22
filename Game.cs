using System.Collections.Generic;
using DeveloperRPG.GameStates;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace DeveloperRPG
{
    public class Game
    {
        private static readonly string WindowTitle = "DeveloperRPG";
        public readonly Queue<IGameState> States = new();
        private bool _isFullScreen;
        public uint Height = 720;
        public uint Width = 1280;
        public RenderWindow Window;

        public Game()
        {
            Window = new RenderWindow(new VideoMode(Width, Height), WindowTitle, Styles.Default | Styles.Resize);
            InitWindow();

            States.Enqueue(new GameStateMain(this));
        }

        private IGameState CurrentState => States.TryPeek(out var state) ? state : null;

        private void InitWindow()
        {
            Window.Closed += (sender, _) => ((Window) sender)?.Close();
            Window.KeyPressed += (_, e) => InputManager.PressKey(e.Code);
            Window.KeyReleased += (_, e) => InputManager.ReleaseKey(e.Code);
            Window.Resized += (_, e) =>
            {
                Width = e.Width;
                Height = e.Height;
            };
            Window.SetVerticalSyncEnabled(true);
        }

        public void Gameloop()
        {
            var clock = new Clock();
            while (Window.IsOpen)
            {
                Window.DispatchEvents();

                var elapsed = clock.Restart();

                CurrentState.HandleInput();
                CurrentState.Update(elapsed);

                if (InputManager.IsKeyPressed(Keyboard.Key.F11)) ToggleFullScreen();

                InputManager.NewFrame();

                Window.Clear(Color.Black);
                CurrentState.Render();
                Window.Display();
            }
        }

        private void ToggleFullScreen()
        {
            _isFullScreen = !_isFullScreen;
            if (_isFullScreen)
            {
                Window.Close();
                Window = new RenderWindow(VideoMode.DesktopMode, WindowTitle,
                    Styles.Fullscreen | Styles.Default);
                InitWindow();
            }
            else
            {
                Window.Close();
                Window = new RenderWindow(new VideoMode(Width, Height), WindowTitle,
                    Styles.Default | Styles.Resize);
                InitWindow();
            }
        }
    }
}