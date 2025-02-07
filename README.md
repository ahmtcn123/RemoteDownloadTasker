# RemoteDownloadTasker

This is a try out project for me.

## Features
 - ✅ Clean Architecture
 - ✅ Fluent Validation
 - ✅ Entity Framework Core
 - ✅ PostgreSql
 - ✅ JWT Authentication
 - ✅ Swagger
 - ✅ AutoMapper

## How to run the migration
```bash
dotnet ef migrations add Initial --project src/Infrastructure/Infrastructure.csproj --startup-project src/Api/Api.csproj
````

```bash
dotnet ef database update --project src/Infrastructure/Infrastructure.csproj --startup-project src/Api/Api.csproj
```
