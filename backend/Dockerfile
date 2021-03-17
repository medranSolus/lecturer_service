FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS build
WORKDIR /src
COPY ["backend/LecturerService.csproj", "backend/"]
RUN dotnet restore "backend/LecturerService.csproj"
COPY . .
WORKDIR "/src/LecturerService"
RUN dotnet build "LecturerService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "LecturerService.csproj" -c Release -o /app
  
FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "LecturerService.dll"]