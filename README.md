# ESDC Simulation Engine

This project is the simulation component of the Maternity Benefit Policy Difference Engine prototype. 

## Policy Difference Engine

A Policy Difference Engine (PDE) is a set of tool that measures the impact of a proposed change to a rule (e.g. policy, regulation, legislation, etc.) The idea is to have both sets of rules encoded (the existing rules and proposed changes). A sample population of individuals is run against both sets of rules, generating results that show the impact of the change.

### Maternity Benefits Example

Let's use the example of a maternity benefits (PDE) to illustrate the concept. The "rule" that we are measuring is 'how much money an eligible maternity benefit applicant is entitled to'. This number is calculated based on a number of properties, such as:
- Applicant data: income levels, unemployment rate in their region
- Rule parameters: maximum number of weeks, percentage of average income, maximum weekly amount

The maternity benefit rule/calculation can be encoded by these three rule parameters. If we change those parameters, then that would constitute a "rule change", which we want to run simulations against. So we can encode the general rules into a separate rules engine, and pass in parameters that signify a change in those rules. It's worth noting that there are many other changes that could be done to the maternity benefits system - this is a proof-of-concept that shows a change that simply involves adjusting these parameters.

Suppose we have 100 applicants eligible for maternity benefits. To see how much they are entitled to under the current system, we run their relevant data (income levels, region) against the existing rule parameters. This will result in 100 values, corresponding to how much each applicant is eligible for. Next, we run them against the proposed rule change to see how much they would be eligible for under the new system. Some will gain money and some will lose money. The results of both rules can be aggregated and sent off to a separate UI or other tool for analysis. 

### Components and Architecture of a PDE

We've identified 4 key components of a PDE:

#### Data 
This is the fuel for the PDE. In our maternity benefit example, a data point corresponds to the application and income data for an individual maternity benefit applicant. This will be stored in a database and connected to by the simulation engine. The simulation engine is agnostic about the technology used to store data, simply specifying an interface for interacting with a data store.

#### Rules Engine
The codified rules should be stored in a separate engine. so that it can be have multiple use cases. The rules engine will take a data point (e.g. maternity benefit applicant), run the relevant calculation, and output the calculated value (e.g. the maternity benefit eligibility amount). It can be configured to run the existing rule or a proposed change to the rule. Ideally the rules engine is exposed via an accessible web API.

#### Simulation Engine
The simulation engine brings the data and rules engine together. It feeds the population data into the rules engine for both the existing case and the proposed changes. It does this by passing two complex pieces of data to the rules API:
- The rule, encoded by the rule parameters
- The person to apply the rule on, encoded by the data needed to run the rule calculation

The result returned from such a request to the rules API will be a number resulting from the calculation. In the case of maternity, it is the amount they are entitled to. The simulation engine will iterate over the entire population for both sets of rules. The simulation engine is also exposed as a web API. 

This project is a template for a simulation engine that can be run as a web API.

#### UI
Once the simulation is complete, the results can be sent to a user interface for further processing
 

## Overview of projects

This repo is a template for the simulation engine component of the PDE, and it can be run as a web API. The goal is to make this repo act as a generic template for *any* PDE, since PDE's will all have some components and functionality in common. We want to capture those commonalities through interfaces and generics, so that it is very easy to create PDE's for a given rule in the future. In some cases, this will involve a developer implementing pre-defined interfaces, such as those for interacting with the rules and interacting with a database. The repo is composed of several projects:

### esdc-simulation-base
This project is a class library that contains the core code for running a simulation for a PDE. This includes generic handlers, simulation case runners, request/result classes, interfaces for a rules engine, and interfaces for storage systems. It is up to the developer to implement these interfaces to suit their purpose. It doesn't contain any code specific to a rule.

### sample-scenario
This is a sample of what an implemented PDE scenario might look like. It contains implementations of the various interfaces specified in the esdc-simulation-base project. It is a class library with a dependency on that base library. Note that this dependency should only go one way. The base class library should NOT depend on the class library of a specific scenario.

### esdc-simulation-api
This is the executable web-api project that exposes the functionality of the other class libraries.

### maternity-benefits
This is a concrete example of a class library that implements the interfaces specified in the base library.

## Development

### Running Locally

- `cd` into the solution folder
- `dotnet run --project esdc-simulation-api`

Note: If running this project locally alongside related web APIs, ensure you are specifying the projects to run on separate ports in the launchSettings.json file

### Testing

Tests are set up for the base library and the two scenario class libraries (sampleScenario and maternityBenefits). Running `dotnet test` will run the tests for all three test projects

