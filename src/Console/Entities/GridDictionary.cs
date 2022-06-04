namespace Console.Entities;

public class GridDictionary
{
    private readonly Dictionary<string, City> _cities = new ();

    public City? this[int x, int y]
    {
        get => _cities.GetValueOrDefault(GetKey(x, y));
        set => _cities[GetKey(x, y)] = value!;
    }

    private static string GetKey(int x, int y) => $"{x}-{y}";
}