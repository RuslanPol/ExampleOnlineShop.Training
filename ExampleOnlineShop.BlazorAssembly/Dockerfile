FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ExampleOnlineShop.BlazorAssembly/ExampleOnlineShop.BlazorAssembly.csproj", "ExampleOnlineShop.BlazorAssembly/"]
RUN dotnet restore "ExampleOnlineShop.BlazorAssembly/ExampleOnlineShop.BlazorAssembly.csproj"
COPY . .
WORKDIR "/src/ExampleOnlineShop.BlazorAssembly"
RUN dotnet build "ExampleOnlineShop.BlazorAssembly.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ExampleOnlineShop.BlazorAssembly.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExampleOnlineShop.BlazorAssembly.dll"]
