# This is test api for docker container and other research

## Create a .net api project

# 1) Create the project with controllers
dotnet new webapi --use-controllers -o MyApp
code -r MyApp
dotnet run

# URL to set in myapp or calling api  / call by service name not by localhost because LOCALHOST will search endpoint in same container
 
builder.Services.AddHttpClient("TestAPI", client =>
{
    client.BaseAddress = new Uri("http://host.docker.internal:5215");
});

Why service names? Inside the compose network, http://testapi1:8080 resolves to the testapi1 container.
Don’t use localhost from one container to reach another; localhost would refer to the same container.

# 2) Open in browser
http://localhost:5271/api/WeatherForecast

## Add .dockerignore for docker
.dockerignore

## Add dockerfile to create docker image
Dockerfile

# How to run in docker
1. docker build -t testapp . (used to build the image)
2. docker run -p 5271:80 testapp (used to run the image)
3. http://localhost:5271/api/WeatherForecast (used to open the api in browser which is running in docker)

  
