using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;
using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Data.Repos
{
    public class FileOrderRepository : IOrderRepository
    {
        private static List<Order> orders;
        private string FILENAME;

        public int NextOrderNumber(string date)
        {
            orders = LoadOrders(date);

            int OrderNumber = 0;
            foreach (Order order in orders)
            {
                if (order.OrderNumber > OrderNumber)
                {
                    OrderNumber = order.OrderNumber;
                }
            }
            OrderNumber = OrderNumber + 1;
            return OrderNumber;
        }

        public void Create(OrderResponse orderResponse)
        {
            orderResponse.Orders = LoadOrders(orderResponse.StringDate);
            orderResponse.Orders.Add(orderResponse.Order);

            if (!File.Exists($"Orders_{orderResponse.StringDate}.txt"))
            {
                File.Create($"Orders_{orderResponse.StringDate}.txt").Close();
            }

            SaveOrder(orderResponse);
        }

        public void Update(OrderResponse orderResponse)
        {
            for (int i = 0; i < orderResponse.Orders.Count; i++)
            {
                if (orderResponse.Orders[i].OrderNumber != orderResponse.Order.OrderNumber) continue;
                orders[i] = orderResponse.Order;
                break;
            }

            SaveOrder(orderResponse);
        }

        public void Delete(OrderResponse orderResponse)
        {
            foreach (var item in orderResponse.Orders)
            {
                if (item.OrderNumber == orderResponse.Order.OrderNumber)
                {
                    orders.Remove(item);

                    break;
                }
            }
            SaveOrder(orderResponse);
        }

        public List<Order> LoadOrders(string userDate)
        {
            FILENAME = $"Orders_{userDate}.txt";
            List<Order> results = new List<Order>();
            StreamReader sr = null;

            try
            {
                sr = new StreamReader(FILENAME);
                sr.ReadLine();
                string row = "";
                while ((row = sr.ReadLine()) != null)
                {
                    Order o = OrderMapper.StringToOrder(row);
                    results.Add(o);
                }
                orders = results;

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (sr != null) sr.Close();
            }
            return results;
        }

        public void SaveOrder(OrderResponse orderResponse)
        {
            StreamWriter sw = null;

            try
            {
                sw = new StreamWriter($"Orders_{orderResponse.StringDate}.txt");
                sw.Write("OrderNumber::CustomerName::State::TaxRate::ProductType::Area::CostPerSquareFoot::LaborCostPerSquareFoot::MaterialCost::LaborCost::Tax::Total");

                foreach (Order order in orderResponse.Orders)
                {
                    sw.Write("\n" + OrderMapper.OrderToString(order));
                    sw.Flush();
                }

            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong");
            }
            finally
            {
                if (sw != null) sw.Close();
            }
        }
    }
}

