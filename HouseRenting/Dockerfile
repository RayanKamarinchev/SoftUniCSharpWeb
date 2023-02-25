#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HouseRenting/HouseRenting.Web.csproj", "HouseRenting/"]
COPY ["HouseRenting.Core/HouseRenting.Core.csproj", "HouseRenting.Core/"]
COPY ["HouseRenting.Data/HouseRenting.Data.csproj", "HouseRenting.Data/"]
RUN dotnet restore "HouseRenting/HouseRenting.Web.csproj"
COPY . .
WORKDIR "/src/HouseRenting"
RUN dotnet build "HouseRenting.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HouseRenting.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HouseRenting.Web.dll"]