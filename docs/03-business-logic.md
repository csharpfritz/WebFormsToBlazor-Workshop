# .NET Standard and Business Logic

Our business logic and data interactions for the application can be safely moved from .NET Framework and embedded in our application into a .NET Standard library that presents the same features in a cross-framework capable binary.

## Setup

You can create a new .NET Standard project on the command-line with this command:

```dotnetcli
dotnet new classlib -f netstandard2.0 -o eShop.Core
dotnet new sln -name eShopOnBlazor
dotnet sln add eShop.Core
```

## Migrate the classes

In this case, we already have a nicely formatted data model that resided in the `/Models` and `ViewModel` folder of the application.  We can move, not copy, those files into our new project so that they can be used by the existing Web Forms application AND our new Blazor application.

This has the added benefit of migrating and validating your business logic in-place while you prepare the new application platform.

Don't forget to fix up any namespace references in the classes so that they refer to the new `eShop.Core` namespace.

We also need to bring in the DataAnnotations feature, so let's add that package to the project:

```dotnetcli
dotnet add package System.ComponentModel.Annotations
```

## Migrate other class libraries

Other class libraries that your project may reference can be ported to .NET Standard.  The [.NET Portability Analyzer tool](https://docs.microsoft.com/dotnet/standard/analyzers/portability-analyzer) is available to help identify if your code can be ported and what changes may be needed to ensure it works in .NET Standard.

The general strategy recommended is to create a new project, as we did above, and copy in your classes from the existing library.

## Re-introduce the data objects to the .NET Framework project

We can now update the .NET Framework project by adding a reference to the new library.  Since our existing project is .NET 4.7.2, our .NET Standard project is referenced cleanly.  If your .NET Framework project is .NET 4.7.1 or earlier, we recommend updating to .NET 4.8 for maintained support and portability of your logic to .NET Standard.

Previous - [Session 1: WebForms and Blazor Comparison](02-webforms-and-blazor-comparison.md)

Next up - [Session 2: Migrating Pages & Components](04-migrating-pages.md)