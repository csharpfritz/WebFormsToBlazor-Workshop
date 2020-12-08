# User Controls and Custom Controls

Controls were the backbone of the ASP.NET Web Forms environment.  You could build your own controls to enhance your development experience, using either ASCX files or your own classes that would compose the exact content of the HTML you wanted delivered to your application users.

## User Controls (ASCX)

User controls are the simpler migration, with a direct correlation between the ASCX markup content and the destination Blazor `.razor` file format.

Similar to how we migrated pages, we will create a `.razor` file with the same base name as our user control and, by convention, store it in the `/Components` folder.  This time, when we create our razor file, we will omit the `@page` directive at the top.  This prevents users from being able to navigate directly to our control.

We can then copy into our component any content or any `@inject` directives that we need in order to make our component functional.  

The Hyperlink component for our eShop can be delivered with this simple syntax, residing at [`/src/04-MigratingPages/eShopOnBlazor/Components/HyperLink.razor`](/src/04-MigratingPages/eShopOnBlazor/Components/HyperLink.razor)

```razor
<a href="@NavigateUrl" class="@CssClass">@ChildContent</a>

@code {

 [Parameter]
 public string NavigateUrl { get; set; }

 [Parameter]
 public string CssClass { get; set; }

 [Parameter]
 public RenderFragment ChildContent { get; set; }

}
```

The `Parameter` attribute is a new feature that we haven't seen yet, and provides the interaction we need in order to use attributes on our HTML tags to pass data into our component.

It would be used like this in another Blazor page:

```razor
<HyperLink NavigateUrl="https://bing.com" CssClass="bold">Go to Bing!</HyperLink>
```

The `RenderFragment ChildContent` property is a special "catch-all" property that can be declared to receive any content inside the component tag and redirect that content to the location specified when the `@ChildContent` property is referenced.  The name and type `RenderFragment ChildContent` **MUST** be matched in order to receive the internal content of the control.

## Custom Controls

Custom Controls CAN be migrated and used in our projects, but the syntax and interaction is significantly different.  Instead of inheriting from `Control`, `WebControl` or `CompositeControl` your class will inherit from `Microsoft.AspNetCore.Components.ComponentBase` and you will work extensively with the `BuildRenderTree` method to add HTML tags to the content that Blazor will render.

We could build our own Bold tag that outputs a `<strong>` element with the following syntax:

```csharp
 public class Bold : ComponentBase
 {

  [Parameter] public RenderFragment ChildContent { get; set; }

  [Parameter] public string CssClass { get; set; }

  protected override void BuildRenderTree(RenderTreeBuilder builder)
  {

   builder.OpenElement(0, "strong");
   builder.AddAttribute(1, "class", CssClass);
   builder.AddContent(2, ChildContent);
   builder.CloseElement();

   base.BuildRenderTree(builder);
  }

 }
```

This is not a common scenario, and will not be covered further.

## Razor Class Libraries

Content from these components can be re-used and deployed to other Blazor projects by assembling them in a separate project called a Razor Class library.

```dotnetcli
dotnet new razorclasslib -o MyComponents
```

You will then have a project with a wwwroot folder, a razor file, and some CSS to go with it.  Starting with .NET 5, CSS files named with `component.razor.css` will be conditionally included in your HTML output when the component is included on a Blazor page. Content from the wwwroot folder like images and scripts can be referenced in your HTML content using the syntax `_content/MyComponents/` where `MyComponents` is the name of your razor class library.

This library will compile into a NuGet package that can then be referenced in your other projects or deployed to NuGet.org for use by the public.

Previous - [Session 3: Migrating Services, Modules, and Handlers](06-migrating-other.md)