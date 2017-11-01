using IrresistibleApisCore.Repository;
using Microsoft.AspNetCore.Mvc;

namespace IrresistibleApisCore.Controllers
{
    [Route("api/[controller]")]
    public class ToppingsController : Controller
    {
        private readonly ToppingRepository _toppingRepository;

        public ToppingsController(ToppingRepository toppingRepository)
        {
            _toppingRepository = toppingRepository;
        }
    }
}