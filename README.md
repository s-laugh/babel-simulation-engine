## README

Ideas
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
    - GET all sims?
    - POST persons in bulk
    - GET all persons
