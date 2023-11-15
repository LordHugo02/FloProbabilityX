// Startup.cs
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProbabilityX_API.Extensions;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // Configurations des services (ajoute tes services ici)
        services.AddApplicationServices();

        // Configuration de l'authentification, des politiques, etc.

        // Ajoute d'autres services si nécessaire
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            // Configuration pour l'environnement de production
            // par exemple : app.UseExceptionHandler("/Home/Error");
            // app.UseHsts();
        }

        // Configuration de la gestion statique, des sessions, etc.

        app.UseRouting();

        // Configuration de l'authentification et de l'autorisation

        app.UseEndpoints(endpoints =>
        {
            // Configuration des points de terminaison (routes) MVC, API, etc.
            endpoints.MapControllers(); // Ajoute cette ligne si tu utilises des contrôleurs
        });
    }
}
