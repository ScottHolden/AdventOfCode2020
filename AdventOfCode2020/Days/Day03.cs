namespace AdventOfCode2020
{
    [PuzzleInput("Day03.txt", 282, 958815792)]
    [PuzzleInput("Day03-Sample.txt", 7, 336)]
    public class Day03 : IDay
    {
        private const char Tree = '#';

        public long Part1(string input) => Part1(input.SplitNonEmptyLines());
        private static long Part1(string[] lines) => CountTrees(lines, 3, 1);

        public long Part2(string input) => Part2(input.SplitNonEmptyLines());
        private static long Part2(string[] lines)
            => CountTrees(lines, 1, 1) *
                CountTrees(lines, 3, 1) *
                CountTrees(lines, 5, 1) *
                CountTrees(lines, 7, 1) *
                CountTrees(lines, 1, 2);

        private static long CountTrees(string[] lines, int xStep, int yStep)
        {
            long count = 0;

            for (int x = 0, y = 0; y < lines.Length; x += xStep, y += yStep)
            {
                if (lines[y][x % lines[y].Length] == Tree)
                {
                    count++;
                }
            }

            return count;
        }
    }
}
