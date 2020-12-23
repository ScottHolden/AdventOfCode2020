using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    [PuzzleInput("Day23.txt", 78569234, 565615814504)]
    [PuzzleInput("Day23-Sample.txt", 67384529, 149245887792)]
    public class Day23 : IDay
    {
        public long Part1(string input)
        {
            List<int> cups = Solve(ParseCups(input), 100);
            string[] p = string.Join("", cups).Split('1');
            return long.Parse(p[1] + p[0]);
        }

        public long Part2(string input)
        {
            List<int> startingCups = ParseCups(input);
            int maxCup = startingCups.Max();
            startingCups.AddRange(Enumerable.Range(maxCup + 1, 1000000 - maxCup));

            List<int> cups = Solve(startingCups, 10000000);

            int i = cups.IndexOf(1);
            return (long)cups[i + 1] * cups[i + 2];
        }

        private static List<int> ParseCups(string input)
            => input.Trim().ToCharArray().Select(x => int.Parse(x.ToString())).ToList();

        private static List<int> Solve(List<int> startingCups, int itterations)
        {
            LinkedList<int> cups = new(startingCups);
            LinkedListNode<int>[] lookup = BuildLookup(cups);
            var maxCup = cups.Max();
            LinkedListNode<int> current = cups.First!;
            for (int i = 0; i < itterations; i++)
            {
                int currentValue = current.Value;
                LinkedListNode<int> cupARef = current.Next ?? cups.First!;
                LinkedListNode<int> cupBRef = cupARef.Next ?? cups.First!;
                LinkedListNode<int> cupCRef = cupBRef.Next ?? cups.First!;
                int cupA = cupARef.Value;
                int cupB = cupBRef.Value;
                int cupC = cupCRef.Value;
                cups.Remove(cupARef);
                cups.Remove(cupBRef);
                cups.Remove(cupCRef);
                int dest = ModWrap(currentValue - 1, maxCup);
                while (dest == cupA || dest == cupB || dest == cupC) dest = ModWrap(dest - 1, maxCup);
                LinkedListNode<int> destCup = lookup[dest];
                cups.AddAfter(destCup, cupCRef);
                cups.AddAfter(destCup, cupBRef);
                cups.AddAfter(destCup, cupARef);
                current = lookup[currentValue].Next ?? cups.First!;
            }
            return cups.ToList();
        }

        private static LinkedListNode<int>[] BuildLookup(LinkedList<int> input)
        {
            LinkedListNode<int>[] lookup = new LinkedListNode<int>[input.Max() + 1];
            LinkedListNode<int> lookupCurrent = input.First!;
            for (int i = 0; i < input.Count; i++)
            {
                lookup[lookupCurrent.Value] = lookupCurrent;
                lookupCurrent = lookupCurrent.Next ?? input.First!;
            }
            return lookup;
        }

        private static int ModWrap(int input, int mod)
        {
            return input <= 0 ? mod + input : input % mod;
        }
    }
}
