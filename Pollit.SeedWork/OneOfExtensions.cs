using OneOf;

namespace Pollit.SeedWork;

public static class OneOfExtensions
{
    public static Task SwitchAsync<T0, T1>(
        this OneOfBase<T0, T1> oneOf,
        Func<T0, Task> f0,
        Action<T1> f1)
    {
        return oneOf.Match(
            f0,
            t1 => { f1(t1); return Task.CompletedTask; }
        );
    }
    
    public static Task SwitchAsync<T0, T1, T2>(
        this OneOfBase<T0, T1, T2> oneOf,
        Func<T0, Task> f0,
        Action<T1> f1, 
        Action<T2> f2)
    {
        return oneOf.Match(
            f0,
            t1 => { f1(t1); return Task.CompletedTask; },
            t2 => { f2(t2); return Task.CompletedTask; }
        );
    }
    
    public static Task SwitchAsync<T0, T1, T2, T3>(
        this OneOfBase<T0, T1, T2, T3> oneOf,
        Func<T0, Task> f0,
        Action<T1> f1, 
        Action<T2> f2, 
        Action<T3> f3)
    {
        return oneOf.Match(
            f0,
            t1 => { f1(t1); return Task.CompletedTask; },
            t2 => { f2(t2); return Task.CompletedTask; },
            t3 => { f3(t3); return Task.CompletedTask; }
        );
    }
    
    public static Task SwitchAsync<T0, T1, T2, T3, T4>(
        this OneOfBase<T0, T1, T2, T3, T4> oneOf,
        Func<T0, Task> f0,
        Action<T1> f1, 
        Action<T2> f2, 
        Action<T3> f3,
        Action<T4> f4)
    {
        return oneOf.Match(
            f0,
            t1 => { f1(t1); return Task.CompletedTask; },
            t2 => { f2(t2); return Task.CompletedTask; },
            t3 => { f3(t3); return Task.CompletedTask; },
            t4 => { f4(t4); return Task.CompletedTask; }
        );
    }
}