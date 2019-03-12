using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models.Interfaces
{
    public interface IUserIO
    {
        string PromptUserForString(string message);
        int PromptUserForInt(string message);
        string PromptUserForDate(string message);
        string GetCustomerName();
        bool ValidateName(string name);
        void DisplayOrder(Order order, DateTime userDate);
        void DisplayProducts(List<Product> products);
    }
}
