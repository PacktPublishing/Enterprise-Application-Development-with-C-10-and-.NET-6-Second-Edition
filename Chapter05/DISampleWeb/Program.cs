using DISampleWeb;
using DISampleWeb.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Register as Scoped
builder.Services.AddScoped<IScopedService,ScopedService>();
//Register as Singleton
builder.Services.AddSingleton<ISingletonService,SingletonService>();
//Register as Transient
builder.Services.AddTransient<ITransientService,TransientService>();

builder.Services.AddScoped<IWeatherForcastService, WeatherForcastService>();
builder.Services.Replace(ServiceDescriptor.Scoped<IWeatherForcastService, WeatherForcastServiceV2>());

//Removes the first registration of IWeatherForcastService .
builder.Services.Remove(ServiceDescriptor.Scoped<IWeatherForcastService, WeatherForcastService>());



//Removes all the registrations of IWeatherForcastService.
builder.Services.RemoveAll<IWeatherForcastService>();




var _disposableSingletonService = new DisposableSingletonService();
// Registering an instance of a class with singleton lifetime
builder.Services.AddSingleton(_disposableSingletonService);

// Extension method to register the services
builder.Services.AddNotificationServices();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.Lifetime.ApplicationStopping.Register(() => {
    _disposableSingletonService.Dispose();
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
