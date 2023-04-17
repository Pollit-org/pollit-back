namespace Pollit.SeedWork;

public static class DictionaryExtensions
{
    public static TValue GetOrAdd<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, Func<TKey, TValue> valueGenerator)
    {
        if (dict.TryGetValue(key, out TValue value))
            return value;
        
        value = valueGenerator(key);

        dict.Add(key, value);

        return value;
    }
    
    public static TValue GetOrAdd<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue valueToAdd)
    {
        if (dict.TryGetValue(key, out TValue value))
            return value;

        dict.Add(key, valueToAdd);

        return valueToAdd;
    }
}