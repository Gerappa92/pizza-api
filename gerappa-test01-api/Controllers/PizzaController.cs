using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gerappa_test01_api.Data;
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
        private readonly IRepository<Pizza> _repository;

        public PizzaController(IRepository<Pizza> repository)
        {
            this._repository = repository;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Pizza pizza)
        {
            pizza.Id = Guid.NewGuid().ToString();
            await _repository.Add(pizza);
            return Ok(pizza);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var pizza = await _repository.Get(id);
            return Ok(pizza);
        }
    }
}