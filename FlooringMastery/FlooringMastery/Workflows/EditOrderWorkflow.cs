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
    public class EditOrderWorkflow
    {
        IUserIO io = new UserIO();
        OrderManager manager = OrderManagerFactory.Create();

        DisplayOrderResponse displayResponse = new DisplayOrderResponse();
        OrderResponse orderResponse = new OrderResponse();

        DateTime userDate = new DateTime();

        string dateString = "";

        int userId;

        public void Execute()
        {
            Console.Clear();

            EditOrderDate();

            userId = io.PromptUserForInt("What order would you like to edit: ");

            ConfirmEditOrder();
        }

        private void EditOrderDate()
        {
            bool isValidDate = false;

            while (isValidDate == false)
            {
                dateString = io.PromptUserForDate("What date would you like to look up: ");
                userDate = DateTime.ParseExact(dateString, "MMddyyyy", CultureInfo.GetCultureInfo("en-us"));
                displayResponse = manager.DisplayOrders(dateString);

                if (displayResponse.Success == true)
                {
                    foreach (Order order in displayResponse.Orders)
                    {
                        io.DisplayOrder(order, userDate);
                    }
                    isValidDate = true;
                }
            }
        }

        private void ConfirmEditOrder()
        {
            bool editValid = false;

            while (editValid == false)
            {
                foreach (Order order in displayResponse.Orders)
                {
                    if (userId == order.OrderNumber)
                    {
                        orderResponse.Order = order;
                        orderResponse.Orders = displayResponse.Orders;
                        orderResponse.StringDate = dateString;
                        orderResponse.DateDate = userDate;

                        EditOrder(order);
                        editValid = true;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a vaild order number");
                    }
                }
            }
        }

        public void EditOrder(Order order)
        {
            bool saveFile = false;

            Console.Clear();
            io.DisplayOrder(order, userDate);
            Console.WriteLine();
            Console.WriteLine();

            string customerName = order.CustomerName;
            Console.WriteLine($"\nCurrent customer name: {order.CustomerName}\n");
            Console.WriteLine("Press enter to leave the name unchanged");
            string newName = EditCustomerName();
            if (newName != "")
            {
                order.CustomerName = newName;
            }

            string customerTax = order.OrderTax.StateAbbreviation;
            Console.WriteLine($"\nCurrent state: {order.OrderTax.StateAbbreviation}\n");
            Console.WriteLine("Press enter to leave the state unchanged");
            string newState = EditState();
            if (newState != "")
            {
                order.OrderTax.StateAbbreviation = newState;
            }

            string customerProduct = order.OrderProduct.ProductType;
            Console.WriteLine($"\nCurrent product: {order.OrderProduct.ProductType}\n");
            Console.WriteLine("Press enter to leave the product unchanged");
            string newProduct = EditProduct();
            if (newProduct != "")
            {
                order.OrderProduct.ProductType = newProduct;
            }

            decimal customerArea = order.Area;
            Console.WriteLine($"\nCurrent area: {order.Area}\n");
            Console.WriteLine("Press enter to leave the area unchanged");
            decimal newArea = EditArea();
            if (newArea != -1)
            {
                order.Area = newArea;
            }

            Console.Clear();
            io.DisplayOrder(order, userDate);
            while (saveFile == false)
            {
                string save = io.PromptUserForString("This is the new order information, would you like to save the order (Y or N)? ");
                switch (save.ToUpper())
                {
                    case "Y":
                        manager.SaveEdit(orderResponse);
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

        public string EditCustomerName()
        {
            bool isValidName = false;
            string userName = "";
            while (isValidName == false)
            {
                userName = io.PromptUserForString("Please enter your company name ([a-z],[0-9], and commas and periods only): ");

                if (userName == "")
                {
                    return userName;
                }
                if (io.ValidateName(userName) == false)
                {
                    Console.WriteLine("Invalid character entered");
                }
                else
                {
                    isValidName = true;
                }

            }
            return userName;
        }

        public string EditState()
        {
            string userState = "";
            bool isValidState = false;
            CheckTaxStateResponse taxResponse = new CheckTaxStateResponse();

            while (isValidState == false)
            {
                userState = io.PromptUserForString("Please enter your State abbreviation (OH format): ");

                if (userState == "")
                {
                    isValidState = true;
                }
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
            return userState;
        }

        public string EditProduct()
        {
            string userProduct = "";
            bool isValidProduct = false;
            CheckProductResponse productResponse = new CheckProductResponse();

            while (isValidProduct == false)
            {
                io.DisplayProducts(manager.GetProducts());

                userProduct = io.PromptUserForString("Please enter your product type: ");

                productResponse = manager.CheckProduct(userProduct);

                if (userProduct == "")
                {
                    isValidProduct = true;
                }
                if (productResponse.Success)
                {
                    isValidProduct = true;
                }
                else
                {
                    Console.WriteLine("Does not match a product type in our files");
                }
            }
            return userProduct;
        }

        public decimal EditArea()
        {
            bool isValidArea = false;
            decimal area = -1;

            while (isValidArea == false)
            {
                area = -1;
                string areaString = io.PromptUserForString("How large is the desired area(square feet): ");
                if (areaString == "")
                {
                    return area;
                }
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
            return area;
        }
    }
}
