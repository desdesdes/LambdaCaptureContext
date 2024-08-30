namespace LambdaCaptureContext;

public struct MyMutableStruct
{
    public int Value;

    public void Mutate() => Value = 2;
}