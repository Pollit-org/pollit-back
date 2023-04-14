using System.Reflection;

namespace Pollit.SeedWork;

public static class AssemblyExtensions
{
    public static async Task<string> ReadResourceAsync(this Assembly assembly, string name)
    {
        var resourcePath = name;
        resourcePath = assembly.GetManifestResourceNames().Single(str => str.EndsWith(name));

        await using var stream = assembly.GetManifestResourceStream(resourcePath)!;
        using StreamReader reader = new(stream);
        return await reader.ReadToEndAsync();
    }
    
    public static string ReadResource(this Assembly assembly, string name)
    {
        var resourcePath = name;
        resourcePath = assembly.GetManifestResourceNames().Single(str => str.EndsWith(name));

        using var stream = assembly.GetManifestResourceStream(resourcePath)!;
        using StreamReader reader = new(stream);
        return reader.ReadToEnd();
    }
}