public abstract class Option<T>
{
    public abstract T Value { get; }
    public abstract bool HasValue { get; }
}

public class Some<T> : Option<T>
{
    private readonly T _value;

    public Some(T value)
    {
        _value = value;
    }

    public override T Value => _value;
    public override bool HasValue => true;
}

public class None<T> : Option<T>
{
    public override T Value => default!;
    public override bool HasValue => false;
}