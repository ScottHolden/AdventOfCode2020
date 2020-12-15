using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
    public static class PuzzleHelpers
    {
        public static IEnumerable<Type> GetDayTypes()
            => typeof(IDay).Assembly.GetTypes()
                    .Where(x => !x.IsAbstract && x.IsClass && x.IsAssignableTo(typeof(IDay)))
                    .OrderByDescending(x => int.Parse(x.Name[3..]));

        public static IDay? CreateInstance(Type t)
            => PuzzleDisplay.MeasureWithMessage(() => (IDay?)Activator.CreateInstance(t), t.Name + ".ctor");

        public static string ReadFile(string filename)
            => PuzzleDisplay.MeasureWithMessage(() => File.ReadAllText(filename), " " + Path.GetFileNameWithoutExtension(filename));
    }
}
