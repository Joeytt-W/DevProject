#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Dev.CoreApi/Dev.CoreApi.csproj", "Dev.CoreApi/"]
RUN dotnet restore "Dev.CoreApi/Dev.CoreApi.csproj"
COPY . .
WORKDIR "/src/Dev.CoreApi"
RUN dotnet build "Dev.CoreApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dev.CoreApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dev.CoreApi.dll"]