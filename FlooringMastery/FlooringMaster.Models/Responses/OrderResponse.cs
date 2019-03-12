using FlooringMaster.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models.Responses
{
    public class OrderResponse : Response
    {
        public List<Order> Orders { get; set; }
        public Order Order { get; set; }
        public string StringDate { get; set; }
        public DateTime DateDate { get; set; }
    }
}
