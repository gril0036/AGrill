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
    public class OrderLookupWorkflow
    {
        IUserIO io = new UserIO();
        OrderManager manager = OrderManagerFactory.Create();
        DisplayOrderResponse response = new DisplayOrderResponse();

        public void Execute()
        {
            Console.Clear();

            string dateString = "";



            dateString = io.PromptUserForDate("What date would you like to look up: ");
            DateTime userDate = DateTime.ParseExact(dateString, "MMddyyyy", CultureInfo.GetCultureInfo("en-us"));
            response = manager.DisplayOrders(dateString);

            if (response.Success == true)
            {
                foreach (Order order in response.Orders)
                {
                    io.DisplayOrder(order, userDate);
                }
            }
            Console.WriteLine("Press enter to exit to main menu : ");
            Console.ReadLine();
        }
    }
}
