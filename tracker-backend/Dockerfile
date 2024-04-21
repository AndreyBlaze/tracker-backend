#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["tracker-backend/tracker-backend.csproj", "tracker-backend/"]
RUN dotnet restore "tracker-backend/tracker-backend.csproj"
COPY . .
WORKDIR "/src/tracker-backend"
RUN dotnet build "tracker-backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "tracker-backend.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "tracker-backend.dll"]