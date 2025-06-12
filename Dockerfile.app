### build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . ./
RUN dotnet restore "src/Application/Application.csproj"
RUN dotnet build "src/Application/Application.csproj" -c Release

### publish
FROM build AS publish
RUN dotnet publish "src/Application/Application.csproj" -c Release -o /app/out/Application

### runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=publish /app/out/Application .

EXPOSE 80

ENTRYPOINT ["dotnet", "Application.dll"]