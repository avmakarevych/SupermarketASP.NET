using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.Extensions.DependencyInjection;
using SupermarketAPI.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SupermarketAPIContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SupermarketAPIContext") ?? throw new InvalidOperationException("Connection string 'SupermarketAPIContext' not found.")));
// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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