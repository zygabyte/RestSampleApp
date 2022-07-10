using Microsoft.EntityFrameworkCore;
using RestSampleApp.Data;
using RestSampleApp.Services.ItemService;
using RestSampleApp.Services.UserService;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = configuration.GetConnectionString("DbConnectionString");

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseNpgsql(connectionString);
});

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IItemService, ItemService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
