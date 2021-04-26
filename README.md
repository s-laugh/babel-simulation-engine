# ESDC Simulation Engine

## What is it?

This project is a template for building the simulation component of a policy difference engine

### Policy Difference Engine

A Policy Difference Engine (PDE) is a set of tools that measures the impact of a proposed change to a rule (e.g. policy, regulation, legislation, etc.) The idea is to have both sets of rules encoded (the existing rules and proposed changes). You take a sample population of individuals, and run each of them against both sets of rules, generating results that show the impact of the change.

### Maternity Benefits Example

Let's use the example of a maternity benefits (PDE) to illustrate the concept. The "rule" that we are measuring is 'how much money a successful maternity benefit applicant is entitled to'. This number is calculated based on a number of properties, such as:
- Applicant data: income levels, unemployment rate in their region
- Rule parameters: maximum number of weeks, percentage of average income, maximum weekly amount

The maternity benefit rule/calculation boils down to these rule parameters. If we change those parameters, then that would constitute a "rule change", which we want to run simulations against. So we can encode the general rules into a separate rules engine, and pass in parameters that signify a change in those rules.

Suppose we have 100 applicants eligible for maternity benefits. To see how much they are entitled to under the current system, we run their relevant data (income levels, region) against the existing rule parameters. This will result in 100 values, corresponding to how much each applicant is eligible for. Next, we run them against the proposed rule change to see how much they would be eligible for under the new system. Some will gain money and some will lose money. The results of both rules can be aggregated and sent off to a separate UI or other tool for analysis. 

### Components and Architecture of a PDE

We've identified 4 key components of a PDE:

#### Data 
This is the fuel for the PDE. In our maternity benefit example, a data point corresponds to the application data for an individual maternity benefit applicant. This will be stored in a database and connected to by the simulation engine. The simulation engine is agnostic about the technology used to store data, simply specifying an interface for interacting with a data store.

#### Rules Engine
The codified rules should be stored in a separate engine. so that it can be have multiple use cases. The rules engine will take a data point (e.g. maternity benefit applicant), run the relevant calculation, and output the calculated value (e.g. the maternity benefit eligibility amount). It can be configured to run the existing rule or a proposed change to the rule. Ideally the rules engine is exposed via an accessible web API.

#### Simulation Engine
The simulation engine brings the data and rules engine together. It feeds the population data into the rules engine for both the existing case and the proposed changes. It does this by passing two pieces of data to the rules API:
- The rule parameters, that specify the encoded rule to apply
- A data point, representing a person that the rule is applied to

The result returned from such a request to the rules API will be a number resulting from the calculation. The simulation engine will iterate over the entire population for both sets of rules. The simulation engine is also exposed as a web API. 

This project is a template for a simulation engine that can be run as a web API.

#### UI
Once the simulation is complete, the results can be sent to a user interface for further processing
 

## Overview of projects

This repo is a template for the simulation engine component of the PDE, and it can be run as a web API. 

It contains the code that will be common to *any* PDE. In some cases, this will simply be interfaces that a developer must implement. There are interfaces for interacting with the rules and interacting with a database. The repo is composed of several projects

### esdc-simulation-base
This project is a class library that contains the core code for running any sort of simulation for a PDE. This includes generic handlers, simulation case runners, request/result classes, interfaces for a rules engine, and interfaces for storage systems. It is up to the developer to implement these interfaces to suit their purpose.

### sample-scenario
This is a sample of what an implemented scenario might look like. It contains implementations of the various interfaces specified in the esdc-simulation-base project. It is a class library with a dependency on the base library. Note that this dependency should only go one way. The base class library should NOT depend on the class library for a specific scenario.

### esdc-simulation-api
This project is a thinner web-api project that exposes the functionality of the two previous class libraries. 


## Deep dive into code

In this section, we will go through the components of the base class library, and see their sample implementations in the sample-scenario library

### Simulation flow and functionality

To be continued...


## Next Steps

One potential goal is to build a templating system, where a developer can enter a few details in the command line, and then generate a template as a starting point for the simulation engine. Will continue looking into this.

Links
- Use a script/template to generate the files
    - https://docs.microsoft.com/en-us/dotnet/core/tutorials/cli-templates-create-item-template
    - https://docs.microsoft.com/en-us/dotnet/core/tools/custom-templates
    - https://devblogs.microsoft.com/dotnet/how-to-create-your-own-templates-for-dotnet-new/

Setup Commands:
- dotnet new classlib -o maternity-benefits
- dotnet new xunit -o maternity-benefits.Tests
- dotnet sln add maternity-benefits
- dotnet sln add maternity-benefits.Tests
- dotnet add esdc-simulation-api reference maternity-benefits
- dotnet add maternity-benefits reference esdc-simulation-base
- dotnet add maternity-benefits.Tests reference maternity-benefits
- dotnet add maternity-benefits.Tests reference esdc-simulation-base

Other set up:
- Need Injection in api Startup file
- Install Microsoft Caching package
- If doing caching, then may need to change the strings?
- Scaffold a controller
    - GET Sim by Id
    - POST new sim
    - POST persons in bulk
    - GET all persons
