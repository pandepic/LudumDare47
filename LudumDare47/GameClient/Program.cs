using System;

namespace GameClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var game = new GameClient())
                game.Run();
        }
    }
}
