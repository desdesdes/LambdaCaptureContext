using BenchmarkDotNet.Running;
using System.Diagnostics.CodeAnalysis;

namespace LambdaCaptureContext;

internal class Program
{
    static void Main(string[] args)
    {
        BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        //MutableStructSample();
    }

    static void MutableStructSample()
    {
        var s = new MyMutableStruct { Value = 1 };

        // Using a Method Group
        Console.WriteLine(s.Value); // 1
        DoSomething(s.Mutate);
        Console.WriteLine(s.Value); // 1 ⚠️ the value is not changed

        // Using a Lambda expression
        Console.WriteLine(s.Value); // 1
        DoSomething(() => s.Mutate());
        Console.WriteLine(s.Value); // 2 ✅ the value is updated
    }

    static void DoSomething(Action a) => a();
}
