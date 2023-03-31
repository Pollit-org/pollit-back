namespace Pollit.SeedWork;

public static class Asyncator
{
    public static Task Async(this Action action)
    {
        action();
        return Task.CompletedTask;
    }
    
    public static Task Async<T1>(this Action<T1> action, T1 param1)
    {
        action(param1);
        return Task.CompletedTask;
    }
    
    public static Task Async<T1, T2>(this Action<T1, T2> action, T1 param1, T2 param2)
    {
        action(param1, param2);
        return Task.CompletedTask;
    }
    
    public static Task Async<T1, T2, T3>(this Action<T1, T2, T3> action, T1 param1, T2 param2, T3 param3)
    {
        action(param1, param2, param3);
        return Task.CompletedTask;
    }
    
    public static Task Async<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> action, T1 param1, T2 param2, T3 param3, T4 param4)
    {
        action(param1, param2, param3, param4);
        return Task.CompletedTask;
    }
    
    public static Task Async<T1, T2, T3, T4, T5>(this Action<T1, T2, T3, T4, T5> action, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
    {
        action(param1, param2, param3, param4, param5);
        return Task.CompletedTask;
    }
    
    public static Task<T> Async<T>(this Func<T> function)
    {
        return Task.FromResult(function());
    }
    
    public static Task<TReturn> Async<TParam1, TReturn>(this Func<TParam1, TReturn> function, TParam1 param1)
    {
        return Task.FromResult(function(param1));
    }

    public static Task<TReturn> Async<TParam1, TParam2, TReturn>(this Func<TParam1, TParam2, TReturn> function, TParam1 param1, TParam2 param2)
    {
        return Task.FromResult(function(param1, param2));
    }
    
    public static Task<TReturn> Async<TParam1, TParam2, TParam3, TReturn>(this Func<TParam1, TParam2, TParam3, TReturn> function, TParam1 param1, TParam2 param2, TParam3 param3)
    {
        return Task.FromResult(function(param1, param2, param3));
    }
    
    public static Task<TReturn> Async<TParam1, TParam2, TParam3, TParam4, TReturn>(this Func<TParam1, TParam2, TParam3, TParam4, TReturn> function, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4)
    {
        return Task.FromResult(function(param1, param2, param3, param4));
    }
    
    public static Task<TReturn> Async<TParam1, TParam2, TParam3, TParam4, TParam5, TReturn>(this Func<TParam1, TParam2, TParam3, TParam4, TParam5, TReturn> function, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5)
    {
        return Task.FromResult(function(param1, param2, param3, param4, param5));
    }
}