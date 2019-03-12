using FlooringMastery.Models;
using FlooringMastery.Data;
using FlooringMastery.Data.Mappers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Tests
{
    [TestFixture]
    class MapperTests
    {
        
        [TestCase("1::Wise::OH::6.25::Wood::100.00::5.15::4.75::515.00::475.00::61.88::1051.88", 1, "Wise", "OH", 6.25, "Wood", 100.00, 5.15, 4.75, 515.00, 475.00, 61.88, 1051.88)]
        public void StringToOrderMapperTest(string row, int orderNumber, string customerName, string state, decimal taxRate, string productType, decimal area, decimal costPerFoot, decimal laborPerFoot, decimal material, decimal labor, decimal tax, decimal total)
        {
            Product product = new Product()
            {
                ProductType = productType,
                CostPerSquareFoot = costPerFoot,
                LaborCostPerSquareFoot = laborPerFoot
            };

            Tax taxes = new Tax()
            {
                StateAbbreviation = state,
                TaxRate = taxRate
            };

            Order order = new Order()
            {
                OrderNumber = orderNumber,
                CustomerName = customerName,
                Area = area,
                OrderProduct = product,
                OrderTax = taxes
            };

            Order result = OrderMapper.StringToOrder(row);

            Assert.AreEqual(result.OrderNumber, order.OrderNumber);
            Assert.AreEqual(result.CustomerName, order.CustomerName);
            Assert.AreEqual(result.OrderTax.TaxRate, order.OrderTax.TaxRate);
            Assert.AreEqual(result.OrderProduct.ProductType, order.OrderProduct.ProductType);
            Assert.AreEqual(result.Area, order.Area);
            Assert.AreEqual(result.OrderProduct.CostPerSquareFoot, order.OrderProduct.CostPerSquareFoot);
            Assert.AreEqual(result.OrderProduct.LaborCostPerSquareFoot, order.OrderProduct.LaborCostPerSquareFoot);
            Assert.AreEqual(result.MaterialCost, order.MaterialCost);
            Assert.AreEqual(result.LaborCost, order.LaborCost);
            Assert.AreEqual(result.Tax, order.Tax);
            Assert.AreEqual(result.Total, order.Total);
        }

        [TestCase(1, "Wise", "OH", 6.25, "Wood", 100.00, 5.15, 4.75, 515.00, 475.00, 61.88, 1051.88, "1::Wise::OH::6.25::Wood::100.00::5.15::4.75::515.00::475.00::61.88::1051.88")]
        public void OrderToStringMapperTest(int orderNumber, string customerName, string state, decimal taxRate, string productType, decimal area, decimal costPerFoot, decimal laborPerFoot, decimal material, decimal labor, decimal tax, decimal total, string row)
        {
            Product product = new Product()
            {
                ProductType = productType,
                CostPerSquareFoot = costPerFoot,
                LaborCostPerSquareFoot = laborPerFoot
            };

            Tax taxes = new Tax()
            {
                StateAbbreviation = state,
                TaxRate = taxRate
            };

            Order order = new Order()
            {
                OrderNumber = orderNumber,
                CustomerName = customerName,
                Area = area,
                OrderProduct = product,
                OrderTax = taxes
            };

            string result = OrderMapper.OrderToString(order);

            Assert.AreEqual(result, row);
        }

        [TestCase("Carpet,2.25,2.10", "Carpet", 2.25, 2.10)]
        [TestCase("Laminate,1.75,2.10", "Laminate", 1.75, 2.10)]
        [TestCase("Tile,3.50,4.15", "Tile", 3.50, 4.15)]
        [TestCase("Wood,5.15,4.75", "Wood", 5.15, 4.75)]
        public void ToProductMapperTest(string row, string productType, decimal costPerSquareFoot, decimal laborCostPerSquareFoot)
        {
            Product product = new Product();

            product.ProductType = productType;
            product.CostPerSquareFoot = costPerSquareFoot;
            product.LaborCostPerSquareFoot = laborCostPerSquareFoot;

            Product result = ProductMapper.StringToProduct(row);

            Assert.AreEqual(result.ProductType, product.ProductType);
            Assert.AreEqual(result.CostPerSquareFoot, product.CostPerSquareFoot);
            Assert.AreEqual(result.LaborCostPerSquareFoot, product.LaborCostPerSquareFoot);
        }

        [TestCase("OH,Ohio,6.25", "OH", "Ohio", 6.25)]
        [TestCase("PA,Pennsylvania,6.75", "PA", "Pennsylvania", 6.75)]
        [TestCase("MI,Michigan,5.75", "MI", "Michigan", 5.75)]
        [TestCase("IN,Indiana,6.00", "IN", "Indiana", 6.00)]
        public void ToTaxMapperTest(string row, string stateAbbreviation, string stateName, decimal taxRate)
        {
            Tax tax = new Tax();

            tax.StateAbbreviation = stateAbbreviation;
            tax.StateName = stateName;
            tax.TaxRate = taxRate;

            Tax result = TaxMapper.StringToTax(row);

            Assert.AreEqual(result.StateAbbreviation, tax.StateAbbreviation);
            Assert.AreEqual(result.StateName, tax.StateName);
            Assert.AreEqual(result.TaxRate, tax.TaxRate);
        }
    }
}
