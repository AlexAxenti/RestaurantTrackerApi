FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY src/RestaurantTracker.Api/RestaurantTracker.Api.csproj src/RestaurantTracker.Api/
RUN dotnet restore src/RestaurantTracker.Api/RestaurantTracker.Api.csproj

COPY . .
RUN dotnet publish src/RestaurantTracker.Api/RestaurantTracker.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "RestaurantTracker.Api.dll"]