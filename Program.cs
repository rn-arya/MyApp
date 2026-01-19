var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Configure HttpClient for TestAPI
// builder.Services.AddHttpClient("TestAPI", client =>
// {
//     client.BaseAddress = new Uri("http://host.docker.internal:5215");
// });


// Read base URL from env (fallback for local dev)
var testApi1BaseUrl = builder.Configuration["TestApi_BaseUrl"]
                      ?? "http://localhost:5215"; // for local non-docker runs

builder.Services.AddHttpClient("TestAPI", client =>
{
    client.BaseAddress = new Uri(testApi1BaseUrl);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
