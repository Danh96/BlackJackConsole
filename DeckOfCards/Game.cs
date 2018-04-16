using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DeckOfCards
{
    public class Game
    {
        private PlayingCardDeck Deck = new PlayingCardDeck();
        private List<Card> PlayersHand = new List<Card>();
        private List<Card> DealerHand = new List<Card>();
        private int DealersHandTotal;
        private int PlayersHandTotal;
        private int DealerGameScore;
        private int PlayerGameScore;

        public Game(int startScore)
        {
            DealerGameScore = startScore;
            PlayerGameScore = startScore;
        }

        public void Start()
        {
            bool runMatch = true;

            while (runMatch)
            {
                TypeToConsole.WriteLine("Lets Deal! " + DealerEmoji.DealingFace);

                StartGame();

                if (PlayerGameScore == 0 || DealerGameScore == 0)
                {
                    runMatch = false;
                }
                else
                {
                    TypeToConsole.WriteLine($"\nYour match score is {PlayerGameScore} And my match score is {DealerGameScore}");
                    TypeToConsole.WriteLine("\nLet the match continue, Next round! " + DealerEmoji.NextRoundFace);
                    Console.Clear();
                }
            }

            PrintFinalScore();
        }

        private void PrintFinalScore()
        {
            if (DealerGameScore > PlayerGameScore)
            {
                TypeToConsole.WriteLine("\nGame over! I win! better luck next time looser! " + DealerEmoji.WinFace);
            }
            else
            {
                TypeToConsole.WriteLine("\nGame over! Dammit you win, You have defeated me! " + DealerEmoji.LooseFace);
            }
        }

        private void DealersTurn()
        {
            bool dealersTurn = true;

            while (dealersTurn)
            {
                if (DealersHandTotal > PlayersHandTotal || DealersHandTotal == -1)
                {
                    dealersTurn = false;
                }
                else if (DealersHandTotal <= 16)
                {
                    DealerHand.Add(Deck.RemoveTopCard());
                    DealersHandTotal = UpdateScore(DealerHand);
                    CheckIfBust();
                }
                else
                {
                    dealersTurn = false;
                }
            }

            Thread.Sleep(1000);
        }

        private void CheckIfBust()
        {
            if (PlayersHandTotal > 21)
            {
                PlayersHandTotal = -1;
            }

            if (DealersHandTotal > 21)
            {
                DealersHandTotal = -1;
            }
        }

        private void PrintGameScore()
        {
            var PlayersHandFinalTotal = PlayersHandTotal == -1 ? "Bust!" : PlayersHandTotal.ToString();
            var DealersHandFinalTotal = DealersHandTotal == -1 ? "Bust!" : DealersHandTotal.ToString();

            if (PlayersHandTotal > DealersHandTotal)
            {
                TypeToConsole.WriteLine($"No you won the game! you scored {PlayersHandFinalTotal}. I scored {DealersHandFinalTotal}. " + DealerEmoji.AngryFace);
                DealerGameScore--;
                PlayerGameScore++;
            }
            else if (PlayersHandTotal < DealersHandTotal)
            {
                TypeToConsole.WriteLine($"HAHA! you lost! you scored {PlayersHandFinalTotal}. I scored {DealersHandFinalTotal}. " + DealerEmoji.HappyFace);
                DealerGameScore++;
                PlayerGameScore--;
            }
            else if (PlayersHandTotal == DealersHandTotal) 
            {
                TypeToConsole.WriteLine($"Dammit its a draw! we both scored {PlayersHandFinalTotal}. Our match scores stay the same! " + DealerEmoji.DislikeFace);
            }
        }

        private void PrintBothHands()
        {
            TypeToConsole.WriteLine("My hand was -");
            PrintHand(DealerHand);
            TypeToConsole.WriteLine("\nYour hand was -");
            PrintHand(PlayersHand);
        }

        private int UpdateScore(List<Card> hand)
        {
            int handTotal = 0;
            List<Card> aces = new List<Card>();

            foreach (Card c in hand)
            {
                if (c.Value == 1)
                {
                    aces.Add(c);
                }
                else if (c.Value > 10)
                {
                    handTotal += 10;
                }
                else
                {
                    handTotal += c.Value;
                }
            }

            return SetAceValue(aces, handTotal);
        }

        private int SetAceValue(List<Card> aces, int total)
        {
            int handTotal = total;

            foreach (Card c in aces)
            {
                if (total + 11 > 21)
                {
                    handTotal++;
                }
                else
                {
                    handTotal += 11;
                }
            }

            return handTotal;
        }

        private void NewGame()
        {
            Deck.GenerateDeck();
            Deck.Shuffle();

            PlayersHandTotal = 0;
            PlayersHand.Clear();

            DealersHandTotal = 0;
            DealerHand.Clear();

            PlayersHand.Add(Deck.RemoveTopCard());
            DealerHand.Add(Deck.RemoveTopCard());
            PlayersHand.Add(Deck.RemoveTopCard());
            DealerHand.Add(Deck.RemoveTopCard());

            DealersHandTotal = UpdateScore(DealerHand);

            TypeToConsole.WriteLine("Your hand is -");
            PrintHand(PlayersHand);
            PlayersHandTotal = UpdateScore(PlayersHand);
            PrintPlayerScore();
        }

        private void StartGame()
        {
            NewGame();

            bool playersTurn = true;

            while (playersTurn)
            {
                Console.WriteLine("Press '1' to stick or Press '2' to hit\n");

                switch (GetUserInput())
                {
                    case ConsoleKey.D1:
                        playersTurn = false;
                        break;
                    case ConsoleKey.D2:
                        GetPlayerNewCard();
                        playersTurn = PlayersHandTotal < 21;
                        CheckIfBust();
                        break;
                    default:
                        Console.WriteLine("Invalid input!\n");
                        break;
                }
            }

            TypeToConsole.WriteLine("Now its my turn! " + DealerEmoji.GoFace);
            DealersTurn();

            TypeToConsole.WriteLine("Ive finished my go, lets see who's won this hand! " + DealerEmoji.WhosWonFace);

            PrintGameScore();
            PrintBothHands();
        }

        private void GetPlayerNewCard()
        {
            PlayersHand.Add(Deck.RemoveTopCard());
            TypeToConsole.WriteLine("Your hand -");
            PrintHand(PlayersHand);
            PlayersHandTotal = UpdateScore(PlayersHand);
            PrintPlayerScore();
        }

        private void PrintPlayerScore()
        {
            if (PlayersHandTotal > 21)
            {
                Console.WriteLine($"\nYour score is {PlayersHandTotal}.\n");
                TypeToConsole.WriteLine("HAHA! you've gone bust! " + DealerEmoji.PlayerBustFace);
            }
            else
            {
                TypeToConsole.WriteLine($"\nYour score is {PlayersHandTotal}.");
            }
        }

        private void PrintHand(List<Card> hand)
        {
            AsciiCards.PrintAsciiCards(hand);
        }

        private static ConsoleKey GetUserInput()
        {
            try
            {
                return Console.ReadKey(true).Key;
            }
            catch
            {
                return ConsoleKey.D0;
            }
        }
    }
}