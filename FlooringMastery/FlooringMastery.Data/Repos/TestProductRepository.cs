using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Data.Repos
{
    public class TestProductRepository : IProductRepository
    {
        private static Product _product = new Product
        {
            ProductType = "Carpet",
            CostPerSquareFoot = 2.25m,
            LaborCostPerSquareFoot = 2.10m
        };

        public List<Product> LoadFromTxt()
        {
            List<Product> products = new List<Product>();

            products.Add(_product);

            return products;
        }
    }
}
