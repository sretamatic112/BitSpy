FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:7186

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["BitSpy.Ui.csproj", "."]
RUN dotnet restore "./BitSpy.Ui.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "BitSpy.Ui.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BitSpy.Ui.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BitSpy.Ui.dll"]