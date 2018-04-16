using System;
using System.Runtime.InteropServices;

namespace DeckOfCards
{
    class MainClass
    {
        [DllImport("libc")]
        private static extern int system(string exec);

        public static void Main(string[] args)
        {
            system(@"printf '\e[8;50;160t'");

            Console.Clear();
            Console.WriteLine("Press enter to start blackjack......");

            var EasterEgg = string.Empty;

            bool GameStart = false;

            while(GameStart == false)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Enter:
                        GameStart = true;
                        break;
                    case ConsoleKey.E:
                        EasterEgg = "(But you can call me daddy!) ";
                        GameStart = true;
                        break;
                    default:
                        Console.WriteLine("Invalid Input!\n");
                        break;
                }
            }

            Console.Clear();
            Console.WriteLine();
            AsciiTitle.Print();
            TypeToConsole.WriteLine($"Hello! my name's Kevin {EasterEgg}and I will be your dealer today! You think you've got what it takes to beat me?! " + DealerEmoji.StartFace);

            Game game = new Game(3);
            game.Start();
        }
    }
}