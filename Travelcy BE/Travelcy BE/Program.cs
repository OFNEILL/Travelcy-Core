using Travelcy.App;
using Travelcy.App.DataObjects.Responses;
using Travelcy.App.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBusinessRepository, BusinessRepository>();

//CORS
string cors = "AllowAll";
builder.Services.AddCors(options =>
{
    options.AddPolicy(cors, policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/conversion/HandleConversion", (float amount, string currency, IBusinessRepository repo) =>
{
    HandleConversionResponse response = new HandleConversionResponse();

    try
    {
        response = repo.HandleConversionAsync(amount, currency.ToUpper()).Result;
    }

    catch
    {
        response.IsSuccessful = false;
    }

    return response;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.UseCors("AllowAll");
app.Run();