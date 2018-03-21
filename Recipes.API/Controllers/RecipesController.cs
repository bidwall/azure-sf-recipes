using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recipes.API.Model;

namespace Recipes.API.Controllers
{
    [Route("api/[controller]")]
    public class RecipesController : Controller
    {
        [HttpGet]
        public async Task<IEnumerable<ApiRecipe>> Get()
        {
            var apiRecipe = new ApiRecipe()
            {
                Id = Guid.NewGuid(),
                Description = "description",
                Name = "name",
                Servings = 1
            };

            return new[] { apiRecipe };
        }

        // GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/values
        [HttpPost]
        public void Post([FromBody]ApiRecipe value)
        {

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
