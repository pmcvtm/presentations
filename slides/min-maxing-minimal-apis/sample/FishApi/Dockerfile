FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FishApi/FishApi.csproj", "FishApi/"]
RUN dotnet restore "FishApi/FishApi.csproj"
COPY . .
WORKDIR "/src/FishApi"
RUN dotnet build "FishApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FishApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FishApi.dll"]
