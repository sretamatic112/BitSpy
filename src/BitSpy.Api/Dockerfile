FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:7185

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["BitSpy.Api.csproj", "."]
RUN dotnet restore "./BitSpy.Api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "BitSpy.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BitSpy.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BitSpy.Api.dll"]