using Decorator.Api.Extensions;
using Decorator.DataAccessLayer.Extensions;
using Decorator.BusinessLayer.Extensions;
using Decorator.Api.Middlewares;
using Serilog;

string siteCorsPolicy = "SiteCorsPolicy";

var builder = WebApplication.CreateBuilder(args);
{

    builder.Services.AddCors(options =>
    {
        options.AddPolicy(siteCorsPolicy,
                           builder =>
                           {
                               builder.WithOrigins("https://localhost:7027", "https://localhost:7292")
                                                   .AllowAnyHeader()
                                                   .AllowAnyMethod()
                                                   .AllowCredentials();
                           });
    });
    builder.Services.AddSwaggerGen();


    builder.Services
        .AddPresentation()
        .AddDatabaseLayer(builder.Configuration.GetConnectionString("Database"))
        .AddBusinessLayer();
}
builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddSerilog();
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Add the GlobalExceptionMiddleware to the request pipeline.
app.UseMiddleware<GlobalExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();

}
app.UseCors(siteCorsPolicy);

app.UseAuthorization();

app.MapControllers();

app.Run();
