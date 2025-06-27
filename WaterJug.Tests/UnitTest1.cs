namespace WaterJug.Tests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using WaterJug.Data;
using WaterJug.Models.Responses;
using WaterJug.Services;
using Xunit;


public class UnitTest1
{
    /// <summary>
    /// Tests that SolveProblem returns a Success response with a valid solution
    /// </summary>
    [Fact]
    public async Task SolveProblem_FindsSolution_ReturnsSuccess()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDb1")
            .Options;

        await using var context = new AppDbContext(options);
        var service = new WaterJugService(context);

        var response = await service.SolveProblem(2, 10, 4);

        Assert.NotNull(response);
        Assert.True(response.success);
        Assert.Equal("Success", response.message);
        Assert.NotEmpty(response.solution);
    }

    /// <summary>
    /// Tests that SolveProblem returns an error response when a solution isn't possible
    /// </summary>
    [Fact]
    public async Task SolveProblem_NoSolution_ReturnsError()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDb1")
            .Options;

        await using var context = new AppDbContext(options);
        var service = new WaterJugService(context);

        var response = await service.SolveProblem(2, 6, 5);

        Assert.NotNull(response);
        Assert.False(response.success);
        Assert.Equal("No valid solution found.", response.message);
        Assert.Empty(response.solution);
    }
}
