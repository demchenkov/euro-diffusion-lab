namespace Console.Entities;

public class MapGrid
{ 
    Country[] countries;
    GridDictionary countriesGrid = new GridDictionary();

    int minX = 0;
    int minY = 0;
    int maxX = 0;
    int maxY = 0;

    public MapGrid(Country[] countries) 
    {
        this.countries = countries;

        foreach (var country in countries)
        {
            minX = Math.Min(minX, country.Coordinates.Xl);
            minY = Math.Min(minY, country.Coordinates.Yl);
            maxX = Math.Max(maxX, country.Coordinates.Xh);
            maxY = Math.Max(maxY, country.Coordinates.Yh);
        }

        AddCitiesToCountries();
        AddNeighborsToCities();
    }

    public bool IsCompleted => countries.All(country => country.IsCalculationFinished);

    public Dictionary<string, int> StartDiffusionEmulation()
    {
        countriesGrid = new GridDictionary();
        var result = new Dictionary<string, int>();
        var currentDay = 0;
  
        while (!IsCompleted) 
        {
            foreach (var city in countries.SelectMany(x => x.Cities))
            {
                city.TransportCoinsToNeighbors();

                if (city.Country.IsCalculationFinished && !result.ContainsKey(city.CountryName)) {
                    result[city.CountryName] = currentDay;
                }
            }

            foreach (var city in countries.SelectMany(x => x.Cities))
                city.UpdateCoins();
            
            currentDay += 1;
        }

        foreach (var country in countries)
        {
            if (!result.ContainsKey(country.Name)) {
                result[country.Name] = currentDay;
            }
        }

        return result;
    }
    
    private void AddCitiesToCountries() 
    {
        var coinTypes = countries.Select(country => country.Name).ToArray();

        for (var i = 0; i < countries.Length; i++)
        {
            var country = countries[i];
            AddCitiesToCountry(country, coinTypes, i);
        }
    }

    private void AddCitiesToCountry(Country country, string[] coinTypes, int countryIndex)
    {
        var (xl, yl, xh, yh) = country.Coordinates.AsTuple();
        
        for (int x = xl; x <= xh; x++)
        for (int y = yl; y <= yh; y++)
        {
            var city = new City(coinTypes, country);
            countriesGrid[x, y] = city;
            countries[countryIndex].AddCity(city);
        }
    }

    private void AddNeighborsToCities() 
    {
        for (var x = minX; x <= maxX; x++) 
        for (var y = minY; y <= maxY; y++) 
        {
            var city = countriesGrid[x, y];
            if (city is null) 
                continue;

            if (x < maxX)
            {
                city.TryAddNeighbor(countriesGrid[x + 1, y]);
            }
            if (x > minY) 
            {
                city.TryAddNeighbor(countriesGrid[x - 1, y]);
            }
            if (y < this.maxY) {
                city.TryAddNeighbor(countriesGrid[x, y + 1]);
            }
            if (y > this.minY) {
                city.TryAddNeighbor(countriesGrid[x, y - 1]);
            }
          
            if (countries.Length > 1 && city.Neighbors.Count == 0) 
            {
                throw new Exception($"City in {city.CountryName} has no neighbors");
            }
        }
    }
}