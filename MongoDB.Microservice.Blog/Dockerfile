#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
ENV INDOCKER=1

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MongoDB.Microservice.Blog/MongoDB.Microservice.Blog.csproj", "MongoDB.Microservice.Blog/"]
RUN dotnet restore "./MongoDB.Microservice.Blog/MongoDB.Microservice.Blog.csproj"
COPY . .
WORKDIR "/src/MongoDB.Microservice.Blog"
RUN dotnet build "./MongoDB.Microservice.Blog.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MongoDB.Microservice.Blog.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MongoDB.Microservice.Blog.dll"]