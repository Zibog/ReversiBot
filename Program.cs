using System;

namespace ReversiBot
{
    class Program
    {
        static void Main(string[] args)
        {
            int id = int.Parse(Console.ReadLine()); // id of your player.
            int boardSize = int.Parse(Console.ReadLine());

            // game loop
            while (true)
            {
                for (int i = 0; i < boardSize; i++)
                {
                    string line = Console.ReadLine(); // rows from top to bottom (viewer perspective).
                }

                int actionCount = int.Parse(Console.ReadLine()); // number of legal actions for this turn.
                for (int i = 0; i < actionCount; i++)
                {
                    string action = Console.ReadLine(); // the action
                }

                // Write an action using Console.WriteLine()
                // To debug: Console.Error.WriteLine("Debug messages...");

                Console.WriteLine("f4"); // a-h1-8
            }
        }
    }
}