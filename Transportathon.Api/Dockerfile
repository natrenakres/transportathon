FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Transportathon.Api/Transportathon.Api.csproj", "Transportathon.Api/"]
RUN dotnet restore "Transportathon.Api/Transportathon.Api.csproj"
COPY . .
WORKDIR "/src/Transportathon.Api"
RUN dotnet build "Transportathon.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Transportathon.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Transportathon.Api.dll"]
