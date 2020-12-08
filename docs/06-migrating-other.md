# Migrating Services, HttpModules and HttpHandlers

These migrations are not as common as migrating pages, but are part of the ASP.NET framework and for completeness we are providing this documentation to help with migration.

## Services

In ASP.NET, there were several models for providing services from our application to other applications:

- SOAP Services
- WCF
- WebAPI

Unfortunately, in ASP.NET Core and Blazor we do not have similar services to offer that match SOAP or WCF.  SOAP has been deprecated and will need to be replaced.  We recommend using the new OpenAPI templates that are now used as a standard throughout the web.  Your SOAP classes can be recreated as OpenAPI definitions.  More information about OpenAPI is available in [the .NET docs](https://docs.microsoft.com/aspnet/core/tutorials/web-api-help-pages-using-swagger)

WCF has also been deprecated, and there is a team building a community-supported implementation of WCF services for .NET Core.  The [CoreWCF project](https://github.com/CoreWCF/CoreWCF) is sponsored by the .NET Foundation and has been adding features over the past year.  The project, as of December 2020 declares that it is NOT production ready.

An alternative that the .NET team recommends is to migrate your WCF services to the new gRPC model.  This migration is documented in [a complete eBook on the Microsoft Docs website](https://docs.microsoft.com/dotnet/architecture/grpc-for-wcf-developers/migrate-wcf-to-grpc).

WebAPI is available in .NET Core and .NET 5, and you can migrate your `ApiController` classes with slight modification to ASP.NET Core as `Controller` classes and similar interactions.  A [walk-through of these steps](https://docs.microsoft.com/aspnet/core/migration/webapi) is available in the .NET Docs.

### Recommendation

We recommend leaving these services in-place, and maintaining a small footprint server that your new application can interact with as you begin to migrate them to the newer service models.

Wrap your clients that interact with those services in a `Client` class with an appropriate interface so that you can swap out the client references after these services are migrated in the future.

## HttpModules

In our original ASP.NET application, there was an HttpModule-like configuration that used log4Net to log information about requests that came through the application using the `Application_BeginRequest` event.  HttpModules could reside in extra files and defined as classes that would intercept these events in the application lifecycle and add value to our interactions.

The original C# looked like this:

```csharp
protected void Application_BeginRequest(object sender, EventArgs e)
{
  //set the property to our new object
  LogicalThreadContext.Properties["activityid"] = new ActivityIdHelper();

  LogicalThreadContext.Properties["requestinfo"] = new WebRequestInfo();

}
```

This interaction, along with the other events handled by the HttpModule are now covered by the Http pipeline defined in the `Startup.Configure` method.  We can insert this same interaction with the `app.Use` middleware syntax using the following:

```csharp
app.Use((ctx, next) =>
{
  LogicalThreadContext.Properties["activityid"] = new ActivityIdHelper(ctx);
  LogicalThreadContext.Properties["requestinfo"] = new WebRequestInfo(ctx);
  return next();
});
```

Additional documentation and [strategy for migrating HttpModule are available](https://docs.microsoft.com/aspnet/core/migration/http-modules).

## HttpHandlers

HttpHandlers were a low-ceremony way to provide direct interaction with the Http request in ASP.NET.  We can recreate this same type of request in the Http pipeline in `Startup.Configure` using the `MapWhen` syntax:

```csharp
app.MapWhen(
  context => context.Request.Path.ToString().EndsWith(".foo"),
  appBranch => {
    appBranch.Use(ctx, next) => {
      // do my task
      Console.WriteLine("Foo was requested");
    }
  });
```

The .NET Docs have more information about [configuring middleware to behave like HttpHandlers](https://docs.microsoft.com/aspnet/core/migration/http-modules#migrating-handler-code-to-middleware).

Previous - [Lab 2: Migrating eShopOnWebForms](05-migrating-site.md)

Next Up (Optional) - Discussion: Middleware and Hosted Services in ASP.NET Core

Next Up - [Session 4: User Controls and Custom Controls](07-migrating-controls.md)