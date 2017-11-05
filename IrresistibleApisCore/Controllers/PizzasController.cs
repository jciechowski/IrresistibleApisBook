using System.Collections.Generic;
using System.Linq;
using IrresistibleApisCore.Models;
using IrresistibleApisCore.Repository;
using Microsoft.AspNetCore.Mvc;

namespace IrresistibleApisCore.Controllers
{
    [Route("api/[controller]")]
    public class PizzasController : Controller
    {
        private readonly PizzaRepository _pizzaRepository;

        public PizzasController(PizzaRepository pizzaRepository)
        {
            _pizzaRepository = pizzaRepository;
        }

        [HttpGet]
        public IEnumerable<Pizza> Get()
        {
            return _pizzaRepository.Get();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var pizza = _pizzaRepository.Get(id);

            if (pizza.Any())
                return new ObjectResult(pizza);
            return NotFound();
        }

        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            var pizza = _pizzaRepository.Get(name);
            if (pizza.Any())
                return Ok(pizza.First());
            return NotFound();
        }

        [HttpPost]
        public IActionResult Post([FromBody] Pizza pizza)
        {
            _pizzaRepository.Add(pizza);

            var maybePizza = _pizzaRepository.Get(pizza.Name);
            if (maybePizza.Any())
            {
                return new ContentResult
                {
                    Content = $"/pizzas/{maybePizza.First().Id}",
                    StatusCode = 201
                };
            }
            return new ContentResult
            {
                Content = "Duplicate pizza name/id",
                StatusCode = 409
            };
        }

//        [HttpPut("{id")]
//        public IActionResult Put([FromBody] Pizza pizza)
//        {
//            _pizzaRepository.Update(pizza);
//            var updatedPizza = _pizzaRepository.Get(pizza.Name);
//            if(updatedPizza)
//            return Ok();
//        }
    }
}