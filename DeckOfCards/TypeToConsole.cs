using System;
using System.Threading;

namespace DeckOfCards
{
    public static class TypeToConsole
    {
        public static void WriteLine(string line)
        {
            for (int i = 0; i < line.Length; i++)
            {
                Console.Write(line[i]);
                Thread.Sleep(50);
            }

            Console.WriteLine();
            Console.WriteLine();
            Thread.Sleep(2000);
        }
    }
}