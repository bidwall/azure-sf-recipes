using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Exceptions;
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

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var recipe = await _catalogService.GetRecipe(id);

            if (recipe != null)
            {
                return Ok(recipe);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RecipeModel recipe)
        {
            await _catalogService.SaveRecipe(recipe.ToDomain());

            return StatusCode(201);
        }

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody]JsonPatchDocument<RecipeModel> recipePatch)
        {
            var recipe = await _catalogService.GetRecipe(id);
            if (recipe == null)
                return NotFound();

            try
            {
                var recipeModel = recipe.ToModel();
                recipePatch.ApplyTo(recipeModel);
                await _catalogService.SaveRecipe(recipeModel.ToDomain());

                return Ok(recipeModel);
            }
            catch (JsonPatchException exception)
            {
                return BadRequest($"An error occured whilst updating book. {exception.Message}.");
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var recipe = _catalogService.GetRecipe(id);
            if (recipe == null)
                return NotFound();

            await _catalogService.DeleteRecipe(id);

            return NoContent();
        }
    }
}
