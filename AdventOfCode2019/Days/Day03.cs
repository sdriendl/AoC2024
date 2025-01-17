using AdventOfCode.Common;

namespace AdventOfCode2019.Days;

public sealed class Day03 : CustomInputPathBaseDay
{
    public List<List<string>> _wires;

    protected override void Initialize()
    {
        _wires = File.ReadAllLines(InputFilePath)
            .Select(l => l.Split(",").ToList())
            .ToList();
    }

    public async override ValueTask<string> Solve_1()
    {
        var wire1 = new HashSet<(int X, int Y)>();
        var wire2 = new HashSet<(int X, int Y)>();

        (int X, int Y) s = (0, 0);
        foreach (var inst in _wires[0])
        {
            var n = int.Parse(inst[1..]);
            for (int i = 0; i < n; i++)
            {
                s = inst[0] switch
                {
                    'R' => (s.X + 1, s.Y),
                    'L' => (s.X - 1, s.Y),
                    'D' => (s.X, s.Y + 1),
                    'U' => (s.X, s.Y - 1),
                };
                wire1.Add(s);
            }
        }
        s = (0, 0);
        foreach (var inst in _wires[1])
        {
            var n = int.Parse(inst[1..]);
            for (int i = 0; i < n; i++)
            {
                s = inst[0] switch
                {
                    'R' => (s.X + 1, s.Y),
                    'L' => (s.X - 1, s.Y),
                    'D' => (s.X, s.Y + 1),
                    'U' => (s.X, s.Y - 1),
                };
                wire2.Add(s);
            }
        }
        var intersections = wire1.Intersect(wire2);
        return wire1.Intersect(wire2).Min(t => Math.Abs(t.X) + Math.Abs(t.Y)).ToString();
    }

    public async override ValueTask<string> Solve_2()
    {
        var wire1 = new HashSet<(int X, int Y)>();
        var wire2 = new HashSet<(int X, int Y)>();
        var wire1Steps = new Dictionary<(int X, int Y), int>();
        var wire2Steps = new Dictionary<(int X, int Y), int>();

        (int X, int Y) s = (0, 0);
        var steps = 0;
        foreach (var inst in _wires[0])
        {
            var n = int.Parse(inst[1..]);
            for (int i = 0; i < n; i++)
            {
                steps++;
                s = inst[0] switch
                {
                    'R' => (s.X + 1, s.Y),
                    'L' => (s.X - 1, s.Y),
                    'D' => (s.X, s.Y + 1),
                    'U' => (s.X, s.Y - 1),
                };
                wire1.Add(s);
                if (!wire1Steps.TryGetValue(s, out _))
                {
                    wire1Steps[s] = steps;
                }
            }
        }
        s = (0, 0);
        steps = 0;
        foreach (var inst in _wires[1])
        {
            var n = int.Parse(inst[1..]);
            for (int i = 0; i < n; i++)
            {
                steps++;
                s = inst[0] switch
                {
                    'R' => (s.X + 1, s.Y),
                    'L' => (s.X - 1, s.Y),
                    'D' => (s.X, s.Y + 1),
                    'U' => (s.X, s.Y - 1),
                };
                wire2.Add(s);
                if (!wire2Steps.TryGetValue(s, out _))
                {
                    wire2Steps[s] = steps;
                }
            }
        }
        var intersections = wire1.Intersect(wire2);
        return wire1.Intersect(wire2).Min(t => wire1Steps[t] + wire2Steps[t]).ToString();
    }
}
