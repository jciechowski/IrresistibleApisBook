using System.Linq;
using IrresistibleApisCore.Models;
using IrresistibleApisCore.Repository;
using Xunit;

namespace IrresistibleApisCore.UnitTests
{
    public class PizzaRepositorySpec
    {
        [Fact]
        public void ShouldPopulatePizzasListAfterCreating()
        {
            var sut = new PizzaRepository();
            Assert.Equal(3, sut.Get().Count());
        }

        [Fact]
        public void ShouldReturnEmptyMaybeWhenAddingExistingPizza()
        {
            var sut = new PizzaRepository();
            var existingPizza = sut.Get().First();
            var result = sut.Add(existingPizza);
            Assert.Equal(result, new Maybe<Pizza>());
        }

        [Fact]
        public void ShouldReturnMaybeAfterAddedPizza()
        {
            var sut = new PizzaRepository();
            var newPizza = new Pizza
            {
                Name = "Dummy"
            };
            var result = sut.Add(newPizza);
            Assert.Equal(result.First().Name, "Dummy");
        }

        [Fact]
        public void ShouldReturnEmptyMaybeWhenPizzaNotFound()
        {
            var sut = new PizzaRepository();
            var result = sut.Get(555);
            Assert.Empty(result);
        }

        [Fact]
        public void ShouldFindPizzaByName()
        {
            var sut = new PizzaRepository();
            sut.Add(new Pizza
            {
                Name = "Dummy"
            });

            var result = sut.Get("umm");
            Assert.Equal(result.First().Name, "Dummy");
        }

        [Fact]
        public void ShouldUpdateExistingPizza()
        {
            var sut = new PizzaRepository();
            var pizza = new Pizza
            {
                Name = "Dummy",
                Id = 666
            };

            sut.Add(pizza);

            var updatedPizza = new Pizza
            {
                Name = "Updated Dummy",
                Id = 666
            };

            sut.Update(updatedPizza);

            var result = sut.Get(pizza.Id);
            Assert.Equal(result.First().Name, "Updated Dummy");
        }
    }
}