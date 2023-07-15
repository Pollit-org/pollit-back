namespace Pollit.SeedWork;

public static class DictionaryExtensions
{
    public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, Func<TKey, TValue> valueGenerator)
    {
        if (dict.TryGetValue(key, out TValue value))
            return value;
        
        value = valueGenerator(key);

        dict.Add(key, value);

        return value;
    }
    
    public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, Func<TValue> valueGenerator)
    {
        return dict.GetOrAdd(key, (_) => valueGenerator());
    }
    
    public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue valueToAdd)
    {
        return dict.GetOrAdd(key, () => valueToAdd);
    }
}