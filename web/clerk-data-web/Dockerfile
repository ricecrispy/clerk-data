#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 5555

ENV ASPNETCORE_URLS=http://+:5555

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["clerk-data-web.csproj", "./"]
RUN dotnet restore "./clerk-data-web.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "clerk-data-web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "clerk-data-web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "clerk-data-web.dll"]