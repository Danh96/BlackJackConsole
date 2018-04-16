using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DeckOfCards
{
    public static class AsciiCards
    {
        public static void PrintAsciiCards(List<Card> playersHand)
        {
            var playersAsciiHand = playersHand.Select(c => GenerateAsciiCard(c));

            for (int i = 0; i < playersAsciiHand.First().Count(); i++)
            {
                foreach (List<string> asciiCard in playersAsciiHand)
                {
                    Console.Write(asciiCard[i]);
                    Console.Write("  ");
                }

                Console.WriteLine();
                Thread.Sleep(100);
            }
        }

        public static List<string> GenerateAsciiCard(Card card)
        {
            var cardValue = card.GetCardValue();
            var suit = card.GetcardSuit();

            string spacing = card.Value == 10 ? "" : " ";

            List<string> asciiCard = new List<string>
            {
                " ------------------- ",
                "| " + cardValue + spacing + "                |",
                "|   -------------   |",
                "|  |             |  |",
                "|  |             |  |",
                "|  |             |  |",
                "|  |             |  |",
                "|  |      " + suit + "      |  |",
                "|  |             |  |",
                "|  |             |  |",
                "|  |             |  |",
                "|  |             |  |",
                "|   -------------   |",
                "|                " + spacing + cardValue + " |",
                " ------------------- "
           };

            return asciiCard;
        }
    }
}