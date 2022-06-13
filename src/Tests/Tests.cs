using Console.Entities;

namespace Tests;

public class Tests
{
    [Fact]
    public void ResultShouldContainsAllKeys()
    {
        var diffusionResult = new MapGrid(new []
        {
            new Country("France", CountryCoordinates.FromArray(1, 4, 4, 6)),
            new Country("Spain", CountryCoordinates.FromArray(3, 1, 6, 3)),
            new Country("Portugal", CountryCoordinates.FromArray(1, 1, 2, 2))
        }).StartDiffusionEmulation();
        
        Assert.Equal(new []{ "France", "Portugal", "Spain" }, diffusionResult.Keys.OrderBy(x => x).ToArray());
    }
    
    [Fact]
    public void ShouldThrowWhenCountryWithoutNeighbors()
    {
        Assert.Throws<Exception>(() =>
        {
            new MapGrid(new[]
            {
                new Country("Test 1", CountryCoordinates.FromArray(1, 1, 1, 1)),
                new Country("Test 2", CountryCoordinates.FromArray(3, 3, 3, 3)),
            }).StartDiffusionEmulation();
        });
    }

    [Fact]
    public void ShouldThrowExceptionWhenInvalidCoordinates()
    {
        Assert.Throws<ArgumentException>(() => CountryCoordinates.FromArray(11, 1, 1, 1));
        Assert.Throws<ArgumentException>(() => CountryCoordinates.FromArray(-1, 1, 1, 1));
        Assert.Throws<ArgumentException>(() => CountryCoordinates.FromArray(1, 1, 34, 4));
        Assert.Throws<ArgumentException>(() => CountryCoordinates.FromArray(1, 2, 3, 0));
    }
    
    [Fact]
    public void ShouldWorksProperly1()
    {
        var diffusionResult = new MapGrid(new []
        {
            new Country("France", CountryCoordinates.FromArray(1, 4, 4, 6)),
            new Country("Spain", CountryCoordinates.FromArray(3, 1, 6, 3)),
            new Country("Portugal", CountryCoordinates.FromArray(1, 1, 2, 2)),
        }).StartDiffusionEmulation();
        
        Assert.Equal(382, diffusionResult["Spain"]);
        Assert.Equal(416, diffusionResult["Portugal"]);
        Assert.Equal(1325, diffusionResult["France"]);
    }
    
    [Fact]
    public void ShouldWorksProperly2()
    {
        var diffusionResult = new MapGrid(new []
        {
            new Country("Luxembourg", CountryCoordinates.FromArray(1, 1, 1, 1)),
        }).StartDiffusionEmulation();
        
        Assert.Equal(0, diffusionResult["Luxembourg"]);
    }
    
    [Fact]
    public void ShouldWorksProperly3()
    {
        var diffusionResult = new MapGrid(new []
        {
            new Country("Netherlands", CountryCoordinates.FromArray(1, 3, 2, 4)),
            new Country("Belgium", CountryCoordinates.FromArray(1, 1, 2, 2)),
        }).StartDiffusionEmulation();
        
        Assert.Equal(2, diffusionResult["Netherlands"]);
        Assert.Equal(2, diffusionResult["Belgium"]);
    }
    
}