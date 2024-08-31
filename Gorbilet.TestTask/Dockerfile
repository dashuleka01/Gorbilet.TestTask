FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS build
WORKDIR /src
COPY ["Gorbilet.TestTask/Gorbilet.TestTask.csproj", "Gorbilet.TestTask/"]
RUN dotnet restore "./Gorbilet.TestTask/Gorbilet.TestTask.csproj"
COPY . .
WORKDIR "/src/Gorbilet.TestTask"
RUN dotnet build "./Gorbilet.TestTask.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "./Gorbilet.TestTask.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Gorbilet.TestTask.dll"]