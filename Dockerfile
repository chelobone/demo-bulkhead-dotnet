#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0-preview AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0-preview AS build
WORKDIR /src
COPY ["web-app-demo/web-app-demo.csproj", "web-app-demo/"]
RUN dotnet restore "web-app-demo/web-app-demo.csproj"
COPY . .
WORKDIR "/src/web-app-demo"
RUN dotnet build "web-app-demo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "web-app-demo.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "web-app-demo.dll"]
