﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gerappa_test01_api.Data;
using gerappa_test01_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace gerappa_test01_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase, ICrud<Order>
    {
        private readonly IRepository<Order> _repository;

        public OrderController(IRepository<Order> repository)
        {
            _repository = repository;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Order order)
        {
            order.Id = Guid.NewGuid().ToString();
            order.OrderDate = DateTime.Now;
            order.TotalPrice = order.Pizza.Price;
            order.Name = $"{order.Pizza.Name} - {order.Client.Name}";

            await _repository.Add(order);
            return Ok(order);
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
        public async Task<IActionResult> Update(Order entity)
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