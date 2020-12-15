using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AdventOfCode2020
{
    [PuzzleInput("Day04.txt", 245, 133)]
    [PuzzleInput("Day04-Sample01.txt", 2, 2)]
    [PuzzleInput("Day04-Sample02.txt", 8, 4)]
    public class Day04 : IDay
    {
        private const string BirthYear = "byr";
        private const string IssueYear = "iyr";
        private const string ExpiryYear = "eyr";
        private const string Height = "hgt";
        private const string HairColor = "hcl";
        private const string EyeColor = "ecl";
        private const string PassportId = "pid";

        private static Dictionary<string, string>[] ParsePassports(string input)
            => input.Split("\n\n", StringSplitOptions.RemoveEmptyEntries)
                    .Select(y => y.Split(new char[] { '\n', '\r', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(z => z.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries))
                        .ToDictionary(a => a[0], a => a[1])).ToArray();

        public long Part1(string input) => ParsePassports(input).Count(Part1Valid);

        private bool Part1Valid(Dictionary<string, string> passport)
            => passport.ContainsKey(BirthYear) &&
                passport.ContainsKey(IssueYear) &&
                passport.ContainsKey(ExpiryYear) &&
                passport.ContainsKey(Height) &&
                passport.ContainsKey(HairColor) &&
                passport.ContainsKey(EyeColor) &&
                passport.ContainsKey(PassportId);

        public long Part2(string input) => ParsePassports(input).Count(Part2Valid);

        private bool Part2Valid(Dictionary<string, string> passport)
            => ValidBirthYear(passport) &&
                ValidIssueYear(passport) &&
                ValidExpiryYear(passport) &&
                ValidHeight(passport) &&
                ValidHair(passport) &&
                ValidEyes(passport) &&
                ValidPassportId(passport);

        private static bool ValidBirthYear(Dictionary<string, string> passport)
            => ValidInRange(passport, BirthYear, 1920, 2002);

        private static bool ValidIssueYear(Dictionary<string, string> passport)
            => ValidInRange(passport, IssueYear, 2010, 2020);

        private static bool ValidExpiryYear(Dictionary<string, string> passport)
            => ValidInRange(passport, ExpiryYear, 2020, 2030);
        private static bool ValidInRange(Dictionary<string, string> passport, string key, int min, int max)
            => passport.ContainsKey(key) && int.TryParse(passport[key], out int val) && val >= min && val <= max;

        private static bool ValidHeight(Dictionary<string, string> passport)
            => passport.ContainsKey(Height) && passport[Height] switch
            {
                string inch when inch.EndsWith("in") && int.TryParse(inch[0..^2], out int x) && x >= 59 && x <= 76 => true,
                string cm when cm.EndsWith("cm") && int.TryParse(cm[0..^2], out int x) && x >= 150 && x <= 193 => true,
                _ => false
            };

        private static bool ValidHair(Dictionary<string, string> passport)
            => passport.ContainsKey(HairColor) && passport[HairColor].Length == 7 && passport[HairColor].StartsWith('#') && int.TryParse(passport[HairColor][1..], NumberStyles.HexNumber, null, out int _);

        private static readonly string[] s_validEyeValues = new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

        private static bool ValidEyes(Dictionary<string, string> passport)
            => passport.ContainsKey(EyeColor) && s_validEyeValues.Contains(passport[EyeColor]);

        private static bool ValidPassportId(Dictionary<string, string> passport)
            => passport.ContainsKey(PassportId) && passport[PassportId].Length == 9 && int.TryParse(passport[PassportId], out int _);
    }
}
