#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SimpleAuthApp/SimpleAuthApp.csproj", "SimpleAuthApp/"]
RUN dotnet restore "SimpleAuthApp/SimpleAuthApp.csproj"
COPY . .
WORKDIR "/src/SimpleAuthApp"
RUN dotnet build "SimpleAuthApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SimpleAuthApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimpleAuthApp.dll"]