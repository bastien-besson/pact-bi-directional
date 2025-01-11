namespace Infrastructure.ExternalApi;

public static class ExternalApiEndpoints
{
    public static string Get(int id)
        => $"/external-api/{id}";


    public static string GetLeaves()
        => $"/external-api/leaves/";
}
