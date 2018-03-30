using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Client;
using Recipes.API.Model;
using Recipes.CatalogService.Domain;

namespace Recipes.API.Controllers
{
    [Route("api/[controller]")]
    public class RecipesController : Controller
    {
        private readonly ICatalogService _catalogService;

        public RecipesController()
        {
            var serviceProxyFactory = new ServiceProxyFactory(context => new FabricTransportServiceRemotingClientFactory());
            _catalogService = serviceProxyFactory.CreateServiceProxy<ICatalogService>(new Uri("fabric:/Recipes/CatalogService"), new ServicePartitionKey(0));
        }

        [HttpGet]
        public async Task<IEnumerable<RecipeModel>> Get()
        {
            try
            {
                var recipes = await _catalogService.GetRecipes();

                return recipes.Select(r => new RecipeModel
                {
                    Id = r.Id,
                    Description = r.Description,
                    Name = r.Name
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        // GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody]RecipeModel recipeModel)
        {
            var newRecipe = new Recipe
            {
                Id = Guid.NewGuid(),
                Description = recipeModel.Description,
                Name = recipeModel.Name,
                Servings = recipeModel.Servings
            };

            await _catalogService.AddRecipe(newRecipe);
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
