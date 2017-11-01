using System.Collections.Generic;

namespace IrresistibleApisCore.Models
{
    public class Pizza
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<int> Toppings { get; set; }
    }
}