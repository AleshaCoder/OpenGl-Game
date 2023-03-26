using Engine.Core;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            MyGame game = new MyGame();
            new CoreEngine(1280, 720, 4, game);
        }
    }
}
