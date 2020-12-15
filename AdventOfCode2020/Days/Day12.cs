using System;
using System.Numerics;

namespace AdventOfCode2020
{
    [PuzzleInput("Day12.txt", 1424, 63447)]
    [PuzzleInput("Day12-Sample.txt", 25, 286)]
    public class Day12 : IDay
    {
        public long Part1(string input)
        {
            double x = 0.0;
            double y = 0.0;
            int angle = 0;
            foreach (string s in input.SplitNonEmptyLines())
            {
                int value = int.Parse(s[1..]);
                if (s[0] == 'N')
                {
                    y -= value;
                }
                else if (s[0] == 'S')
                {
                    y += value;
                }
                else if (s[0] == 'E')
                {
                    x += value;
                }
                else if (s[0] == 'W')
                {
                    x -= value;
                }
                else if (s[0] == 'L')
                {
                    angle -= value;
                    if (angle < 0) angle += 360;
                }
                else if (s[0] == 'R')
                {
                    angle += value;
                    if (angle >= 360) angle -= 360;
                }
                else if (s[0] == 'F')
                {
                    x += value * Math.Cos(angle * Math.PI / 180.0);
                    y += value * Math.Sin(angle * Math.PI / 180.0);
                }
            }
            return (long)(Math.Abs(x) + Math.Abs(y));
        }
        public long Part2(string input)
        {
            Vector2 waypoint = new(10, -1);
            Vector2 ship = new();
            foreach (string s in input.SplitNonEmptyLines())
            {
                int value = int.Parse(s[1..]);
                (waypoint, ship) = s[0] switch
                {
                    'N' => (waypoint + new Vector2(0, -value), ship),
                    'S' => (waypoint + new Vector2(0, value), ship),
                    'E' => (waypoint + new Vector2(value, 0), ship),
                    'W' => (waypoint + new Vector2(-value, 0), ship),
                    'L' => Rotate(waypoint, ship, -value),
                    'R' => Rotate(waypoint, ship, value),
                    'F' => MoveForwards(waypoint, ship, value),
                    _ => throw new Exception("Woah!")
                };
            }

            return (long)(Math.Abs(ship.X) + Math.Abs(ship.Y));
        }
        private static (Vector2 Waypoint, Vector2 Ship) Rotate(Vector2 waypoint, Vector2 ship, int value)
            => (Vector2.Add(ship, Vector2.Transform(Vector2.Subtract(waypoint, ship), Quaternion.CreateFromAxisAngle(Vector3.UnitZ, value * MathF.PI / 180.0f))), ship);
        private static (Vector2 Waypoint, Vector2 Ship) MoveForwards(Vector2 waypoint, Vector2 ship, int value)
        {
            Vector2 travel = Vector2.Multiply(value, Vector2.Subtract(waypoint, ship));
            return (waypoint + travel, ship + travel);
        }
    }
}
