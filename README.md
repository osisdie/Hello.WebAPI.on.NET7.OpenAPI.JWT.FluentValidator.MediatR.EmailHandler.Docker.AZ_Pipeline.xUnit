## Prerequisite
- [Download .NET 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
  - And the latest Visual Studio / VS Code
- [Setup minikube](https://minikube.sigs.k8s.io/docs/start/)
  - Skip if you have you own k8s environment
- [nginx for Windows](https://nginx.org/en/docs/windows.html)
  - Skip if you don't need reverse proxy server

## Features
---
- Use NET Standard 2.1 for library projects
- Use NET 8.0 for Web Applications (Migrated from NET 7.0)
- Use Built-In Microsoft ConfigurationBuilder to config (appsettings.json, hellosettings.json)
- Use Secret Storage to protect sensitive configs
- Use Middleware for logging Request/Response
- Use Built-In Microsoft DI to inject most service classes
- Use Data Annotations to validate Request data format
- Follow HTTP Status Code for 2xx, 3xx, 4xx
- Async for HTTP communications
- CamelCase property format
- Use Built-In Microsoft ILogger for Log4net Adapter
- Use Built-In Microsoft OpenAPI for Swagger UI (v1 and v2)
- Use Built-In HealthCheck (~/health)
- Use **.editorconfig** to align coding style
- Switch deployment environment by runtime system environment variable
  - ASPNETCORE_ENVIRONMENT: **Debug**
  - ASPNETCORE_ENVIRONMENT: **Development**
  - ASPNETCORE_ENVIRONMENT: **Testing**
  - ASPNETCORE_ENVIRONMENT: **Staging**
  - ASPNETCORE_ENVIRONMENT: **Production**
- Use xUnit UnitTest Projects


## Projects
---
**Hello.MediatR.Endpoint** is the primary project wraps all the other dependent projects, such as
- CoreFX: Including abstraction design, common utilites
  - CoreFX.Abstractions
  - CoreFX.Common
  - CoreFX.Hosting
  - CoreFX.Logging.Log4net
  - CoreFX.DataAccess.Mapper

- Hello.MediatR: Including domain-driven design, services
  - Hello.MediatR.Domain.Contract
  - Hello.MediatR.Domain.DataAccess.Database
  - Hello.MediatR.Domain.SDK

## Versioning
Whenever any feature, bugfix or necessary to rebuild a new image, make sure you or your builder modify **.version** and **ChangeLog.md** files.
- Version File: `./src/Endpoint/HelloMediatR/.version`
- Version Format: `#.#.#-###`
  - [major version].[minor version].[revision version]-[build version]  (ex: `1.0.1-100`)
  - Adding AZ pipeline's build-id in the suffix
    - #**.**#**.**#**-**###  (ex: `1.0.1-100`)

- ChangeLog File: `./ChangeLog.md`
- ChangeLog Format: Markdown with date and version number, such as
  ```markdown
  ### 2024-09-9
  * **Hello.MediatR.Endpoint (2.0.1)**
    * Upgraded to NET 8.0 from NET 7.0
  ### 2021-04-18
  * **Hello.MediatR.Domain.Contract (1.0.1)**
    * Created
  ### 2023-03-28
  * **Hello.MediatR.Endpoint (1.0.0)**
    * Upgrade to NET 7.0 from NET 5.0
  ```

- Git Version Tags: `hello-mediatr-api/v1.0.1-100`


## UnitTesting

### UnitTest

- Necessary Environment Variables
  - **ASPNETCORE_ENVIRONMENT**: ex: `Debug`
  - **COREFX_API_NAME**: ex: `hello-mediatr-api-debug`
- Expect test results
  - Passed!
  - Duration: less then 1m

```powershell
$env:ASPNETCORE_ENVIRONMENT = 'Debug'
$env:COREFX_API_NAME = 'hello-mediatr-api-debug'

dotnet test tests/UnitTest/CoreFX/UnitTest.CoreFX.csproj -c Release --filter FullyQualifiedName=UnitTest.CoreFX.Mapper_Test.TypeCovert_Test

#  Determining projects to restore...
#  All projects are up-to-date for restore.
#  ...
# Test run for ...
# Microsoft (R) Test Execution Command Line Tool Version 16.8.0
# Copyright (c) Microsoft Corporation.  All rights reserved.

Starting test execution, please wait...
A total of 1 test files matched the specified pattern.

Passed!  - Failed:     0, Passed:     1, Skipped:     0, Total:     1, Duration: 40 s - UnitTest.CoreFX.dll (net6.0)
```
