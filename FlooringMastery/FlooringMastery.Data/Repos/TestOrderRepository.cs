using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;
using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Data.Repos
{
    public class TestOrderRepository : IOrderRepository
    {

        private static Order _order = new Order
        {
            OrderNumber = 1,
            CustomerName = "Wise",
            Area = 100,
            OrderProduct = new Product
            {
                ProductType = "Wood",
                CostPerSquareFoot = 5.15m,
                LaborCostPerSquareFoot = 4.75m
            },
            OrderTax = new Tax
            {
                StateAbbreviation = "OH",
                StateName = "Ohio",
                TaxRate = 6.25m
            }
        };

        private static List<Order> _orders = new List<Order>
        {
             _order
        };

        public void Create(OrderResponse orderResponse)
        {
            _orders.Add(orderResponse.Order);
        }

        public void Delete(OrderResponse orderResponse)
        {
            _orders.Remove(orderResponse.Order);
        }

        public List<Order> LoadOrders(string userDate)
        {   
            return _orders;
        }

        public int NextOrderNumber(string date)
        {
            Order order = new Order();

            LoadOrders(date);

            int OrderNumber = 0;
            foreach (Order item in _orders)
            {
                if (item.OrderNumber > OrderNumber)
                {
                    OrderNumber = item.OrderNumber;
                }
            }
            OrderNumber = OrderNumber + 1;
            return OrderNumber;
        }

        public void SaveOrder(OrderResponse orderResponse)
        {
            _order = orderResponse.Order;
        }

        public void Update(OrderResponse orderResponse)
        {
            _order = orderResponse.Order;
        }
    }
}
