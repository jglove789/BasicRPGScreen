
using System;

namespace BasicRPGScreen
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new BasicRPGScreenGame())
                game.Run();
        }
    }
}