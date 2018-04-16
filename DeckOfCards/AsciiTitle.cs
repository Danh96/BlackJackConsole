using System;
using System.Collections.Generic;
using System.Threading;

namespace DeckOfCards
{
    public class AsciiTitle
    {
        public static void Print()
        {
            List<String> title = new List<String>
            {
                "  ██████╗ ██╗      █████╗  ██████╗██╗  ██╗     ██╗ █████╗  ██████╗██╗  ██╗    ",
                "  ██╔══██╗██║     ██╔══██╗██╔════╝██║ ██╔╝     ██║██╔══██╗██╔════╝██║ ██╔╝    ",
                "  ██████╔╝██║     ███████║██║     █████╔╝      ██║███████║██║     █████╔╝     ",
                "  ██╔══██╗██║     ██╔══██║██║     ██╔═██╗ ██   ██║██╔══██║██║     ██╔═██╗     ",
                "  ██████╔╝███████╗██║  ██║╚██████╗██║  ██╗╚█████╔╝██║  ██║╚██████╗██║  ██╗    ",
                "  ╚═════╝ ╚══════╝╚═╝  ╚═╝ ╚═════╝╚═╝  ╚═╝ ╚════╝ ╚═╝  ╚═╝ ╚═════╝╚═╝  ╚═╝    "
            };

            Thread.Sleep(600);

            foreach (string s in title)
            {
                Console.WriteLine(s);
                Thread.Sleep(200);
            }

            Console.WriteLine();
            Thread.Sleep(2000);
        }
    }
}