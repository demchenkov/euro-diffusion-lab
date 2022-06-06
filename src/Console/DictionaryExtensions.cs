namespace Console;

public static class DictionaryExtensions
{
    public static string DiffusionResultToString(this Dictionary<string, int> dictionary)
    {
        var keys = dictionary
            .OrderBy(x => x.Value)
            .ThenBy(x => x.Key)
            .Select(x => $"{x.Key} {x.Value}");
        
       return string.Join(Environment.NewLine, keys);
    }
}