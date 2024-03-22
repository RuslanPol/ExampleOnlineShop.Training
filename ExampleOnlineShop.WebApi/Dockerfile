FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ExampleOnlineShop.WebApi/ExampleOnlineShop.WebApi.csproj", "ExampleOnlineShop.WebApi/"]
RUN dotnet restore "ExampleOnlineShop.WebApi/ExampleOnlineShop.WebApi.csproj"
COPY . .
WORKDIR "/src/ExampleOnlineShop.WebApi"
RUN dotnet build "ExampleOnlineShop.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ExampleOnlineShop.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExampleOnlineShop.WebApi.dll"]
