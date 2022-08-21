#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NTech.WebAPI/NTech.WebAPI.csproj", "NTech.WebAPI/"]
COPY ["NTech.DataAccess/NTech.DataAccess.csproj", "NTech.DataAccess/"]
COPY ["NTech.Entity/NTech.Entity.csproj", "NTech.Entity/"]
COPY ["Core/Core.csproj", "Core/"]
COPY ["NTech.Dto/NTech.Dto.csproj", "NTech.Dto/"]
COPY ["NTech.Business/NTech.Business.csproj", "NTech.Business/"]
RUN dotnet restore "NTech.WebAPI/NTech.WebAPI.csproj"
COPY . .
WORKDIR "/src/NTech.WebAPI"
RUN dotnet build "NTech.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NTech.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NTech.WebAPI.dll"]