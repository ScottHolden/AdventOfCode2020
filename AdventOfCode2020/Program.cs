using AdventOfCode2020;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

const string DataPath = ".\\Data\\";
bool pauseOnNext = false;
Stopwatch wholeThing = Stopwatch.StartNew();

PuzzleDisplay.WriteOutput("Advent Of Code: 2020 - ScottDev");

foreach (Type t in PuzzleHelpers.GetDayTypes())
{
    PuzzleDisplay.WriteHeader(t.Name);
    try
    {
        IDay? day = PuzzleHelpers.CreateInstance(t);

        if (day is null) continue;

        foreach (PuzzleInputAttribute puzzle in t.GetCustomAttributes<PuzzleInputAttribute>()
                                                    .OrderByDescending(x => x.Filename.Length))
        {
            string input = PuzzleHelpers.ReadFile(Path.Combine(DataPath, puzzle.Filename));

            if (string.IsNullOrWhiteSpace(input))
            {
                PuzzleDisplay.WriteOutput("  Empty file found, skipping...");
                continue;
            }

            pauseOnNext |= !PuzzleDisplay.DisplayAnswer(day.Part1, input, puzzle.Part1Answer, puzzle.SkipPart1);
            pauseOnNext |= !PuzzleDisplay.DisplayAnswer(day.Part2, input, puzzle.Part2Answer, puzzle.SkipPart2);
        }
    }
    catch (Exception e)
    {
        pauseOnNext = true;
        PuzzleDisplay.WriteOutput(e.ToString());
    }

    if (pauseOnNext)
    {
        wholeThing.Stop();
        PuzzleDisplay.Pause();
        pauseOnNext = false;
        wholeThing.Start();
    }
}

PuzzleDisplay.WriteFooter();

wholeThing.Stop();

PuzzleDisplay.WriteOutput($"AoC 2020 Completed in {wholeThing.ElapsedMilliseconds} ms!");
