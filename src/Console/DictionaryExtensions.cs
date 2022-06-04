namespace Console;

public static class DictionaryExtensions
{
    public static string DiffusionResultToString(this Dictionary<string, int> dictionary)
    {
        var results = new List<string>();

        var a = dictionary
            .OrderBy(x => x.Value)
            .ThenBy(x => x.Key)
            .Select(x => $"{x.Key} {x.Value}")
            .ToArray();
        
        return string.Join(Environment.NewLine, a);
    }
}