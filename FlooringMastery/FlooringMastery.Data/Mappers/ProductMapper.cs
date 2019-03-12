using FlooringMastery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Data
{
    public class ProductMapper
    {
        public static Product StringToProduct(string row)
        {
            Product p = new Product();
            string[] fields = row.Split(',');

            p.ProductType = fields[0];
            p.CostPerSquareFoot = decimal.Parse(fields[1]);
            p.LaborCostPerSquareFoot = decimal.Parse(fields[2]);
            return p;
        }
    }
}
