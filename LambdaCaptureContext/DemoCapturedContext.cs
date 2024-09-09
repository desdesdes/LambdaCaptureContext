using BenchmarkDotNet.Attributes;
using Microsoft.Diagnostics.Runtime;

namespace LambdaCaptureContext;

/// <summary>
/// https://www.meziantou.net/performance-lambda-expressions-method-groups-and-delegate-caching.htm
/// </summary>
[MemoryDiagnoser]
public class DemoDelegateParams
{
    int _factor = 2;
    FastDelegateHelper _fastDelegateHelperStaticLa;
    FastDelegateHelper _fastDelegateHelperMg;
    FastDelegateHelper _fastDelegateHelperLa;

    [Benchmark]
    public int Normal_Call()
    {
        int factor = _factor;
        return GetInt(factor);
    }

    [Benchmark]
    public int Lambda_With_IntParam()
    {
        int factor = _factor;
        return DelegateCallIntParam(factor, (x) => GetInt(x));
    }

    [Benchmark]
    public int Lambda_Static_With_IntParam()
    {
        int factor = _factor;
        return DelegateCallIntParam(factor, static (x) => GetInt(x));
    }

    [Benchmark]
    public int Lambda_With_CastedObjectParam()
    {
        object factor = _factor;
        return DelegateCallObjectParam(factor, (x) => GetInt((int)x));
    }

    [Benchmark]
    public int Lambda_With_CastedObjectParam_Static()
    {
        object factor = _factor;
        return DelegateCallObjectParam(factor, static (x) => GetInt((int)x));
    }

    [Benchmark(Baseline = true)]
    public int Lambda_Captured_Local_Var()
    {
        int factor = _factor;

        return DelegateCall(() => GetInt(factor));
    }

    [Benchmark]
    public int Method_Group_Captured_Local_Function_Var()
    {
        int factor = _factor;

        return DelegateCall(CalculateLocal);

        int CalculateLocal()
        {
            return GetInt(factor);
        }
    }

    [Benchmark]
    public int Lambda_Captured_Class_Var()
    {
        int factor = _factor;

        return DelegateCall(() => GetInt(_factor));
    }

    [Benchmark]
    public int Method_Group_Captured_Class_Var()
    {
        int factor = _factor;

        return DelegateCall(GetIntFromClassVar);
    }

    [Benchmark]
    public int Lambda_Captured_Local_Var_Cached()
    {
        int factor = _factor;

        if(_fastDelegateHelperLa == null)
        {
            _fastDelegateHelperLa = new FastDelegateHelper((x) => GetInt(x));
        }

        _fastDelegateHelperLa.StateValue = factor;
        return _fastDelegateHelperLa.CachedDelegate();
    }

    [Benchmark]
    public int Lambda_Static_Captured_Local_Var_Cached()
    {
        int factor = _factor;

        if (_fastDelegateHelperStaticLa == null)
        {
            _fastDelegateHelperStaticLa = new FastDelegateHelper(static (x) => GetInt(x));
        }

        _fastDelegateHelperStaticLa.StateValue = factor;
        return _fastDelegateHelperStaticLa.CachedDelegate();
    }

    [Benchmark]
    public int Method_Group_Captured_Local_Var_Cached()
    {
        int factor = _factor;

        if (_fastDelegateHelperMg == null)
        {
            _fastDelegateHelperMg = new FastDelegateHelper(GetInt);
        }

        _fastDelegateHelperMg.StateValue = factor;
        return _fastDelegateHelperMg.CachedDelegate();
    }

    public int GetIntFromClassVar()
    {
        return GetInt(_factor);
    }

    public static int GetInt(int value) => value * 5;

    int DelegateCall(Func<int> action)
    {
        return action();
    }

    int DelegateCallIntParam(int value, Func<int, int> action)
    {
        return action(value);
    }

    int DelegateCallObjectParam(object value, Func<object, int> action)
    {
        return action(value);
    }
}
