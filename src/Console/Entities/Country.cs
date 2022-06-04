namespace Console.Entities;

public class Country
{
    private const int MaxNameLength = 25;
    
    private readonly List<City> _cities;

    
    public Country(string name, CountryCoordinates coordinates) {
        if (name.Length > MaxNameLength) 
            throw new ArgumentException($"Name must be less than {MaxNameLength} characters");

        _cities = new List<City>();
        Name = name;
        Coordinates = coordinates;
    }
    
    public string Name { get; }    
    public CountryCoordinates Coordinates { get; }
    public IReadOnlyList<City> Cities => _cities.AsReadOnly();

    public void AddCity(City city) => _cities.Add(city);

    public bool IsCalculationFinished => _cities.All(city => city.IsCalculationFinished);

    public static Country GetCountryFromString(string countryString)
    {
        var parts = countryString.Split(' ');
        var name = parts.First();
        var coordinates = parts[1..];
        
        var coordinatesArray = coordinates.Select(int.Parse);
        return new Country(name, CountryCoordinates.FromArray(coordinatesArray));
    }
}