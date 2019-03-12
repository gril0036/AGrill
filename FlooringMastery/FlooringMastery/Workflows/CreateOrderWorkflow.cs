using FlooringMastery.BLL;
using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;
using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.UI.Workflows
{
    public class CreateOrderWorkflow
    {
        IUserIO io = new UserIO();
        OrderManager manager = OrderManagerFactory.Create();

        CheckTaxStateResponse taxResponse = new CheckTaxStateResponse();
        CheckProductResponse productResponse = new CheckProductResponse();
        NextOrderNumberResponse orderNumberResponse = new NextOrderNumberResponse();

        OrderResponse orderResponse = new OrderResponse();

        Order order = new Order();
        DateTime userDate = new DateTime();

        string dateString = "";
        string customerName = "";
        string userState = "";
        string userProduct = "";

        decimal area = 0;

        public void Execute()
        {
            CreateOrderDate();

            orderNumberResponse = manager.NextOrderNumber(dateString);

            customerName = io.GetCustomerName();

            CreateCustomerName();

            CreateCustomerProduct();

            CreateCustomerArea();

            SetNewOrderProperties();

            ConfirmNewOrder();
        }

        private void CreateOrderDate()
        {
            bool isValidDate = false;

            Console.Clear();

            while (isValidDate == false)
            {
                dateString = io.PromptUserForDate("Please enter your order date: ");
                userDate = DateTime.ParseExact(dateString, "MMddyyyy", CultureInfo.GetCultureInfo("en-us"));

                if (userDate < DateTime.Today)
                {
                    Console.WriteLine("Invalid date entry, please try again");
                }
                else
                {
                    isValidDate = true;
                }
            }
        }

        private void CreateCustomerName()
        {
            bool isValidState = false;

            while (isValidState == false)
            {
                userState = io.PromptUserForString("Please enter your State abbreviation (OH format): ");

                if (userState.Length != 2)
                {
                    Console.WriteLine("Invalid entry, please try again");
                }
                else
                {
                    userState = userState.ToUpper();
                }

                taxResponse = manager.CheckTax(userState);

                if (taxResponse.Success)
                {
                    isValidState = true;
                }
                else
                {
                    Console.WriteLine("Does not match a State in our files");
                }
            }
        }

        private void CreateCustomerProduct()
        {
            bool isValidProduct = false;

            while (isValidProduct == false)
            {
                io.DisplayProducts(manager.GetProducts());

                userProduct = io.PromptUserForString("Please enter your product type: ");

                productResponse = manager.CheckProduct(userProduct);

                if (productResponse.Success)
                {
                    isValidProduct = true;
                }
                else
                {
                    Console.WriteLine("Does not match a product type in our files");
                }
            }
        }

        private void CreateCustomerArea()
        {
            bool isValidArea = false;

            while (isValidArea == false)
            {
                string areaString = io.PromptUserForString("How large is the desired area(square feet greater than 100): ");

                if (decimal.TryParse(areaString, out area))
                {
                    if (area >= 100)
                    {
                        isValidArea = true;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid entry");
                }
            }
        }

        private void SetNewOrderProperties()
        {
            order.OrderNumber = orderNumberResponse.OrderNumber;
            order.CustomerName = customerName;
            order.OrderTax = taxResponse.Tax;
            order.OrderProduct = productResponse.product;
            order.Area = area;

            orderResponse.Order = order;
            orderResponse.StringDate = dateString;
            orderResponse.DateDate = userDate;
        }

        private void ConfirmNewOrder()
        {
            bool saveFile = false;

            Console.Clear();
            io.DisplayOrder(order, userDate);

            while (saveFile == false)
            {
                string save = io.PromptUserForString("This is the new order information, would you like to save the order (Y or N)? ");
                switch (save.ToUpper())
                {
                    case "Y":
                        orderResponse.Order = order;
                        orderResponse.StringDate = dateString;
                        manager.SaveNewOrder(orderResponse);
                        saveFile = true;
                        break;
                    case "N":
                        saveFile = true;
                        break;
                    default:
                        Console.WriteLine("Please enter Y or N");
                        break;
                }
            }           
        }
    }
}
