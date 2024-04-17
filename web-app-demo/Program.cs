using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using web_app_demo.Interfaces;
using web_app_demo.Service;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddLogging();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;
var connectionString = configuration.GetValue<string>("MySql:ConnectionString");
var connectionStringOracle = configuration.GetValue<string>("Oracle:ConnectionString");
Console.WriteLine(connectionString);


builder.Services.AddDbContext<DbDemoContext>(options => {
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddTransient<IUser, UserService>();
builder.Services.AddTransient<IUserContext, DbDemoContext>();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseRouting();
app.UseHttpMetrics();

app.MapControllers();
app.MapMetrics();

app.UseExceptionHandler();

app.Run();

