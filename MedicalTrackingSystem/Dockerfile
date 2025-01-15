# .NET SDK image'ını kullan
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# .NET SDK image'ını kullanarak build işlemi yap
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MedicalTrackingSystem.csproj", "./"]
# Add SQLite package before restore
RUN dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 7.0.0
RUN dotnet restore "MedicalTrackingSystem.csproj"
COPY . .
RUN dotnet build "MedicalTrackingSystem.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MedicalTrackingSystem.csproj" -c Release -o /app/publish

# Çalıştırma adımında uygulamayı başlat
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MedicalTrackingSystem.dll"]
