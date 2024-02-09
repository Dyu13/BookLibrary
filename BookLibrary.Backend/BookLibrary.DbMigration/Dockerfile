#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base

COPY ../wait-for-it.sh /wait-for-it.sh
RUN chmod +x wait-for-it.sh

WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src

COPY ["BookLibrary.DbMigration/BookLibrary.DbMigration.csproj", "BookLibrary.DbMigration/"]
COPY ["BookLibrary.Infrastructure/BookLibrary.Infrastructure.csproj", "BookLibrary.Infrastructure/"]
COPY ["BookLibrary.Application/BookLibrary.Application.csproj", "BookLibrary.Application/"]
COPY ["BookLibrary.Domain/BookLibrary.Domain.csproj", "BookLibrary.Domain/"]

RUN dotnet restore "BookLibrary.DbMigration/BookLibrary.DbMigration.csproj"

COPY . .

WORKDIR "/src/BookLibrary.DbMigration"
RUN dotnet build "BookLibrary.DbMigration.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookLibrary.DbMigration.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "/wait-for-it.sh", "book-library-db-ip:1433", "-t", "120", "--", "dotnet", "BookLibrary.DbMigration.dll"]