using API;
using API.Data;
using API.Interfaces;
using API.SignalR.ChatHub;
using Microsoft.EntityFrameworkCore;
using Microsoft.Azure.SignalR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR().AddAzureSignalR(options =>
{
    options.ConnectionString = builder.Configuration["Azure__SignalR__ConnectionString"];
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200", "https://messagebox-b7hhc4efdugjb9c6.germanywestcentral-01.azurewebsites.net")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();



builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING"));
});
builder.Services.AddSignalR().AddAzureSignalR();

var endpoint = builder.Configuration.GetConnectionString("AZURE_AI_ENDPOINT");
var apiKey = builder.Configuration.GetConnectionString("AZURE_AI_API_KEY");
builder.Services.AddSingleton(new TextAnalyticsService(endpoint, apiKey));


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();

var app = builder.Build();


app.UseHttpsRedirection();
app.UseAuthorization();

app.UseDefaultFiles();
app.UseStaticFiles();


app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedData(context);
}
catch (Exception ex)
{
    var logger = services.GetService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");
}

app.UseCors("CorsPolicy");
app.UseRouting();

app.MapHub<MessageHub>("/chat");
app.MapFallbackToController("Index", "Fallback");

app.Run();
