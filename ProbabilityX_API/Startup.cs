using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ProbabilityX_API.Settings;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
            options.Providers.Add<BrotliCompressionProvider>();
            options.Providers.Add<GzipCompressionProvider>();
        });

        services.AddDbContext<ProbabilityXContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        services.AddMvc();

        // Configuration Swagger
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProbabilityX", Version = "v1" });
        });

        // Configurations des services (ajoute tes services ici)
        ProbabilityXDIConfiguration.ConfigureDependencies(services);

        // Configuration de l'authentification, des politiques, etc.

        // Ajoute d'autres services si nécessaire
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            // Swagger uniquement en mode développement
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProbabilityX");
                c.RoutePrefix = "swagger"; // Endpoint pour accéder à l'interface Swagger
            });
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
