using System;
using System.Collections;

namespace RockPaperScissors.ConsoleApp
{
    class Program
    {
        static int idSeed = 0;
        static bool run = true;
        static Game game = new Game();
        static Computer c = new Computer();

        static void Main(string[] args)
        {
            Console.WriteLine("Rock Paper Scissors Game");
            Console.WriteLine("------------------------");

            while (run)
            {
                Console.WriteLine("\nEnter r for Rock\nEnter p for Paper\nEnter s for Scissors\nEnter l for Log\nEnter q to Quit");
                string input = Console.ReadLine();
                if (input.Equals("r", StringComparison.InvariantCultureIgnoreCase))
                {
                    Play(Option.Rock);
                }
                else if (input.Equals("p", StringComparison.InvariantCultureIgnoreCase))
                {
                    Play(Option.Paper);
                }
                else if (input.Equals("s", StringComparison.InvariantCultureIgnoreCase))
                {
                    Play(Option.Scissors);
                }
                else if (input.Equals("l", StringComparison.InvariantCultureIgnoreCase))
                {
                    Play(Option.Log);
                }
                else if (input.Equals("q", StringComparison.InvariantCultureIgnoreCase))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("\nEnter r for Rock\nEnter p for Paper\nEnter s for Scissors\nEnter l for Log\nEnter q to Quit");
                }
            }
        }
        static void Play(Option o)
        {
            idSeed++;
            if(o == Option.Log)
            {
                Console.WriteLine(game.DisplayLog()); 
            }
            else
            {
                Console.WriteLine(game.PlayGame(idSeed, o, c.GetOption()));
            }
        }
    }
}
