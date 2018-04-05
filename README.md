# azure-sf-recipes
An API which provides basic CRUD operations for food recipies.

## Architecture
Using Azure Service Fabic, this simple solution comprises of two microservices, an ASP.NET Core WebApi and a catalog service, responsible for maintaining state.

The API is a stateless service which can be have many instances, just update the `Recipes.API_InstanceCount` in `ApplicationManifest.xml`.

While, the catalog service is a stateful service, this service can be partitioned and provide high availability with replica set, again just update the values in `ApplicationManifest.xml`.

## Requirements
- .NET Core 2.0
- Azure Service Fabric SDK

> **Note:** Once Service Fabric SDK is installed, you can use the Service Fabric browser explorer in system task tray to view nodes, instances, partitions etc. Or alternativley, use [Service Fabric Explorer](https://docs.microsoft.com/en-us/azure/service-fabric/service-fabric-visualizing-your-cluster "Service Fabric Explorer") application.

## API endpoints

### Post a recipe
`http://localhost:8808/api/recipes`

```json
{
	"id": "deba9f93-ac91-4c43-80c8-46f002954c71",
	"name" : "Margherita Pizza",
	"description" : "Classic Neapolitan pizza, made with San Marzano tomatoes, mozzarella, fresh basil and extra-virgin olive oil.",
	"servings" : "2"
}
```

### Get all recipes
`http://localhost:8808/api/recipes`

### Get a specific recipe
`http://localhost:8808/api/recipes/deba9f93-ac91-4c43-80c8-46f002954c71`

### Patch a specific recipe
`http://localhost:8808/api/recipes/deba9f93-ac91-4c43-80c8-46f002954c71`

```json
[{
    "op": "replace",
    "path": "/servings",
    "value": "3"
}]
```

### Delete an existing recipe
`http://localhost:8808/api/recipes/deba9f93-ac91-4c43-80c8-46f002954c71`
