namespace AdventOfCode2020
{
    [PuzzleInput("Day01.txt", 997899, 131248694)]
    [PuzzleInput("Day01-Sample.txt", 514579, 241861950)]
    public class Day01 : IDay
    {
        public long Part1(string input)
        {
            long[] lines = input.ParseLinesAsLong();

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = i + 1; j < lines.Length; j++)
                {
                    if (lines[i] + lines[j] == 2020)
                        return lines[i] * lines[j];
                }
            }

            return -1;
        }
        public long Part2(string input)
        {
            long[] lines = input.ParseLinesAsLong();

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = i + 1; j < lines.Length; j++)
                {
                    for (int k = j + 1; k < lines.Length; k++)
                    {
                        if (lines[i] + lines[j] + lines[k] == 2020)
                            return lines[i] * lines[j] * lines[k];
                    }
                }
            }

            return -1;
        }
    }
}
