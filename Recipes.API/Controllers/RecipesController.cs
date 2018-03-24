using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Recipes.API.Model;
using Recipes.Catalog.Domain;

namespace Recipes.API.Controllers
{
    [Route("api/[controller]")]
    public class RecipesController : Controller
    {
        private readonly IRecipesCatalogService _recipesCatalogService;

        public RecipesController()
        {
            _recipesCatalogService = ServiceProxy.Create<IRecipesCatalogService>(new Uri("fabric:/Recipes/Recipes.Catalog"), 
                                                                                 new ServicePartitionKey(0));
        }

        [HttpGet]
        public async Task<IEnumerable<ApiRecipe>> Get()
        {
            var allRecipies = await _recipesCatalogService.GetAllRecipies();

            return allRecipies.Select(r => new ApiRecipe
            {
                Id = r.Id,
                Description = r.Description,
                Name = r.Name
            });
        }

        // GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody]ApiRecipe recipe)
        {
            var newRecipe = new Recipe
            {
                Id = Guid.NewGuid(),
                Description = recipe.Description,
                Name = recipe.Name,
                Servings = recipe.Servings
            };

            await _recipesCatalogService.AddRecipe(newRecipe);
        }

        // PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
