# run the following commands in a terminal to start the service container:
# docker build -t clerk-data-image .
# docker run -dp 5000:5000 clerk-data-image

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/clerk-data-service/clerk-data-service.csproj", "src/clerk-data-service/"]
RUN dotnet restore "src/clerk-data-service/clerk-data-service.csproj"
COPY . .
WORKDIR "/src/src/clerk-data-service"
RUN dotnet build "clerk-data-service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "clerk-data-service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "clerk-data-service.dll"]