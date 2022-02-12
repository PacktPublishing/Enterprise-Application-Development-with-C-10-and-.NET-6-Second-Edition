using DITypes.Service;

var builder = WebApplication.CreateBuilder(args);

// Validate scope lifetime dependency
// builder.Host.UseDefaultServiceProvider(opt => { opt.ValidateScopes = true; });

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IWeatherService, WeatherService_ConstructorInjection>();
builder.Services.AddScoped<IWeatherProvider, WeatherProvider>();
builder.Services.AddSingleton<IInvalidLifetimeTest, InvalidLifetimeTest>();

//Print list of framework services
foreach (var i in builder.Services.AsEnumerable())
{
    Console.WriteLine($"{i.Lifetime} - {i.ServiceType.ToString()}");
}

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Constructor}/{id?}");

app.Run();
