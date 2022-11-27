using API.Data;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateAsyncScope();
            var service = scope.ServiceProvider;
            try
            {
                var context = service.GetRequiredService<DataContext>();
                await context.Database.MigrateAsync();
                await SeedData.SeedUser(context);
            }
            catch (Exception ex)
            {
                var logger = service.GetRequiredService<ILogger<Program>>();
                string str = (ex.Message != null ? ex.Message.ToString() : string.Empty);
                logger.LogInformation(str);
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
