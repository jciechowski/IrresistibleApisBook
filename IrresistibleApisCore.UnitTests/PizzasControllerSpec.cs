using System.Linq;
using IrresistibleApisCore.Controllers;
using IrresistibleApisCore.Models;
using IrresistibleApisCore.Repository;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace IrresistibleApisCore.UnitTests
{
    public class PizzasControllerSpec
    {
        private readonly PizzasController _sut;

        public PizzasControllerSpec()
        {
            _sut = new PizzasController(new PizzaRepository());
        }

        [Fact]
        public void GetReturnsInitialListOfPizzas()
        {
            var result = _sut.Get();
            Assert.NotEmpty(result);
        }

        [Fact]
        public void GetByIdReturnsCorrectStatusCodeForExistingPizza()
        {
            var expectedPizza = _sut.Get().First();
            var actual = _sut.Get(expectedPizza.Id) as OkObjectResult;
            Assert.Equal(200, actual.StatusCode);
        }

        [Fact]
        public void GetByIdReturnsCorrectPizzaIfExists()
        {
            var expectedPizza = _sut.Get().First();
            var actual = _sut.Get(expectedPizza.Id) as OkObjectResult;
            Assert.Equal(expectedPizza, actual.Value);
        }

        [Fact]
        public void GetByIdReturnsReturns404WhenPizzaDoesntExists()
        {
            var dummyId = 666;
            var actual = _sut.Get(dummyId) as ContentResult;
            Assert.Equal(404, actual.StatusCode);
        }
    }
}