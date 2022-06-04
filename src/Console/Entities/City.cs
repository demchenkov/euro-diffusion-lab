namespace Console.Entities;

public class City
{
    private const int InitialCoinsCount = 1000000;
    private const int RepresentativePortion = InitialCoinsCount / 1000;
    
    private readonly string[] _coinTypes;
    private readonly List<City> _neighbors;
    private readonly double[] _coins;
    private readonly double[] _cache;
    private readonly double _representativePortion;

    public City(
        IEnumerable<string> coinTypes,
        Country country,
        int initialCoinsCount = InitialCoinsCount,
        int representativePortion = RepresentativePortion
    ) {
        Country = country;
        _coinTypes = coinTypes.ToArray();
        _neighbors = new List<City>();

        _coins = new double[_coinTypes.Length];
        _cache = new double[_coinTypes.Length];
        
        _representativePortion = representativePortion;

        var countryIndex = Array.IndexOf(_coinTypes, CountryName);
        _coins[countryIndex] = initialCoinsCount;
    }

    public string CountryName => Country.Name;
    public Country Country { get; }

    public IReadOnlyList<City> Neighbors => _neighbors.AsReadOnly();

    public bool IsCalculationFinished => _coins.All(coin => coin > 0);

    public void TransportCoinsToNeighbors() 
    {
        for (int i = 0; i < _coins.Length; i++)
        {
            var coinCount = _coins[i];
            var share = Math.Floor(coinCount / _representativePortion);

            foreach (var neighbor in _neighbors)
            {
                neighbor._cache[i] += share;
                _coins[i] -= share;
            }
        }
    }

    public void UpdateCoins() 
    {
        for (int index = 0; index < _coinTypes.Length; index++) {
            _coins[index] += _cache[index];
            _cache[index] = 0;
        }
    }

    public void TryAddNeighbor(City? neighbor)
    {
        if (neighbor is null) return;
        
        _neighbors.Add(neighbor);
    }
}