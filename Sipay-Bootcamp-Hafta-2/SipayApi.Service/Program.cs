using Microsoft.Extensions.Configuration;
using Serilog;

namespace SipayApi;

public class Program
{
   

    public static void Main(string[] args)
    {
        //Loglama i�lemi i�in
        var config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();

        Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(config).CreateLogger();
        Log.Information("Application is starting");


        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
