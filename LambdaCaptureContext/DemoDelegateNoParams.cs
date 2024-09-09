using BenchmarkDotNet.Attributes;

namespace LambdaCaptureContext;

/// <summary>
/// https://www.meziantou.net/performance-lambda-expressions-method-groups-and-delegate-caching.htm
/// </summary>
[MemoryDiagnoser]
public class DemoDelegateNoParams
{
    Func<int> _instanceMg;
    Func<int> _instanceLa;
    Func<int> _instanceStaticLa;

    [Benchmark]
    public int Normal_Call()
    {
        return GetInt();
    }

    [Benchmark]
    public int Lambda_Only()
    {
        return DelegateCall(() => GetInt());
    }

    [Benchmark]
    public int Lambda_Static_Only()
    {
        return DelegateCall(static () => GetInt());
    }

    [Benchmark]
    public int Method_Group()
    {
        return DelegateCall(GetInt);
    }

    [Benchmark]
    public int Lambda_Cached()
    {
        if (_instanceLa == null)
        {
            _instanceLa = () => GetInt();
        }
        return DelegateCall(_instanceLa);
    }

    [Benchmark]
    public int Lambda_Static_Cached()
    {
        if (_instanceStaticLa == null)
        {
            _instanceStaticLa = static () => GetInt();
        }
        return DelegateCall(_instanceStaticLa);
    }

    [Benchmark]
    public int Method_Group_Cached()
    {
        if(_instanceMg == null)
        {
            _instanceMg = GetInt;
        }
        return DelegateCall(_instanceMg);
    }

    public static int GetInt() => 3;

    int DelegateCall(Func<int> action) 
    {
        return action();
    }
}
