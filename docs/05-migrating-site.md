# Migrating the eShopOnWebForms

Using the same techniques from the previous session, we will migrate the `Catalog/Create`, `Catalog/Delete`, `Catalog/Details` and `Catalog/Edit` pages.

## Migrate Details and Delete

These two pages use almost identical HTML to their original counterparts.  Copy the content to `Pages/Catalog/Details` and `Pages/Catalog/Delete` and update the Razor markup appropriately.

Update the page directive to allow submission of the catalog ID on the request URL:

```razor
@page "/Catalog/Details/{id:int}"
```

Add the inject command to add the CatalogService:

```razor
@inject ICatalogService CatalogService
```

Capture the submitted id and fetch the CatalogItem appropriately by adding this code to the page:

```razor
@code {
    private CatalogItem _item;

    [Parameter]
    public int Id { get; set; }

    protected override void OnInitialized()
    {
        Logger.LogInformation("Now loading... /Catalog/Details/{Id}", Id);

        _item = CatalogService.FindCatalogItem(Id);
    }
}
```

## Create and Edit -- Form Data

The Create and Edit pages require a little more work to support the forms present on those pages, and the Blazor standard components have form components that will serve us.

The final rendition of this lab resides in `/src/04-MigratingPages`

Previous - [Session 2: Migrating Pages & Components](04-migrating-pages.md)

Next Up - [Session 3: Migrating Services, Modules, and Handlers](06-migrating-other.md)