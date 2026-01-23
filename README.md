# Test api (MyAPP) for docker container

## Create another .net api project (myapp) and run it in docker container locally or in docker hub

- Step 1 - Run below command in terminal to create a .net api project with controllers

```.Net 9.0
dotnet new webapi --use-controllers -o MyApp
code -r MyApp
dotnet run
```
- Step 2 - Add .dockerignore for docker to ignore the obj folder and bin folder in docker
- Step 3 - Add a docker file (Dockerfile) to build the image and run the container

### Method 1 -

#### URL to set in myapp or calling api  / call by service name not by localhost because LOCALHOST will search endpoint in same container
```.NET 9.0
builder.Services.AddHttpClient("TestAPI", client =>
{
    client.BaseAddress = new Uri("http://host.docker.internal:5215");
});
```
Why service names? Inside the compose network, http://testapi:8080 resolves to the testapi container.
Don’t use localhost from one container to reach another; localhost would refer to the same container.

#### 2) Open in browser
([http://localhost:5271/api/WeatherForecast](http://localhost:5271/api/myapp))

#### How to run in docker
1. docker build -t testapp . (used to build the image)
2. docker run -p 5271:80 testapp (used to run the image)
3. [http://localhost:5271/api/myapp](http://localhost:5271/api/myapp) (used to open the api in browser which is running in docker)

### Method 2 (Best way to run in docker) -

#### Add myapp-docker-compose.yml
1. Used docker file for context in myapp-docker-compose.yml to build the image and run the container. Its best practice for development bcz just modify the code and it will automatically build and run the container. don't need to build image everytime.
2. __Important__ Used - __TestApi_BaseUrl=http://testapi:8080__ to access the api in my app bcz the service name is testapi and not localhost bcz localhost will search endpoint in same container. Otherwise will have to use service name __http://host.docker.internal:5215__
#### how to run in docker
1. docker compose -f myapp-docker-compose.yml up -d
2. [http://localhost:5271/api/myapp](http://localhost:5271/api/myapp) (used to open the api in browser which is running in docker)
