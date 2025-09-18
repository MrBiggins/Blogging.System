
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src


COPY ["Blogging.System.WebApi/Blogging.System.WebApi.csproj", "Blogging.System.WebApi/"]
COPY ["Blogging.System.Business.Logic/Blogging.System.Business.Logic.csproj", "Blogging.System.Business.Logic/"]
COPY ["Blogging.System.Domain/Blogging.System.Domain.csproj", "Blogging.System.Domain/"]
COPY ["Blogging.System.Infrastructure/Blogging.System.Infrastructure.csproj", "Blogging.System.Infrastructure/"]

RUN dotnet restore "Blogging.System.WebApi/Blogging.System.WebApi.csproj"
COPY . .
WORKDIR /src/Blogging.System.WebApi
ARG BUILD_CONFIGURATION=Release
RUN dotnet build "Blogging.System.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

RUN echo "Running tests..."

WORKDIR /src

RUN dotnet build "Blogging.System.Test/Blogging.System.Test.csproj" -c $BUILD_CONFIGURATION

RUN dotnet test "Blogging.System.Test/Blogging.System.Test.csproj" \
    --no-build \
    --configuration $BUILD_CONFIGURATION \
    --logger "console;verbosity=detailed" \
    --collect:"XPlat Code Coverage" \
    --blame-hang-timeout 60s \
    --list-tests

WORKDIR /src/Blogging.System.WebApi

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Blogging.System.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Blogging.System.WebApi.dll"]