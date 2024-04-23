namespace OTEL1;

public static class Extensions
{
    public static WebApplicationBuilder ConfigureDefaults(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpClient();
        builder.Configuration.AddUserSecrets<Program>();
        return builder;
    }
}
