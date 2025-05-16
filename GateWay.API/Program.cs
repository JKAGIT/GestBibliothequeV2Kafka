using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Microsoft.OpenApi.Models;
using CacheManager.Core;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

try
{
    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();
    builder.Host.UseSerilog();

    builder.Configuration
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

    // Configuration explicite de CacheManager avec un type g�n�rique `object`
    builder.Services.AddSingleton<ICacheManager<object>>(CacheFactory.Build(settings =>
    {
        settings.WithDictionaryHandle(); 
    }));

    builder.Services.AddOcelot();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy",
            builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
    });

    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Biblioth�que API Gateway",
            Version = "v1",
            Description = "API Gateway pour le syst�me de gestion de biblioth�que",
            Contact = new OpenApiContact
            {
                Name = "Administration Biblioth�que"
            }
        });
    });

    builder.Services.AddSwaggerForOcelot(builder.Configuration, options =>
    {
        options.GenerateDocsForGatewayItSelf = true;
    });

    var app = builder.Build();

    // Environnement de d�veloppement : Activer Swagger et SwaggerForOcelot
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerForOcelotUI(opt =>
        {
            opt.PathToSwaggerGenerator = "/swagger/docs";
        });
    }

    app.UseCors("CorsPolicy");
    await app.UseOcelot();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "L'API Gateway n'a pas pu d�marrer correctement");
}
finally
{
    Log.CloseAndFlush();
}