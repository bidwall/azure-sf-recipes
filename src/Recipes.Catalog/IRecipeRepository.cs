using System.Collections.Generic;
using System.Threading.Tasks;
using Recipes.Catalog.Domain;

namespace Recipes.Catalog
{
    public interface IRecipeRepository
    {
        Task<IEnumerable<Recipe>> GetAllRecipes();
        Task AddRecipe(Recipe recipe);
    }
}