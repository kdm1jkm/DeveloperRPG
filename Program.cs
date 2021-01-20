using SFML.Graphics;
using SFML.Window;

namespace DeveloperRPG
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var window = new RenderWindow(new VideoMode(1280, 720), "Hello, World!");
            window.Closed += (sender, _) => { ((Window) sender)?.Close(); };
            
            window.SetFramerateLimit(60);

            while (window.IsOpen)
            {
                window.DispatchEvents();
                window.Clear(Color.Black);
                
                window.Display();
            }
        }
    }
}