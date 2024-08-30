namespace LambdaCaptureContext;

internal class FastDelegateHelper
{
    private readonly Func<int, int> _actualDelegate;

    public int StateValue { get; set; }

    public Func<int> CachedDelegate { get; }

    public FastDelegateHelper(Func<int, int> actualDelegate)
    {
        _actualDelegate = actualDelegate;
        CachedDelegate = CallMe;
    }

    private int CallMe()
    {
        return _actualDelegate(StateValue);
    }
}
