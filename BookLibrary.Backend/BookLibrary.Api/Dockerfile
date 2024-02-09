#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src

COPY ["BookLibrary.Api/BookLibrary.Api.csproj", "BookLibrary.Api/"]
COPY ["BookLibrary.Application/BookLibrary.Application.csproj", "BookLibrary.Application/"]
COPY ["BookLibrary.Domain/BookLibrary.Domain.csproj", "BookLibrary.Domain/"]
COPY ["BookLibrary.Infrastructure/BookLibrary.Infrastructure.csproj", "BookLibrary.Infrastructure/"]

RUN dotnet restore "BookLibrary.Api/BookLibrary.Api.csproj"

COPY . .

WORKDIR "/src/BookLibrary.Api"
RUN dotnet build "BookLibrary.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookLibrary.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookLibrary.Api.dll"]