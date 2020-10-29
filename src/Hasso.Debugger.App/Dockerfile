#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Hasso.Debugger.App/Hasso.Debugger.App.csproj", "Hasso.Debugger.App/"]
RUN dotnet restore "Hasso.Debugger.App/Hasso.Debugger.App.csproj"
COPY . .
WORKDIR "/src/Hasso.Debugger.App"
RUN dotnet build "Hasso.Debugger.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hasso.Debugger.App.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hasso.Debugger.App.dll"]