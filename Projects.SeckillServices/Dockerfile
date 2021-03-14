#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Projects.SeckillServices/Projects.SeckillServices.csproj", "Projects.SeckillServices/"]
COPY ["Projects.Common/Projects.Common.csproj", "Projects.Common/"]
COPY ["Projects.Cores/Projects.Cores.csproj", "Projects.Cores/"]
RUN dotnet restore "Projects.SeckillServices/Projects.SeckillServices.csproj"
COPY . .
WORKDIR "/src/Projects.SeckillServices"
RUN dotnet build "Projects.SeckillServices.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Projects.SeckillServices.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Projects.SeckillServices.dll"]