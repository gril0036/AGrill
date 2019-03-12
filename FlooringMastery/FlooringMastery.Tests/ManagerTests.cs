using FlooringMastery.BLL;
using FlooringMastery.Models;
using FlooringMastery.Models.Responses;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Tests
{
    [TestFixture]
    public class ManagerTests
    {
        OrderManager manager = OrderManagerFactory.Create();
        
        
        [TestCase("MN", false)]
        [TestCase("OH", true)]
        public void CheckTaxTest(string state, bool expected)
        {
            CheckTaxStateResponse response = new CheckTaxStateResponse();

            response = manager.CheckTax(state);

            Assert.AreEqual(response.Success, expected);
        }


        [TestCase("Wood", false)]
        [TestCase("Carpet", true)]
        public void CheckProductTest(string product, bool expected)
        {
            CheckProductResponse response = new CheckProductResponse();

            response = manager.CheckProduct(product);

            Assert.AreEqual(response.Success, expected);
        }

 
        [TestCase("01012020", 2)]
        public void CheckOrderNumberTest(string date, int expected)
        {
            NextOrderNumberResponse response = new NextOrderNumberResponse();

            response = manager.NextOrderNumber(date);

            Assert.AreEqual(response.OrderNumber, expected);
        }
        

        [TestCase("01012020", true)]
        public void DisplayOrderTest(string date, bool expected)
        {
            DisplayOrderResponse response = new DisplayOrderResponse();

            response = manager.DisplayOrders(date);

            Assert.AreEqual(response.Success, expected);
        }
    }
}
