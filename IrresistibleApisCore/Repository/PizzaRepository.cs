﻿using System.Collections.Generic;
using System.Linq;
using IrresistibleApisCore.Models;

namespace IrresistibleApisCore.Repository
{
    public class PizzaRepository
    {
        private readonly List<Pizza> _pizzas;

        public PizzaRepository() => _pizzas = new List<Pizza>
        {
            new Pizza
            {
                Id = 1,
                Name = "Rural",
                Toppings = new List<int> {1, 2, 3}
            },
            new Pizza
            {
                Id = 2,
                Name = "Margharita",
                Toppings = new List<int> {1}
            },
            new Pizza
            {
                Id = 3,
                Name = "Salami",
                Toppings = new List<int> {1, 2}
            },
        };

        public IEnumerable<Pizza> Get()
        {
            return _pizzas;
        }

        public Maybe<Pizza> Get(int id)
        {
            var pizza = _pizzas.FirstOrDefault(_ => _.Id == id);
            return pizza == null ? Maybe.Empty<Pizza>() : pizza.ToMaybe();
        }

        public Maybe<Pizza> Get(string name)
        {
            if (name == null)
                return Maybe.Empty<Pizza>();

            var pizza = _pizzas.FirstOrDefault(_ => _.Name.Contains(name));
            return pizza == null ? Maybe.Empty<Pizza>() : pizza.ToMaybe();
        }

        public void Add(Pizza pizza)
        {
            if (_pizzas.Any(_ => _.Id == pizza.Id || _.Name == pizza.Name))
                return;

            pizza.Id = _pizzas.Count + 1;
            _pizzas.Add(pizza);
        }

        public void Update(Pizza pizza)
        {
            var pizzaToUpdateIndex = _pizzas.FindIndex(_ => _.Id == pizza.Id);
            if(pizzaToUpdateIndex >= 0)
                _pizzas[pizzaToUpdateIndex] = pizza;
        }

        public void Delete(Pizza pizza)
        {
            var pizzaToDelete = _pizzas.Find(_ => _.Id == pizza.Id && _.Name == pizza.Name);
            _pizzas.Remove(pizzaToDelete);
        }
    }
}