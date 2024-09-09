using BenchmarkDotNet.Attributes;

namespace LambdaCaptureContext;

[MemoryDiagnoser]
public class MethodGroupVsLambda
{
    [Benchmark]
    public int Lambda_Capture()
    {
        int result = 0;

        for (int i = 0; i < 10; i++)
        {
            result += DelegateCall(() => i + i);
        }

        return result;
    }

    [Benchmark]
    public int MethodGroup_Capture()
    {
        int result = 0;

        for (int i = 0; i < 10; i++)
        {
            result += DelegateCall(Add);

            int Add() => i + i;
        }

        return result;
    }

    [Benchmark]
    public int Lambda()
    {
        int result = 0;

        for (int i = 0; i < 10; i++)
        {
            result += DelegateCall(i, j => j + j);
        }

        return result;
    }

    [Benchmark]
    public int MethodGroup()
    {
        int result = 0;

        for (int i = 0; i < 10; i++)
        {
            result += DelegateCall(i, Add);
        }

        return result;

        int Add(int j) => j + j;
    }

    [Benchmark]
    public int MethodGroup_Static()
    {
        int result = 0;

        for (int i = 0; i < 10; i++)
        {
            result += DelegateCall(i, Add);
        }

        return result;

        static int Add(int j) => j + j;
    }


    int DelegateCall(Func<int> action) => action();
    int DelegateCall(int i, Func<int, int> action) => action(i);
}