using System.Net.Http.Json;

namespace Infrastructure.ExternalApi;

public class ExternalApiHttpClient
{
    private readonly HttpClient httpClient;

    public ExternalApiHttpClient(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<ExternalApiResponse?> GetAsync(int id)
    {
        string request = ExternalApiEndpoints.Get(id);
        HttpResponseMessage response = await httpClient.GetAsync(request);

        if (response.IsSuccessStatusCode)
        {
            ExternalApiResponse? content = await response.Content.ReadFromJsonAsync<ExternalApiResponse>();
            return content;
        }

        return default;
    }
}
