using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    [PuzzleInput("Day14.txt", 14553106347726, 2737766154126)]
    [PuzzleInput("Day14-Sample01.txt", 165, -1, skipPart2: true)]
    [PuzzleInput("Day14-Sample02.txt", 51, 208)]
    public class Day14 : IDay
    {
        public long Part1(string input)
        {
            long orMask = 0;
            long andMask = 0;
            Dictionary<long, long> memory = new();
            foreach (string line in input.SplitNonEmptyLines())
            {
                string[] parts = line.Split(new char[] { ' ', '=', '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts[0] == "mask")
                {
                    orMask = Convert.ToInt64(parts[1].Replace('X', '0'), 2);
                    andMask = Convert.ToInt64(parts[1].Replace('X', '1'), 2);
                }
                else if (parts[0] == "mem")
                {
                    memory[long.Parse(parts[1])] = long.Parse(parts[2]) & andMask | orMask;
                }
                else
                {
                    throw new Exception("Unknown command " + parts[0]);
                }
            }
            return memory.Values.Sum();
        }
        public long Part2(string input)
        {
            long orMask = 0;
            long andMask = 0;
            long[] buildMasks = Array.Empty<long>();
            Dictionary<long, long> memory = new();
            foreach (string line in input.SplitNonEmptyLines())
            {
                string[] parts = line.Split(new char[] { ' ', '=', '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts[0] == "mask")
                {
                    orMask = Convert.ToInt64(parts[1].Replace('X', '0'), 2);
                    andMask = Convert.ToInt64(parts[1].Replace('0', '1').Replace('X', '0'), 2);
                    buildMasks = BuildMasks(parts[1].Replace('1', '0')).ToArray();
                }
                else if (parts[0] == "mem")
                {
                    foreach (long m in buildMasks)
                    {
                        memory[((long.Parse(parts[1]) | orMask) & andMask) | m] = long.Parse(parts[2]);
                    }
                }
                else
                {
                    throw new Exception("Unknown command " + parts[0]);
                }
            }
            return memory.Values.Sum();
        }
        private static IEnumerable<long> BuildMasks(string input)
        {
            if (input.Contains('X'))
            {
                foreach (string s in ReplaceFirst(input))
                {
                    foreach (var l in BuildMasks(s))
                    {
                        yield return l;
                    }
                }
            }
            else
            {
                yield return Convert.ToInt64(input, 2);
            }
        }
        private static IEnumerable<string> ReplaceFirst(string text)
        {
            int pos = text.IndexOf('X');
            if (pos < 0)
            {
                yield return text;
            }
            else
            {
                yield return text.Substring(0, pos) + '0' + text[(pos + 1)..];
                yield return text.Substring(0, pos) + '1' + text[(pos + 1)..];
            }
        }
    }
}
