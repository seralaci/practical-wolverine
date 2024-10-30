FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
ARG run_codegen=true
WORKDIR /src
COPY ["Directory.*.props", "src/Pinger/"]
COPY ["src/Pinger/Pinger.csproj", "src/Pinger/"]
RUN dotnet restore "src/Pinger/Pinger.csproj"
COPY . .
WORKDIR "/src/src/Pinger"

# Run code generation depending on the build argument
RUN if [ "${run_codegen}" = true ] ; then dotnet run -- codegen write & dotnet run -- codegen test; else echo "skipping code generation"; fi

# Build project with Release configuration
RUN dotnet build "Pinger.csproj" -c $BUILD_CONFIGURATION --no-restore -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Pinger.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pinger.dll"]
