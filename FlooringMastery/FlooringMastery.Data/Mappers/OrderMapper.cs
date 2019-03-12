using FlooringMastery.Data.Repos;
using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Data
{
    public class OrderMapper
    {
        public static Order StringToOrder(string row)
        {
            Order order = new Order();

            string[] fields = row.Split(new string[] { "::" }, StringSplitOptions.None);

            Product product = new Product
            {
                ProductType = fields[4],
                CostPerSquareFoot = decimal.Parse(fields[6]),
                LaborCostPerSquareFoot = decimal.Parse(fields[7])
            };

            Tax tax = new Tax
            {
                StateAbbreviation = fields[2],
                StateName = StateNameConverter(fields[2]),
                TaxRate = decimal.Parse(fields[3])
            };                       

            order.OrderNumber = int.Parse(fields[0]);
            order.CustomerName = fields[1];
            order.OrderProduct = product;
            order.Area = decimal.Parse(fields[5]);
            order.OrderTax = tax;
            order.OrderProduct = product;

            return order;
        }

        private static string StateNameConverter(string stateAbbreviation)
        {
            ITaxRepository _taxRepository = new TaxRepository();
            List<Tax> taxes = _taxRepository.LoadFromTxt();

            string result = "";

            foreach (Tax item in taxes)
            {
                if (stateAbbreviation == item.StateAbbreviation)
                {
                    result = item.StateName;
                }
            }
            return result;
        }

        public static string OrderToString(Order order)
        {
            string format = "0.00";

            string row = $"{order.OrderNumber.ToString()}::{order.CustomerName}::{order.OrderTax.StateAbbreviation}::{order.OrderTax.TaxRate.ToString(format)}::{order.OrderProduct.ProductType}::{order.Area.ToString(format)}::{order.OrderProduct.CostPerSquareFoot.ToString(format)}::{order.OrderProduct.LaborCostPerSquareFoot.ToString(format)}::{order.MaterialCost.ToString(format)}::{order.LaborCost.ToString(format)}::{order.Tax.ToString(format)}::{order.Total.ToString(format)}";

            return row;
        }
    }
}
//OrderNumber – int
//CustomerName – string
//State – string
//TaxRate – decimal
//ProductType – string
//Area – decimal
//CostPerSquareFoot – decimal
//LaborCostPerSquareFoot – decimal
//MaterialCost – decimal
//LaborCost – decimal
//Tax – decimal
//Total – decimal