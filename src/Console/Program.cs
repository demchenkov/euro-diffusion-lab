using Console;
using Console.Entities;
using Console.Parsers;

var caseNumber = 1;
foreach (var caseString in FileInputParser.ReadAndParse(args[0]))
{
    System.Console.WriteLine($"Case Number {caseNumber++}");
    ProcessCase(caseString);
}

void ProcessCase(IEnumerable<string> countryStrings) 
{
    try 
    {
        var countries = countryStrings.Select(Country.GetCountryFromString).ToArray();
        var result = new MapGrid(countries).StartDiffusionEmulation();
        
        System.Console.WriteLine(result.DiffusionResultToString());
    } 
    catch (Exception e) 
    {
        System.Console.Error.WriteLine(e);
    }
};