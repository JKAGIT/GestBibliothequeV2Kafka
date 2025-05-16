
using GestionEvenements.Configuration;
using GestionEvenements.Events;
using GestionEvenements.Kafka;
using GestionEvenements.Kafka.Services;
using GestionNotification.API.Middlewares;
using GestionNotification.API.Services;
using GestionNotification.Application.Interfaces;
using GestionNotification.Application.Services;
using GestionNotification.Domain.Repositories;
using GestionNotification.Infrastructure.Interfaces;
using GestionNotification.Infrastructure.Persistence;
using GestionNotification.Infrastructure.Repositories;
using GestionNotification.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

try
{
    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();
    Log.Information("Démarrage du microservice Gestion Notification");
    builder.Host.UseSerilog();

    // 🔹 Services API
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // 🔹 Services applicatifs
    builder.Services.AddScoped<INotificationService, NotificationService>();
    builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
    builder.Services.AddScoped<INotificationSenderService, NotificationSenderService>();
    builder.Services.AddSignalR();
    //builder.Services.AddScoped<ICourrielService, CourrielService>();
    //builder.Services.AddScoped<ISmsService, SmsService>();

    builder.Services.AddSingleton<ISmsService>(sp =>
        new SmsService(sp.GetRequiredService<IConfiguration>(), builder.Environment.IsDevelopment()));
    builder.Services.AddSingleton<ICourrielService>(sp =>
        new CourrielService(sp.GetRequiredService<IConfiguration>(), builder.Environment.IsDevelopment()));

    builder.Services.AddScoped<INotificationSenderService, NotificationSenderService>();

    builder.Services.AddDbContext<NotificationContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    // 🔹 Configuration Kafka
    builder.Services.Configure<KafkaSettings>(builder.Configuration.GetSection("KafkaSettings"));
    builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<KafkaSettings>>().Value);

    // 🔥 Enregistrement KafkaProducer et KafkaConsumer (sans doublon)
    builder.Services.AddSingleton<IKafkaProducer<NotificationEvent>>(provider =>
    {
        var settings = provider.GetRequiredService<KafkaSettings>();
        return new KafkaProducer<NotificationEvent>(settings.BootstrapServers);
    });

    builder.Services.AddSingleton<IKafkaConsumer<LivreEvent>>(provider =>
    {
        var settings = provider.GetRequiredService<KafkaSettings>();
        return new KafkaConsumer<LivreEvent>(settings.BootstrapServers, settings.GroupId, KafkaTopics.LivreTopic);
    });

    builder.Services.AddSingleton<IKafkaConsumer<StockEvent>>(provider =>
    {
        var settings = provider.GetRequiredService<KafkaSettings>();
        return new KafkaConsumer<StockEvent>(settings.BootstrapServers, settings.GroupId, KafkaTopics.StockTopic);
    });

    // ✅ Enregistrement propre du service d’arrière-plan KafkaConsumerBackgroundService
    builder.Services.AddHostedService<KafkaConsumerBackgroundService>();

    // 🔹 Configuration CORS pour Swagger UI
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
    });

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gestion Notification API v1");
        });
    }

    app.UseMiddleware<ExceptionMiddleware>();
    app.UseHttpsRedirection();
    app.UseCors("AllowAll");
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Le microservice GestionNotification n'a pas pu démarrer correctement");
}
finally
{
    Log.CloseAndFlush();
}