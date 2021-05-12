FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

#
# copy csproj and restore as distinct layers
COPY *.sln .
COPY esdc-simulation-api/*.csproj ./esdc-simulation-api/
COPY esdc-simulation-base/*.csproj ./esdc-simulation-base/
COPY maternity-benefits/*.csproj ./maternity-benefits/
COPY sample-scenario/*.csproj ./sample-scenario/
COPY esdc-simulation-base.Tests/*.csproj ./esdc-simulation-base.Tests/
COPY maternity-benefits.Tests/*.csproj ./maternity-benefits.Tests/
COPY sample-scenario.Tests/*.csproj ./sample-scenario.Tests/ 
#
RUN dotnet restore 
#
# copy everything else and build app
COPY esdc-simulation-api/. ./esdc-simulation-api/
COPY esdc-simulation-base/. ./esdc-simulation-base/ 
COPY maternity-benefits/. ./maternity-benefits/
COPY sample-scenario/. ./sample-scenario/ 
#
WORKDIR /app
RUN dotnet publish -c Release -o ./publish
#
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
#
COPY --from=build-env /app/publish .
ENTRYPOINT ["dotnet", "esdc-simulation-api.dll"]