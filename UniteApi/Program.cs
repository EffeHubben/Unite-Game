using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using UniteApi;

var builder = WebApplication.CreateBuilder(args);

// Stel hier je host URL in voor Unity
builder.WebHost.UseUrls("http://localhost:7077");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Voeg databasecontext toe
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// HTTPS redirect uitgeschakeld omdat Unity niet met HTTPS werkt
// app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
app.Run();
