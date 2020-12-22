using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    [PuzzleInput("Day22.txt", 32199, 33780)]
    //[PuzzleInput("Day22-Sample01.txt", 306, 291)]
    public class Day22 : IDay
    {
        public long Part1(string input)
        {
            Queue<int>[] cards = ParseCards(input);
            while (!cards.Any(x => x.Count == 0))
            {
                int a = cards[0].Dequeue();
                int b = cards[1].Dequeue();
                if (a > b)
                {
                    cards[0].Enqueue(a);
                    cards[0].Enqueue(b);
                }
                else
                {
                    cards[1].Enqueue(b);
                    cards[1].Enqueue(a);
                }
            }
            return SumCards(cards[0].Count > 0 ? cards[0] : cards[1]);
        }

        private static long SumCards(Queue<int> hand)
        {
            int[] solution = hand.ToArray();
            Array.Reverse(solution);
            long sum = 0;
            for (int i = 0; i < solution.Length; i++)
            {
                sum += (i + 1) * solution[i];
            }
            return sum;
        }

        private static Queue<int>[] ParseCards(string input)
            => input.Split("\n\n").Select(x => new Queue<int>(x.Split('\n').Skip(1).Select(int.Parse))).ToArray();

        public long Part2(string input)
        {
            Queue<int>[] cards = ParseCards(input);
            (Queue<int>[] finalCards, int winner) = PlayGame(cards);
            return SumCards(finalCards[winner]);
        }

        private static (Queue<int>[] Cards, int Winner) PlayGame(Queue<int>[] cards)
        {
            HashSet<string> previous = new();
            while (!cards.Any(x => x.Count == 0))
            {
                var prev = ToPrev(cards);
                if (previous.Contains(prev)) return (cards, 0);
                previous.Add(prev);

                int a = cards[0].Dequeue();
                int b = cards[1].Dequeue();

                if (a <= cards[0].Count &&
                    b <= cards[1].Count)
                {
                    int winner = PlayGame(new[]
                    {
                        new Queue<int>(cards[0].Take(a)),
                        new Queue<int>(cards[1].Take(b))
                    }).Winner;
                    cards[winner].Enqueue(winner == 0 ? a : b);
                    cards[winner].Enqueue(winner == 0 ? b : a);
                }
                else if (a > b)
                {
                    cards[0].Enqueue(a);
                    cards[0].Enqueue(b);
                }
                else
                {
                    cards[1].Enqueue(b);
                    cards[1].Enqueue(a);
                }
            }
            return (cards, cards[0].Count > 0 ? 0 : 1);
        }
        private static string ToPrev(Queue<int>[] cards)
        {
            var a = cards[0].ToArray();
            var b = cards[1].ToArray();
            char[] c = new char[a.Length + 1 + b.Length];
            int i;
            for (i = 0; i < a.Length; i++)
            {
                c[i] = (char)(a[i] + 20);
            }
            for (i++; i < c.Length; i++)
            {
                c[i] = (char)(b[i - 1 - a.Length] + 20);
            }
            return new string(c);
        }
        //private static string ToPrev(Queue<int>[] cards)
        //    => string.Join('=', cards.Select(x => string.Join(',', x)));
    }
}
