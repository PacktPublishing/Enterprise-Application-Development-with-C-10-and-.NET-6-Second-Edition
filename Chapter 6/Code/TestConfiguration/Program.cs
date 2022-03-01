using TestConfiguration;
using TestConfiguration.CustomConfiguration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var appInsightsInstrumentationKey = builder.Configuration["ApplicationInsights:InstrumentationKey"];

var service1Name = builder.Configuration["ApiConfigs:Service 1:Name"];

List<ApiConfig> apiConfigs = new List<ApiConfig>();
builder.Configuration.GetSection("ApiConfigs").Bind(apiConfigs);

builder.Configuration.AddAzureKeyVault($"https://{builder.Configuration["KeyVault:Name"]}.vault.azure.net/",
                  builder.Configuration["KeyVault:AppClientId"],
            builder.Configuration["KeyVault:AppClientSecret"]);

builder.Configuration.AddJsonFile("AdditionalConfig.json",
                optional: true,
                reloadOnChange: true);


builder.Configuration.AddXmlFile("AdditionalXMLConfig.xml",
                optional: true,
                reloadOnChange: true);

builder.Configuration.AddSql("Connectionstring","Query"); 


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
