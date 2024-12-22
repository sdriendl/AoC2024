using System.Collections.Concurrent;
using AdventOfCode.Common;
using MoreLinq;

namespace AdventOfCode2024.Days;

public sealed class Day22 : CustomInputPathBaseDay
{
    private List<int> _input;
    public int N { get; set; } = 2000;

    public Day22()
    {
        Initialize();
    }

    protected override void Initialize()
    {
        _input = File.ReadAllLines(InputFilePath)
                     .Select(int.Parse)
                     .ToList();
    }

    public override async ValueTask<string> Solve_1()
    {
        return _input.Sum(seed => NthSecretNumber(seed, N))
                     .ToString();
    }

    public override async ValueTask<string> Solve_2()
    {
        var dict = new ConcurrentDictionary<int,int>();

        Parallel.ForEach(_input, seed =>
        {
            AddSellPrices(dict, seed, N);
        });
        return dict.Max(d => d.Value).ToString();
    }

    private void AddSellPrices(
        ConcurrentDictionary<int, int> dict,
        int seed, 
        int n)
    {
        var seen = new HashSet<long>();
        var secrets = NSecretNumbers(seed, n);
        foreach (var window in secrets.Window(5))
        {
            var deltas =
                (
                    window[1] % 10 - window[0] % 10,
                    window[2] % 10 - window[1] % 10,
                    window[3] % 10 - window[2] % 10,
                    window[4] % 10 - window[3] % 10
                );
            if (seen.Add(DeltasKey(deltas)))
            {
                var rp = window[4] % 10;
                dict.AddOrUpdate(DeltasKey(deltas), rp, (k,v) => v + rp);
            }
        }

        int DeltasKey((int,int,int,int) deltas)
        {
            return 6859 * deltas.Item1 + 361 * deltas.Item2 + 19 * deltas.Item3 + deltas.Item4;
        }
    }

    private int NextSecretNumber(int prev)
    {
        prev ^= prev << 6 & 0xFFFFFF;
        prev ^= prev >> 5 & 0xFFFFFF;
        prev ^= prev << 11 & 0xFFFFFF;

        return prev;
    }

    private IEnumerable<int> NSecretNumbers(int seed, int n)
    {
        var next = seed;
        yield return next;
        for (int i = 0; i < n; i++)
        {
            next = NextSecretNumber(next);
            yield return next;
        }
    }

    private long NthSecretNumber(int seed, int n)
    {
        for (int i = 0; i < n; i++)
        {
            seed = NextSecretNumber(seed);
        }

        return seed;
    }
}