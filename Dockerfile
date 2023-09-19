FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["PrimitiveBankingSystem.csproj", "./"]
RUN dotnet restore "PrimitiveBankingSystem.csproj"
COPY ./src/ .
WORKDIR "/src/"
RUN dotnet build "PrimitiveBankingSystem.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PrimitiveBankingSystem.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PrimitiveBankingSystem.dll"]
