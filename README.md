# ESDC Simulation Engine

This project is the Simulation Engine component of the Maternity Benefit Policy Difference Engine prototype. 

A Policy Difference Engine (PDE) is a set of tool that measures the impact of a proposed change to a rule (e.g. policy, regulation, legislation, etc.) The idea is to have both sets of rules encoded (the existing rules and proposed changes). A sample population of individuals is run against both sets of rules, generating results that show the impact of the change. The Simulation Engine is linked to a database that stores the sample population, and connects to a Rules Engine, which stores the encoded rules. When a new simulation is requested, the population is fetched from the database. Each member of the population is sent to the Rules Engine along with the existing rule (base case). This calculates their base amount, i.e. their maternity benefits entitlement under the current system. Then the member is sent to the Rules Engine with the proposed change (variant case). The result is their variant amount, i.e. their maternity benefits entitlement under the proposed change. These results are stored in the database, and can be  subsequently retrieved for analysis.


## Overview of projects

This repo is a template for the simulation engine component of the PDE, and it can be run as a web API. The goal is to make this repo act as a generic template for *any* PDE, since PDE's will all have some components and functionality in common. We want to capture those commonalities through interfaces and generics, so that it is very easy to create PDE's for a given rule in the future. In some cases, this will involve a developer implementing pre-defined interfaces, such as those for interacting with the rules and interacting with a database. The repo is composed of several projects:

### esdc-simulation-base
This project is a class library that contains the core code for running a simulation for a PDE. This includes generic handlers, simulation case runners, request/result classes, interfaces for a rules engine, and interfaces for storage systems. It is up to the developer to implement these interfaces to suit their purpose. It doesn't contain code specific to any rule. There is also an associated test project.

### esdc-simulation-api
This is the executable web-api project that exposes the functionality of the other class libraries as a REST API.

### esdc-simulation-classes
This project is a class library containing plain C# objects which are used as request/response objects in the esdc-simulation-api project. These are separated as their own class library so that they can be published as a Nuget Package and used by consuming applications, such as the Data Primer and the Front-end Web Application. 

### maternity-benefits
This is a concrete example of a class library that implements the interfaces specified in the esdc-simulation-base library. This includes code specific to the maternity benefits simulation, as well as implementations for the storage layer, which interacts with the database. There is also an associated test project.

## Development

### Config

Ensure the config settings are properly set in the proper appsettings.XXX.json file in the esdc-simulation-api project. If you are developing locally, then set the desired parameters in the appsettings.Development.json file. The two config settings of interest are:
- ConnectionStrings.DefaultDB: this is the connection string for the database that stores the simulation data and results.
- RulesOptions.Url: This is the host URL for the Rules Engine

### Running Locally

- `cd` into the main solution folder
- `dotnet run --project esdc-simulation-api`

Note: If running this project locally alongside related web APIs (such as the Rules Engine), ensure you are specifying the projects to run on separate ports in the launchSettings.json file

### Running in Docker

- `docker build -t babel-simulation-engine .`
- `docker run -it --rm -p 7000:80 babel-simulation-engine`

### Testing

Tests are set up for the base library and the maternity-benefits class library. Running `dotnet test` will run the tests for all test projects

### Deploying

There are currently two separate deployments of the Simulation Engine, both in Microsoft Azure (Azure App Service). The mock deployment is connected to a mock database, and the prod deployment is connected to the prod database. Prior to deploying, ensure the configs are appropriately set in the appsettings.Production.json file in esdc-simulation-api project.

I've used VS Code as an IDE, and it comes with extensions that make deployment easier. Install the following extensions:
- Azure Account
- Azure App Service
- Docker

Once successfully installed, you will get corresponding tabs in VS code that can be used to facilitate deployment. Ensure you are signed in to Azure and that VS Code is connected to your Azure account.

Build the docker file. You can use the previously mentioned docker command or use the docker plugin in VS Code by right-clicking on the docker file and selecting "Build Image".

Navigate to the Docker tab in VS Code and find the "Images" section. Find the image that you just built, expand it, then right-click on the "latest" tag, and select "push". It should confirm the container that will be used for deployment.

If deploying to both mock and prod environments, you will need to do this twice. First, set the appsettings.Production.json config settings to the mock values, then build and push the image. Then change the config to the prod values, and repeat. 


### Nuget package

The esdc-simulation-classes library must be re-published when making a change to the API request/response objects. The Front-end web app and the Data Primer rely on this Nuget Package, so they will need to be updated accordingly.

Reference: https://docs.microsoft.com/en-us/nuget/quickstart/create-and-publish-a-package-using-the-dotnet-cli

To update the Nuget package.
- Open the esdc-simulation-classes.csproj file
- Update the number inside the <Version> tag appropriately, depending on the type of change (semantic versioning)
- Run `dotnet build`. The project is configured to generate the Nuget package on build
- cd into the bin/debug folder of the esdc-simulation-classs project, where the generated .nupkg file is stored
- Replace the relevant values (X.Y.Z and the Api Key) in the following command and run it: `dotnet nuget push BabelSimulationEngineSample.X.Y.Z.nupkg --api-key [API_KEY_GOES_HERE] --source https://api.nuget.org/v3/index.json`. The X.Y.Z should correspond to the version you set in the csproj file.
- You can now go into the consuming applications (e.g. Data Primer) and update the package reference
 
 
