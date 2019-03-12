using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using FlooringMastery.Data;
using FlooringMastery.Data.Repos;
using FlooringMastery.Models.Interfaces;

namespace FlooringMastery.BLL
{
    public static class OrderManagerFactory
    {
        public static OrderManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            IOrderRepository orderRepository;
            ITaxRepository taxRepository;
            IProductRepository productRepository;

            switch (mode)
            {
                case "Test":
                    orderRepository = new TestOrderRepository();
                    taxRepository = new TestTaxRepository();
                    productRepository = new TestProductRepository();
                    return new OrderManager(orderRepository, taxRepository, productRepository);
                case "Production":
                    orderRepository = new FileOrderRepository();
                    taxRepository = new TaxRepository();
                    productRepository = new ProductRepository();
                    return new OrderManager(orderRepository, taxRepository, productRepository);
                default:
                    throw new Exception("Mode value in app config is not valid");
            }
        }
    }
}
