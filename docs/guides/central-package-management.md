# Central Package Management

All package versions are defined in `Directory.Packages.props` at the repository root.

Project files should use package references without `Version` attributes:

```xml
<PackageReference Include="Dapper" />
```

To update package versions, change only `Directory.Packages.props`.
