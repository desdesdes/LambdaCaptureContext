using BenchmarkDotNet.Attributes;

namespace LambdaCaptureContext;

[DisassemblyDiagnoser]
[MemoryDiagnoser(displayGenColumns: false)]
[HideColumns("Job", "Error", "Median", "RatioSD")]
public class Net9vs10
{
    [Benchmark]
    public int ArrayLoopWithDelegateCall()
    {
        int local = 1;
        IEnumerable<int> arr = new int[] { 1, 2, 3 };
        var func = (int x) => x + local;
        int sum = 0;

        foreach (int num in arr)
        {
            sum += func(num);
        }

        return sum;
    }
}
