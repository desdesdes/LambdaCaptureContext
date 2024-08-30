using BenchmarkDotNet.Attributes;
using Microsoft.Diagnostics.Runtime;

namespace LambdaCaptureContext;

/// <summary>
/// https://www.meziantou.net/performance-lambda-expressions-method-groups-and-delegate-caching.htm
/// </summary>
public class DemoDelegateParams
{
    int _factor = 2;
    FastDelegateHelper _fastDelegateHelperStaticLa;
    FastDelegateHelper _fastDelegateHelperMg;
    FastDelegateHelper _fastDelegateHelperLa;

    [Benchmark]
    public void Normal_Call()
    {
        int factor = _factor;
        var y = GetInt(factor);
    }

    [Benchmark]
    public void Lambda_With_IntParam()
    {
        int factor = _factor;
        DelegateCallIntParam(factor, (x) => GetInt(x));
    }

    [Benchmark]
    public void Lambda_Static_With_IntParam()
    {
        int factor = _factor;
        DelegateCallIntParam(factor, static (x) => GetInt(x));
    }

    [Benchmark]
    public void Lambda_With_CastedObjectParam()
    {
        object factor = _factor;
        DelegateCallObjectParam(factor, (x) => GetInt((int)x));
    }

    [Benchmark]
    public void Lambda_With_CastedObjectParam_Static()
    {
        object factor = _factor;
        DelegateCallObjectParam(factor, static (x) => GetInt((int)x));
    }

    [Benchmark(Baseline = true)]
    public void Lambda_Captured_Local_Var()
    {
        int factor = _factor;

        DelegateCall(() => GetInt(factor));
    }

    [Benchmark]
    public void Method_Group_Captured_Local_Function_Var()
    {
        int factor = _factor;

        DelegateCall(CalculateLocal);

        int CalculateLocal()
        {
            return GetInt(factor);
        }
    }

    [Benchmark]
    public void Lambda_Captured_Class_Var()
    {
        int factor = _factor;

        DelegateCall(() => GetInt(_factor));
    }

    [Benchmark]
    public void Method_Group_Captured_Class_Var()
    {
        int factor = _factor;

        DelegateCall(GetIntFromClassVar);
    }

    [Benchmark]
    public void Lambda_Captured_Local_Var_Cached()
    {
        int factor = _factor;

        if(_fastDelegateHelperLa == null)
        {
            _fastDelegateHelperLa = new FastDelegateHelper((x) => GetInt(x));
        }

        _fastDelegateHelperLa.StateValue = factor;
        var result = _fastDelegateHelperLa.CachedDelegate();
    }

    [Benchmark]
    public void Lambda_Static_Captured_Local_Var_Cached()
    {
        int factor = _factor;

        if (_fastDelegateHelperStaticLa == null)
        {
            _fastDelegateHelperStaticLa = new FastDelegateHelper(static (x) => GetInt(x));
        }

        _fastDelegateHelperStaticLa.StateValue = factor;
        var result = _fastDelegateHelperStaticLa.CachedDelegate();
    }

    [Benchmark]
    public void Method_Group_Captured_Local_Var_Cached()
    {
        int factor = _factor;

        if (_fastDelegateHelperMg == null)
        {
            _fastDelegateHelperMg = new FastDelegateHelper(GetInt);
        }

        _fastDelegateHelperMg.StateValue = factor;
        var result = _fastDelegateHelperMg.CachedDelegate();
    }

    public int GetIntFromClassVar()
    {
        return GetInt(_factor);
    }

    public static int GetInt(int value) => value * 5;

    void DelegateCall(Func<int> action)
    {
        var y = action();
    }

    void DelegateCallIntParam(int value, Func<int, int> action)
    {
        var y = action(value);
    }

    void DelegateCallObjectParam(object value, Func<object, int> action)
    {
        var y = action(value);
    }
}
