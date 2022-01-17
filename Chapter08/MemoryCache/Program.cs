using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddResponseCaching();
builder.Services.AddControllers(options =>
{
    options.CacheProfiles.Add("Default", new CacheProfile
    {
        Duration = 500,
        VaryByHeader = "user-agent",
        Location = ResponseCacheLocation.Any,
        VaryByQueryKeys = new[] { "Id" }
    });
});

builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseResponseCaching();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

builder.Services.AddControllers(options =>
{
    options.CacheProfiles.Add("Default", new CacheProfile
    {
        Duration = 500,
        VaryByHeader = "user-agent",
        Location = ResponseCacheLocation.Any,
        VaryByQueryKeys = new[] { "Id" }
    });
});


app.Run();
