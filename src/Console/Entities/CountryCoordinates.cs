namespace Console.Entities;

public class CountryCoordinates
{
    private const int MinCoordinate = 1;
    private const int MaxCoordinate = 10;
    private const int CoordinatesCount = 4;
    
    public CountryCoordinates(int xl, int yl, int xh, int yh)
    {
        if (!AreCoordinatesValid(xl, yl, xh, yh))
            throw new ArgumentException("Invalid coordinates");
        
        Xl = xl; 
        Yl = yl; 
        Xh = xh; 
        Yh = yh;
    }
    
    public int Xl { get; }
    public int Yl { get; }
    public int Xh { get; }
    public int Yh { get; }

    public (int xl, int yl, int xh, int yh) AsTuple() => (Xl, Yl, Xh, Yh);
    
    public static CountryCoordinates FromArray(IEnumerable<int> coordinates)
    {
        var coords = coordinates.ToArray();
        if (coords.Length != CoordinatesCount) 
            throw new ArgumentException($"Incorrect count of coordinates: {coords.Length}");

        return new CountryCoordinates(coords[0], coords[1], coords[2], coords[3]);
    }

    private static bool AreCoordinatesValid(int xl, int yl, int xh, int yh)
    {
        bool IsCorrectLowHighRange(double low, double high) => low <= high;
        bool IsCoordinateInBounds(double coordinate) => coordinate is >= MinCoordinate and <= MaxCoordinate;

        return (
            IsCoordinateInBounds(xl) &&
            IsCoordinateInBounds(yl) &&
            IsCoordinateInBounds(xh) &&
            IsCoordinateInBounds(yh) &&
            IsCorrectLowHighRange(xl, xh) &&
            IsCorrectLowHighRange(yl, yh));
    }
}