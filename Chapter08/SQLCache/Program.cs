var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDistributedSqlServerCache(options =>
{
    options.ConnectionString = "Data Source=.;Initial Catalog=DistributedCache;Integrated Security=true;";
    options.SchemaName = "dbo";
    options.TableName = "Cache";
});

//builder.Services.AddStackExchangeRedisCache(
//                options =>
//                {
//                    options.Configuration = "<Connection string copied in step 1>";
//                });

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
