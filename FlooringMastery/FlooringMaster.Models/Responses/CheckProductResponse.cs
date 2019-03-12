using FlooringMaster.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models.Responses
{
    public class CheckProductResponse : Response
    {
        public Product product { get; set; }
    }
}
