FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

#
# copy csproj and restore as distinct layers
COPY *.sln .
COPY esdc-simulation-api/*.csproj ./esdc-simulation-api/
COPY esdc-simulation-base/*.csproj ./esdc-simulation-base/
COPY maternity-benefits/*.csproj ./maternity-benefits/
COPY esdc-simulation-classes/*.csproj ./esdc-simulation-classes/
COPY esdc-simulation-base.Tests/*.csproj ./esdc-simulation-base.Tests/
COPY maternity-benefits.Tests/*.csproj ./maternity-benefits.Tests/
#
RUN dotnet restore 
#
# copy everything else and build app
COPY esdc-simulation-api/. ./esdc-simulation-api/
COPY esdc-simulation-base/. ./esdc-simulation-base/ 
COPY esdc-simulation-classes/. ./esdc-simulation-classes/ 
COPY maternity-benefits/. ./maternity-benefits/
#
WORKDIR /app
RUN dotnet publish -c Release -o ./publish
#
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app/esdc-simulation-api
#
COPY --from=build-env /app/publish .
ENTRYPOINT ["dotnet", "esdc-simulation-api.dll"]