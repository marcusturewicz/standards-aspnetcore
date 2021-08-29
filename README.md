<p align="center"><a href="https://github.com/marcusturewicz/Standards.AspNetCore"><img src="logo.png" alt="Standards.AspNetCore logo"/></a></p>

<p align="center">Library of standards implemented to use in ASP.NET Core.</p>

<div align="center">

[![CI/CD](https://github.com/marcusturewicz/standards-aspnetcore/actions/workflows/cicd.yml/badge.svg)](https://github.com/marcusturewicz/standards-aspnetcore/actions/workflows/cicd.yml)
[![Nuget](https://img.shields.io/nuget/v/Standards.AspNetCore)](https://www.nuget.org/packages/Standards.AspNetCore)
[![Nuget](https://img.shields.io/nuget/dt/Standards.AspNetCore)](https://www.nuget.org/packages/Standards.AspNetCore)

</div>

For example, it provides convenient abstractions for enforcing `DateTime` objects are binded with ISO-8601 format (YYYY-MM-DD) in MVC.



## Compatibility

Standards.AspNetCore currently targets .NET 5.0+.

## Getting Started

Standards.AspNetCore is available as a [NuGet package](https://www.nuget.org/packages/Standards.AspNetCore). Install from nuget.org:

```
dotnet add package Standards.AspNetCore --version 1.0.0-alpha.3
```

## Features

### `IsoDateModelBinder` and `IsoDateModelBinderProvider`

Useful for ensuring consistent Date format contracts in RESTful APIs. `IsoDateModelBinder` enforces `DateTime` model binding with ISO-8601 format (YYYY-MM-DD) in MVC. This can be used in two ways:

1. In single Controller actions:

```cs
[HttpGet]
public IActionResult Get([ModelBinder(typeof(IsoDateModelBinder))] DateTime date) {
    return Ok("Date is in ISO format");
}
```

2. Set globally in the application via Startup:

```cs
public override void ConfigureServices(IServiceCollection services) {
    services.AddControllers(options => {
        options.ModelBinderProviders.Insert(0, new IsoDateModelBinderProvider());
    })
}
```

Then, only dates in ISO-8601 format (YYYY-MM-DD) can be binded. For example, `/api?date=2021-01-01` will be successfully binded. However, `/api?date=01-01-2021` will not, and the following error message will be added to the problem details response:

```
"Invalid date; must be in ISO-8601 format i.e. YYYY-MM-DD."
```

## Roadmap

Currently only `IsoDateModelBinder` is implemented. The intention is for further useful international standards to be implemented in this library.

## Contributing

Check out the [contributing](CONTRIBUTING.md) page to see the best ways to contribute.

## Code of conduct

See the [Code of Conduct](CODE_OF_CONDUCT.md) for the best ways to interact with this project.

## License

Standards.AspNetCore is licensed under the [MIT license](LICENSE).







