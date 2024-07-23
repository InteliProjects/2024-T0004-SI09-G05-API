#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Usando a imagem SDK do .NET para construir o aplicativo
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiando o arquivo do projeto e restaurando dependências
COPY ["Polo.Dashboard.WebApi.csproj", "."]
RUN dotnet restore "Polo.Dashboard.WebApi.csproj"

# Copiando todo o código e construindo o aplicativo
COPY . .
WORKDIR "/src"
RUN dotnet build "Polo.Dashboard.WebApi.csproj" -c Release -o /app/build

# Publicando o aplicativo
FROM build AS publish
RUN dotnet publish "Polo.Dashboard.WebApi.csproj" -c Release -o /app/publish

# Usando a imagem ASP.NET para executar o aplicativo publicado
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Polo.Dashboard.WebApi.dll"]