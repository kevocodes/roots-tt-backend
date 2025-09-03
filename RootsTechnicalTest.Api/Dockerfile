# --- build ---
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY RootsTechnicalTest.sln ./
COPY RootsTechnicalTest.Api/RootsTechnicalTest.Api.csproj RootsTechnicalTest.Api/
RUN dotnet restore RootsTechnicalTest.Api/RootsTechnicalTest.Api.csproj

COPY . .
RUN dotnet publish RootsTechnicalTest.Api/RootsTechnicalTest.Api.csproj -c Release -o /app/publish /p:UseAppHost=false

# --- runtime ---
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "RootsTechnicalTest.Api.dll"]