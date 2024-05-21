#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["QuerUmLivro.API/QuerUmLivro.API.csproj", "QuerUmLivro.API/"]
COPY ["QuerUmLivro.Infra.Data/QuerUmLivro.Infra.Data.csproj", "QuerUmLivro.Infra.Data/"]
COPY ["QuerUmLivro.Domain/QuerUmLivro.Domain.csproj", "QuerUmLivro.Domain/"]
COPY ["QuerUmLivro.Infra.Ioc/QuerUmLivro.Infra.Ioc.csproj", "QuerUmLivro.Infra.Ioc/"]
COPY ["QuerUmLivro.Gateways/QuerUmLivro.Gateways.csproj", "QuerUmLivro.Gateways/"]
COPY ["QuerUmLivro.Infra.Services/QuerUmLivro.Infra.Services.csproj", "QuerUmLivro.Infra.Services/"]
COPY ["QuerUmLivro.Infra.Mapper/QuerUmLivro.Infra.Mapper.csproj", "QuerUmLivro.Infra.Mapper/"]
RUN dotnet restore "./QuerUmLivro.API/./QuerUmLivro.API.csproj"
COPY . .
WORKDIR "/src/QuerUmLivro.API"
RUN dotnet build "./QuerUmLivro.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./QuerUmLivro.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QuerUmLivro.API.dll"]