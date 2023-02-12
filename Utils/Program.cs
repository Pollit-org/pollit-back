// See https://aka.ms/new-console-template for more information

using System.Reflection;
using Pollit.Application;

var errors =
    typeof(ApplicationError)
        .GetFields(BindingFlags.Public | BindingFlags.Static)
        .Where(f => f.FieldType == typeof(string))
        .Select(f => new { name = f.Name, Value = f.GetValue(null) })
        .OrderBy(x => x.Value);

Console.WriteLine("export type ApiError = ");
Console.Write("    ");
Console.Write(string.Join($" |{Environment.NewLine}    ", errors.Select(e => $"\"{e.Value}\"")));
Console.Write(";");
