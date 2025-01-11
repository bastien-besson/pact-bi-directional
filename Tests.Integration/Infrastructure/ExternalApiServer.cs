using System.Net;
using System.Text.Json;
using Infrastructure.ExternalApi;
using WireMock.Client.Extensions;
using WireMock.Net.Testcontainers;

namespace Tests.Integration.Infrastructure;

public class ExternalApiServer : IAsyncDisposable
{
    private WireMockContainer externalApi = new WireMockContainerBuilder()
        .WithAutoRemove(true)
        .WithCleanUp(true)
        .Build();

    public string Url => this.externalApi.GetPublicUrl();

    public async Task Start()
    {
        await this.externalApi.StartAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await this.externalApi.StopAsync();
        await this.externalApi.DisposeAsync();
    }

    public async Task SetupExternalApi(int id, bool isSuccess)
    {
        var statuc = isSuccess ? HttpStatusCode.OK : HttpStatusCode.InternalServerError;

        var response = new ExternalApiResponse { Data = "data" };

        var mappingBuilder = this.externalApi
            .CreateWireMockAdminClient()
            .GetMappingBuilder();

        mappingBuilder.Given(action =>
            action.WithRequest(request =>
                request
                    .UsingGet()
                    .WithPath(ExternalApiEndpoints.Get(id)))
                .WithResponse(response =>
                    response
                        .WithBody(JsonSerializer.Serialize(response))
                        .WithStatusCode(HttpStatusCode.OK)));

        await mappingBuilder.BuildAndPostAsync();
    }
}
