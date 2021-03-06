using System.Linq;
using IrresistibleApisCore.Models;
using IrresistibleApisCore.Repository;
using Xunit;

namespace IrresistibleApisCore.UnitTests
{
    public class PizzaRepositorySpec
    {
        private PizzaRepository _sut;

        public PizzaRepositorySpec()
        {
            _sut = new PizzaRepository();
        }

        [Fact]
        public void PopulatePizzasListAfterCreating()
        {
            Assert.Equal(3, _sut.Get().Count());
        }

        [Fact]
        public void ShouldNotAddExistingPizza()
        {
            var numberOfPizzasBeforeAdd = _sut.Get().Count();
            var existingPizza = _sut.Get().First();
            _sut.Add(existingPizza);
            var numberOfPizzasAfterAdd = _sut.Get().Count();
            Assert.Equal(numberOfPizzasBeforeAdd, numberOfPizzasAfterAdd);
        }

        [Fact]
        public void ReturnMaybeAfterAddingPizza()
        {
            var newPizza = new Pizza
            {
                Name = "Dummy"
            };
            _sut.Add(newPizza);
            var result = _sut.Get(newPizza.Name);
            Assert.Equal(result.First().Name, "Dummy");
        }

        [Fact]
        public void ReturnEmptyMaybeWhenPizzaNotFound()
        {
            var result = _sut.Get(555);
            Assert.Empty(result);
        }

        [Fact]
        public void FindPizzaByName()
        {
            _sut.Add(new Pizza
            {
                Name = "Dummy"
            });

            var result = _sut.Get("umm");
            Assert.Equal(result.First().Name, "Dummy");
        }

        [Fact]
        public void AddNewPizza()
        {
            var newPizza = new Pizza {Name = "Dummy"};
            var numberOfPizzasBeforeAdd = _sut.Get().Count();

            _sut.Add(newPizza);
            var numberOfPizzasAfterAdd = _sut.Get().Count();
            Assert.Equal(numberOfPizzasBeforeAdd + 1, numberOfPizzasAfterAdd);
        }

        [Fact]
        public void NewAddedPizzaGetsCorrectId()
        {
            var newPizza = new Pizza {Name = "Dummy"};
            var expectedId = _sut.Get().Count() + 1;

            _sut.Add(newPizza);
            var resultId = _sut.Get(newPizza.Name).First().Id;
            Assert.Equal(expectedId, resultId);
        }

        [Fact]
        public void UpdateExistingPizzaByName()
        {
            var pizza = new Pizza
            {
                Name = "Dummy",
            };

            _sut.Add(pizza);

            var updatedPizza = new Pizza
            {
                Name = "Updated Dummy",
                Id = 4
            };

            _sut.Update(updatedPizza);

            var result = _sut.Get(pizza.Id);
            Assert.Equal(result.First().Name, "Updated Dummy");
        }

        [Fact]
        public void DeleteExistingPizza()
        {
            var allPizzas = _sut.Get().ToList();
            var pizzasCount = allPizzas.Count;
            var pizza = allPizzas.First();
            _sut.Delete(pizza);

            Assert.Equal(pizzasCount - 1, _sut.Get().Count());
        }
    }
}