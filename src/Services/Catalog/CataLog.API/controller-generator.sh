dotnet aspnet-codegenerator controller \
    -name CatalogController \
    -api \
    -async \
    -m Microsoft.eShopOnContainers.Services.Catalog.API.Mode.CatalogItem \
    -dc CatalogContext \
    -namespace CataLog.API.Controller \
    -outDir Controllers

# ref: https://mattmillican.com/blog/aspnetcore-controller-scaffolding