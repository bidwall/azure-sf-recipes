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
            var recipes = await _catalogService.GetRecipes();

            return recipes.Select(r => r.ToModel());
        }

        // GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody]RecipeModel recipe)
        {
            await _catalogService.SaveRecipe(recipe.ToDomain());
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
