namespace Console.Entities;

public class CountryCoordinates 
{
    public CountryCoordinates(int xl, int yl, int xh, int yh)
    {
        if (!AreCoordinatesValid(xl, yl, xh, yh))
            throw new ArgumentException("Invalid coordinates");
        
        this.xl = xl; 
        this.yl = yl; 
        this.xh = xh; 
        this.yh = yh;
    }
    
    
    public int xl { get; }
    public int yl { get; }
    public int xh { get; }
    public int yh { get; }

    public (int xl, int yl, int xh, int yh) AsTuple() => (xl, yl, xh, yh);
    
    public static CountryCoordinates FromArray(IEnumerable<int> coordinates)
    {
        var coords = coordinates.ToArray();
        if (coords.Length != 4) 
            throw new ArgumentException("Incorrect count of coordinates: ${_coordinates.length}");

        return new CountryCoordinates(coords[0], coords[1], coords[2], coords[3]);
    }

    private static bool AreCoordinatesValid(int xl, int yl, int xh, int yh)
    {
        bool IsCorrectLowHighRange(double low, double high) => low <= high;
        bool IsCoordinateInBounds(double coordinate) => coordinate is >= 1 and <= 10;

        return (
            IsCoordinateInBounds(xl) &&
            IsCoordinateInBounds(yl) &&
            IsCoordinateInBounds(xh) &&
            IsCoordinateInBounds(yh) &&
            IsCorrectLowHighRange(xl, xh) &&
            IsCorrectLowHighRange(yl, yh));
    }
}