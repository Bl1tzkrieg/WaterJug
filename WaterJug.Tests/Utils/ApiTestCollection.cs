using Xunit;
using WaterJug.Tests.Utils;

namespace WaterJug.Tests.Utils;

/// <summary>
/// A shared test collection for API integration tests using CustomWebApplicationFactory
/// </summary>
[CollectionDefinition("ApiTestCollection")]
public class ApiTestCollection : ICollectionFixture<CustomWebApplicationFactory>
{
}