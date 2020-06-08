using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gerappa_test01_api.Models;
using gerappa_test01_api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace gerappa_test01_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly ICosmosDbService _cosmosDb;

        public PizzaController(ICosmosDbService cosmosDb)
        {
            _cosmosDb = cosmosDb;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Pizza pizza)
        {
            pizza.Id = Guid.NewGuid().ToString();
            await _cosmosDb.AddItemAsync(pizza);
            return Ok(pizza);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var pizza = await _cosmosDb.GetItemAsync(id);
            return Ok(pizza);
        }
    }
}