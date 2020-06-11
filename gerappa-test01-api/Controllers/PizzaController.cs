using System;
using System.Threading.Tasks;
using gerappa_test01_api.Data;
using gerappa_test01_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace gerappa_test01_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase, ICrud<Pizza>
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _repository.Delete(id);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]Pizza entity)
        {
            await _repository.Update(entity);
            return Ok();
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _repository.GetAll();
            return Ok(entities);
        }
    }
}