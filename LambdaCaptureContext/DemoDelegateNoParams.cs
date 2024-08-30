using BenchmarkDotNet.Attributes;

namespace LambdaCaptureContext;

/// <summary>
/// https://www.meziantou.net/performance-lambda-expressions-method-groups-and-delegate-caching.htm
/// </summary>
public class DemoDelegateNoParams
{
    Func<int> _instanceMg;
    Func<int> _instanceLa;

    [Benchmark]
    public void Normal_Call()
    {
        var y = GetInt();
    }

    [Benchmark]
    public void Lambda_Only()
    {
        DelegateCall(() => GetInt());
    }

    [Benchmark]
    public void Lambda_Static_Only()
    {
        DelegateCall(static () => GetInt());
    }

    [Benchmark]
    public void Method_Group()
    {
        DelegateCall(GetInt);
    }

    [Benchmark]
    public void Lambda_Cached()
    {
        if (_instanceLa == null)
        {
            _instanceLa = () => GetInt();
        }
        DelegateCall(_instanceLa);
    }

    [Benchmark]
    public void Lambda_Static_Cached()
    {
        if (_instanceLa == null)
        {
            _instanceLa = static () => GetInt();
        }
        DelegateCall(_instanceLa);
    }

    [Benchmark]
    public void Method_Group_Cached()
    {
        if(_instanceMg == null)
        {
            _instanceMg = GetInt;
        }
        DelegateCall(_instanceMg);
    }

    public static int GetInt() => 3;

    void DelegateCall(Func<int> action) 
    {
        var y = action();
    }
}
