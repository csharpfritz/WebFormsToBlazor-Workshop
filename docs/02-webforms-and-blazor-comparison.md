# Web Forms and Blazor Comparison

Blazor and Web Forms are both frameworks that deliver web applications using the .NET stack.  There are a significant number of similarities that will help you migrate your application easier and faster than choosing another framework.

## Why Migrate?

The first question you should ask yourself, after learning what Blazor is and reviewing your existing ASP.NET application is:  

**Why do we want to migrate this application?**

Migrating an ASP.NET application to Blazor is not a quick or easy task.  Before committing your project team to this endeavor, confirm that this is the next step for your project.  There are several good reasons to choose this migration:

- ASP.NET Web Forms is supported through .NET Framework 4.8 and is not receiving new features
- ASP.NET Web Forms runs on Windows only, and you would like to host your application on a Linux production environment
- You would like to use new deployment technologies like Docker containers and Kubernetes to manage our application

.NET Framework and ASP.NET Web Forms will continue to receive support from Microsoft until 2029 under the current support cycle.  With each release of Windows 10, this support windows is extended FURTHER.

## The Possible Solutions

Some project teams have chosen to migrate their applications with different technologies and have found challenges:

### WRONG!  ASP.NET MVC - ASP.NET Core MVC

- Dramatic shift in programming model 
  - No components available, straight HTML output
  - More testable
- Still ASP.NET, and can move to .NET Core / .NET 5 with modifications
- Can run in the same application as Web Forms, creating a “FrankenApp”

### WRONG!  Angular / Vue / React

- Similar UI component-driven development model
  - Open Sourcce components available
  - Unit Testing frameworks available

- Change from C# / VB to JavaScript

- Does not match 1:1 with the code and architecture presented in ASPX pages / ASCX controls

### Blazor

- Similar UI component-driven development model
  - Open Source components available
  - Unit Testing frameworks available - bUnit

- Keep writing C#
  - Use .NET Standard / .NET 5 libraries

- What if it could match 1:1 with the code and architecture presented in ASPX pages / ASCX controls?

## Similarities

| Web Forms | Blazor Server-Side |
| --- | --- |
| Application configured in global.asax | Application configured in Startup.cs |
| Pages and Controls | Components that can be Pages |
| Build with raw HTML mixed with controls | Build with Razor templates and components |
| Renders content on server | Renders content on server |
| Execute and interact with JavaScript | Execute and interact with JavaScript |
| References logic in class libraries | References logic in class libraries |

## Differences

| Web Forms | Blazor Server-Side |
| --- | --- |
| ASPX files with <% %> notation | Razor files with @ notation |
| ViewState | State managed on server |
| Postback | SignalR channel |
| MasterPages | LayoutComponents |
| .NET Framework 1.0 - 4.x | .NET Core 3.0+ |
| Libraries in .NET Framework | Components in .NET Standard 2+ |
| Runs in IIS on Windows | Runs on Windows, Mac or Linux |

## Migration Strategy

There are several high-level steps to your migration:

1. Prepare the existing application for Migration
1. Migrate existing business logic to .NET Standard class libraries
1. Create a new Blazor Server project as the destination for your Web Forms
1. Migrate Master Pages to Blazor Layout
1. Migrate User Controls to Blazor Components
1. Migrate ASPX to Blazor Pages
1. Rewrite / Migrate Custom Controls

In this workshop, we'll move the User Controls step to the end, as our demo app doesn't have any user controls in the initial sample.

### Prepare the existing application

Not all applications will migrate smoothly. We suggest you evaluate and update the application **IN-PLACE** to simplify the following architecture concerns that will need to be addressed in Blazor:

- ASPX in-line Visual Basic not supported on Razor components in .NET Core
  - Convert to C# or move out to a class library
- No data source controls on ASPX pages
- Business logic needs to be convertible to .NET Core
- Class libraries referenced need to be convertible to .NET Standard
- No 3rd party control libraries that don't have a shim for conversion (none available at the time of this writing)
- Prefer model-binding techniques over handling Form life-cycle events like `Init`, `Load`, `PreRender`, and `Unload`
  - These event-handlers do not exist and function the same way in Blazor.  Avoid acting outside of the `Load` event.  The actions you are taking in `Load` can be executed at the conclusion of the `Initialize` event in Blazor
- Use a repository pattern with interfaces declared for the repositories
  - Inject the concrete implementation of the data access technology.  It _MAY_ change to HTTP access at some point in the future
- Minimal code embedded in ASPX files - this code will need to be updated to work with the components, where they were using full-featured controls.
- No calls through the control hierarchy.  E.g. `FindControl()`
- Any hybrid applications that contains both MVC and Web Forms user interface content will take some extra work to be converted, as the MVC components will need to be converted to ASP<span></span>.NET Core
- `System.Configuration.ConfigurationManager` access will need to be rewritten to use `Microsoft.Extensions.Configuration.IConfiguration` objects that are injected
  - Update configuration access to utilize a repository class object that can be migrated to the new Configuration model in .NET Core
- `HttpContext.Current` access will need to be reevaluated.  Direct `HttpContext` access should be avoided in Blazor
- Custom components will need to be rewritten, and are preferred as User Controls (ASCX)
- Mobile device detection is not available as part of Blazor.  `Site.mobile.master` will not be directly used by the framework.
  - Instead of using an alternate rendering strategy for mobile, we recommend you embrace an adaptive rendering strategy to ensure that mobile device visitors to your application get a good experience

### .NET Standard

[__.NET Standard Compatibility Table, up to .NET Standard 2.0__](https://github.com/dotnet/standard/blob/master/docs/versions.md)

![.NET Standard Compatibility](img/02-dotnetstandard.png)

.NET Standard is a formal specification of .NET APIs that are available on multiple .NET implementations. The motivation behind .NET Standard was to establish greater uniformity in the .NET ecosystem.

Starting with .NET Standard 2.0, the .NET Framework compatibility mode was introduced. This compatibility mode allows .NET Standard projects to reference .NET Framework libraries as if they were compiled for .NET Standard. Referencing .NET Framework libraries doesn't work for all projects, such as libraries that use Windows Presentation Foundation (WPF) APIs.

.NET 5 is the implementation of .NET that Microsoft is actively developing. It's a single product with a uniform set of capabilities and APIs that can be used for Windows desktop apps and cross-platform console apps, cloud services, and websites.  

.NET 5 libraries cannot be referenced by .NET Framework projects, but .NET Standard 2.0 libraries can be referenced by both .NET Framework and .NET 5 projects.  Therefore, we will migrate our Web Forms business logic to .NET Standard 2.0 libraries.

In our next lab, we will migrate business logic to a .NET Standard library that can be referenced by our existing .NET Framework application and our new Blazor application.

Previous - [Demo 1: eShopOnBlazor](01-eshop-on-blazor.md)

Next up (optional) - Discussion: Current State of Web Forms Applications

Next up - [Lab 1: Business Logic & .NET Standard](03-business-logic.md)
