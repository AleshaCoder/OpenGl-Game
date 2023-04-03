using Engine.Core;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            MyGame game = new MyGame();
            new CoreEngine(1000, 1000, 100, game);
        }
    }
}
