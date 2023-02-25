namespace Pollit.Test.Common;

public interface IFluentBuilder<out T>
{
    public T Build();
}