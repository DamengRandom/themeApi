# FROM mcr.microsoft.com/dotnet/core/sdk:3.1
# FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
# # FROM microsoft/aspnetcore-build
# EXPOSE 5002
# WORKDIR /app
# COPY . .
# RUN dotnet restore
# ENTRYPOINT [ "dotnet", "run" ]
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["ThemeApi.csproj", "./"]
RUN dotnet restore "./ThemeApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "ThemeApi.csproj" -c Release -o /app/build
# RUN dotnet run

FROM build AS publish
RUN dotnet publish "ThemeApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "ThemeApi.dll"]
# ENTRYPOINT ["bin/sh"]