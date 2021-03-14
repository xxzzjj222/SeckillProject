#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["SeckillAggregateServices/SeckillAggregateServices.csproj", "SeckillAggregateServices/"]
COPY ["Projects.Common/Projects.Common.csproj", "Projects.Common/"]
COPY ["Projects.Cores/Projects.Cores.csproj", "Projects.Cores/"]
RUN dotnet restore "SeckillAggregateServices/SeckillAggregateServices.csproj"
COPY . .
WORKDIR "/src/SeckillAggregateServices"
RUN dotnet build "SeckillAggregateServices.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SeckillAggregateServices.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SeckillAggregateServices.dll"]