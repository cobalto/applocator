#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["AppLocator.WebApi/AppLocator.WebApi.csproj", "AppLocator.WebApi/"]
COPY ["AppLocator.Infrastructure/AppLocator.Infrastructure.csproj", "AppLocator.Infrastructure/"]
COPY ["AppLocator.Domain/AppLocator.Domain.csproj", "AppLocator.Domain/"]
COPY ["AppLocator.Application/AppLocator.Application.csproj", "AppLocator.Application/"]
RUN dotnet restore "AppLocator.WebApi/AppLocator.WebApi.csproj"
COPY . .
WORKDIR "/src/AppLocator.WebApi"
RUN dotnet build "AppLocator.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AppLocator.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AppLocator.WebApi.dll"]
