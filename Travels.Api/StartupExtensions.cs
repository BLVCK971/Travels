using Microsoft.OpenApi.Models;
using Travels.Application;

namespace Travels.API;

public static class StartupExtensions
{
    public static WebApplication ConfigureServices(
    this WebApplicationBuilder builder)
    {
        AddSwagger(builder.Services);

        builder.Services.AddApplicationServices();

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddControllers();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        });

        return builder.Build();

    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Travels API");
            });
        }

        app.UseHttpsRedirection();

        //app.UseRouting();

        app.UseAuthentication();

        app.UseCors("Open");

        //app.UseAuthorization();

        app.MapControllers();

        return app;

    }
    private static void AddSwagger(IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Travels API",

            });
        });
    }
}