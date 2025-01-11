using System.Net.Http.Json;
using FluentAssertions;
using Infrastructure.ExternalApi;
using Tests.Integration.Infrastructure;

namespace Tests.Integration;

public class CreateLeaveTests : IClassFixture<ApiFactory>
{
    private readonly HttpClient client;
    private readonly Func<int, bool, Task> setupExternalApi;

    public CreateLeaveTests(ApiFactory apiFactory)
    {
        this.client = apiFactory.HttpClient;
        this.setupExternalApi = apiFactory.ExternalApi.SetupExternalApi;
    }


    [Fact]
    public async Task WhenGetLeave_ThenReturnsSuccess()
    {
        // Arrange
        var leaveId = 1;
        await this.setupExternalApi(leaveId, true);

        // Act
        var response = await client.GetAsync(ExternalApiEndpoints.Get(leaveId));

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        var externalApiResponse = await response.Content.ReadFromJsonAsync<ExternalApiResponse>();
        externalApiResponse.Should().NotBeNull();
        externalApiResponse!.Data.Should().Be("data");
    }
}
