using FlooringMastery.UI.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.BLL;

namespace FlooringMastery
{
    public static class Menu
    {
        public static void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Flooring Program");
                Console.WriteLine();
                Console.WriteLine("1) Display Order");
                Console.WriteLine("2) Add Order");
                Console.WriteLine("3) Edit Order");
                Console.WriteLine("4) Remove Order");
                Console.WriteLine();
                Console.WriteLine("5) Quit");
                Console.WriteLine();
                Console.WriteLine("Enter your selection:");
                string userInput = Console.ReadLine();

                switch (userInput.ToUpper())
                {
                    case "1":
                        OrderLookupWorkflow lookupWorkflow = new OrderLookupWorkflow();
                        lookupWorkflow.Execute();
                        break;
                    case "2":
                        CreateOrderWorkflow createWorkFlow = new CreateOrderWorkflow();
                        createWorkFlow.Execute();
                        break;
                    case "3":
                        EditOrderWorkflow editWorkflow = new EditOrderWorkflow();
                        editWorkflow.Execute();
                        break;
                    case "4":
                        RemoveOrderWorkflow removeWorkflow = new RemoveOrderWorkflow();
                        removeWorkflow.Execute();
                        break;
                    case "5":
                    case "Q":
                        Environment.Exit(0);
                        return;
                }
            }
        }
    }
}
