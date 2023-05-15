namespace WebApplicationPostgres.Startup
{
    // How to:
    // https://www.jondjones.com/programming/aspnet-core/how-to/essential-net-6-app-settings-tips-master-developer-and-environment-specific-settings/

    public static class WebApplicationBuilder
    {
        public static IHostBuilder ConfigureAppSettings(this IHostBuilder host)
        {
            host.ConfigureAppConfiguration((ctx, builder) =>
            {
                builder.AddJsonFile("appsettings.json", false, true);
                builder.AddEnvironmentVariables();
            });

            return host;
        }
    }
}
