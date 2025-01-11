using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Tests.Integration.Infrastructure;

public class ApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    public ExternalApiServer ExternalApi { get; } = new();
    public HttpClient HttpClient = default!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(
            services =>

                services.AddHttpClient<ExternalApiServer>(
                    options =>
                    {
                        options.BaseAddress = new Uri("http://localhost:5000");
                    }));
    }


    public async Task InitializeAsync()
    {
        await ExternalApi.Start();
    }

    public async Task DisposeAsync()
    {
        await ExternalApi.DisposeAsync();
    }
}
