FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ExampleConsoleAppTry/ExampleConsoleAppTry.csproj", "ExampleConsoleAppTry/"]
RUN dotnet restore "ExampleConsoleAppTry/ExampleConsoleAppTry.csproj"
COPY . .
WORKDIR "/src/ExampleConsoleAppTry"
RUN dotnet build "ExampleConsoleAppTry.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ExampleConsoleAppTry.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExampleConsoleAppTry.dll"]
