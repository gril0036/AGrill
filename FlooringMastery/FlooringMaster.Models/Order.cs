using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models
{
    public class Order
    {
        public int OrderNumber { get; set; }
        public string CustomerName { get; set; }
        public decimal Area { get; set; }
        public decimal MaterialCost { get => Area * OrderProduct.CostPerSquareFoot; }
        public decimal LaborCost { get => Area * OrderProduct.LaborCostPerSquareFoot; }
        public decimal Tax { get => (MaterialCost + LaborCost) * (OrderTax.TaxRate / 100); }
        public decimal Total { get => MaterialCost + LaborCost + Tax; }
        public Tax OrderTax { get; set; }
        public Product OrderProduct { get; set; }
    }
}
