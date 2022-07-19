namespace Console.Parsers;

public static class FileInputParser
{
    public static List<List<string>> ReadAndParse(string? path)
    {
        path = path ?? throw new ArgumentNullException(path);

        if (!File.Exists(path))
            throw new ArgumentException("File with defined path doesn't exists", nameof(path));
        
        var lines = File.ReadLines(path).ToArray();
        
        if (lines[^1] != "0") 
            throw new Exception("Input file must end with '0' line");

        var countryStrings = new List<List<string>>();
        var lineIndex = 0;
        while (lineIndex < lines.Length - 2) 
        {
            var currentLine = lines[lineIndex];
            var parsed = int.TryParse(currentLine, out var countryNumber);

            if (!parsed)
                throw new Exception($"Error in input file at '${lines[lineIndex]}'. Expected a number of countries");

            lineIndex += 1; // move to first country line
            var countries = new List<string>();
            for (
                var countryLineIndex = lineIndex;
                countryLineIndex < countryNumber + lineIndex;
                countryLineIndex++
            ) 
            {
                countries.Add(lines[countryLineIndex]);
            }
            lineIndex += countryNumber; // move to next number of countries
            countryStrings.Add(countries);
        }

        return countryStrings;
    }
}