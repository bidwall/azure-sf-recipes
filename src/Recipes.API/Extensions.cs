using Recipes.API.Model;
using Recipes.CatalogService.Domain;

namespace Recipes.API
{
    internal static class Extensions
    {
        public static Recipe ToDomain(this RecipeModel model)
        {
            return new Recipe
            {
                Description = model.Description,
                Id = model.Id,
                Name = model.Name,
                Servings = model.Servings
            };
        }

        public static RecipeModel ToModel(this Recipe domain)
        {
            return new RecipeModel
            {
                Description = domain.Description,
                Id = domain.Id,
                Name = domain.Name,
                Servings = domain.Servings
            };
        }
    }
}
