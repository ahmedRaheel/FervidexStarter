# ForgeStarter Enterprise

Production-ready .NET 10 Minimal API starter kit using Vertical Slice Architecture, CQRS, EF Core for writes, Dapper for reads, JWT + refresh tokens, role/policy authorization, Redis caching, Serilog, Docker, Kubernetes, Helm, CI/CD and automated tests.

## Quick start

```bash
cd src/StarterKit.Api
dotnet restore
dotnet ef database update
dotnet run
```

Docker:

```bash
cd deployments
docker compose up --build
```

Default seeded users:

| Email | Password | Role |
|---|---|---|
| admin@forgestarter.dev | Admin@12345 | Admin |
| user@forgestarter.dev | User@12345 | User |

## Commercial Packaging

This repo is designed as a Gumroad-ready source package. Keep docs, deployment examples, and test coverage polished before selling.


## Central Package Management

Package versions are centralized in `Directory.Packages.props`. Do not put `Version` attributes in `.csproj` files.
