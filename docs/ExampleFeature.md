
# Example Feature - Users

This feature demonstrates:
- Vertical Slice implementation
- Command/Query separation
- Validation
- EF Core configuration
- Seeding
- Testing

## Components

- CreateUserCommand
- GetUserByIdQuery
- CreateUserValidator
- UserConfiguration
- UserSeeder
- Endpoint mappings

## Recommended Structure

```text
Features/
 └── Users/
     ├── Commands/
     ├── Queries/
     ├── Validators/
     ├── Endpoints/
     ├── DTOs/
     └── Mapping/
```
