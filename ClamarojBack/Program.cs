// Program.cs
using ClamarojBack;
using ClamarojBack.Context;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var dbContext = services.GetRequiredService<AppDbContext>();

            // Aplicar migraciones
            dbContext.Database.Migrate();

            // Llamar al seeder aquí
            Seeders seeder = new(dbContext);
            seeder.Seed();
        }

        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
