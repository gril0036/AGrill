using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.UI
{
    public class UserIO : IUserIO
    {
        public string PromptUserForString(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }

        public int PromptUserForInt(string message)
        {
            int result = 0;
            bool isValid = false;

            Console.WriteLine(message);

            while (isValid == false)
            {
                if (int.TryParse(Console.ReadLine(), out result))
                {
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("Please enter a valid number");
                }
            }
            return result;
        }

        public string PromptUserForDate(string message)
        {
            string format = "MMddyyyy";
            DateTime date;
            bool isValid = false;
            bool validDate = false;
            string result = "";

            while (isValid == false)
            {
                string dateString = PromptUserForString(message);
                validDate = DateTime.TryParse(dateString, CultureInfo.GetCultureInfo("en-us"), DateTimeStyles.NoCurrentDateDefault, out date);

                if (date.Year > 2012)
                {
                    result = date.ToString(format);
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("Please enter a valid date");
                }
            };

            return result;
        }

        public string GetCustomerName()
        {
            bool isValidName = false;
            string customerName = "";
            while (isValidName == false)
            {
                customerName = PromptUserForString("Please enter your company name ([a-z],[0-9], and commas and periods only): ");

                if (ValidateName(customerName) == false)
                {
                    Console.WriteLine("Invalid character entered");
                }
                else
                {
                    isValidName = true;
                }

            }
            return customerName;
        }

        public bool ValidateName(string name)
        {
            bool isValid = true;
            while (isValid == true)
            {
                string options = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789,. ";
                foreach (var item in name)
                {
                    if (!options.Contains(item))
                    {
                        isValid = false;
                    }
                }
                break;
            }
            return isValid;
        }

        public void DisplayOrder(Order order, DateTime userDate)
        {
            string date = userDate.ToString("MM/dd/yyyy");

            Console.WriteLine("---------------------");
            Console.WriteLine($"{order.OrderNumber} | {date}");
            Console.WriteLine($"{order.CustomerName}");
            Console.WriteLine($"{order.OrderTax.StateName}");
            Console.WriteLine($"Product : {order.OrderProduct.ProductType}");
            Console.WriteLine($"Materials : {order.MaterialCost:c}");
            Console.WriteLine($"Labor : {order.LaborCost:c}");
            Console.WriteLine($"Tax : {order.Tax:c}");
            Console.WriteLine($"Total : {order.Total:c}");
            Console.WriteLine("---------------------");
        }

        public void DisplayProducts(List<Product> products)
        {
            foreach (Product product in products)
            {
                Console.WriteLine("----------------------------------");
                Console.WriteLine($"Product type: {product.ProductType}");
                Console.WriteLine($"Product cost per square foot: {product.CostPerSquareFoot}");
                Console.WriteLine($"Product labor cost per square foot: {product.LaborCostPerSquareFoot}");
                Console.WriteLine("----------------------------------");
            }
        }
    }
}
