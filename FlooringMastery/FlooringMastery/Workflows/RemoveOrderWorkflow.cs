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
    public class RemoveOrderWorkflow
    {
        IUserIO io = new UserIO();
        OrderManager manager = OrderManagerFactory.Create();

        DisplayOrderResponse displayResponse = new DisplayOrderResponse();
        OrderResponse orderResponse = new OrderResponse();

        DateTime userDate = new DateTime();

        bool removeFile = false;

        string dateString = "";

        public void Execute()
        {
            bool isValidDate = false;
            bool idValid = false;

            int userId = 0;

            Console.Clear();

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

            userId = io.PromptUserForInt("What order would you like to remove: ");

            while (idValid == false)
            {
                foreach (Order order in displayResponse.Orders)
                {
                    if (userId == order.OrderNumber)
                    {
                        ConfirmRemoveOrder(order);
                        idValid = true;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a vaild order number");
                    }
                }
            }
        }

        public void ConfirmRemoveOrder(Order order)
        {
            Console.Clear();
            io.DisplayOrder(order, userDate);

            orderResponse.Order = order;
            orderResponse.Orders = displayResponse.Orders;
            orderResponse.StringDate = dateString; 
            orderResponse.DateDate = userDate;

            while (removeFile == false)
            {
                string save = io.PromptUserForString("This is the order information, would you like to remove the order (Y or N)? ");
                switch (save.ToUpper())
                {
                    case "Y":
                        manager.RemoveOrder(orderResponse);
                        removeFile = true;
                        break;
                    case "N":
                        removeFile = true;
                        break;
                    default:
                        Console.WriteLine("Please enter Y or N");
                        break;
                }
            }
        }
    }
}
