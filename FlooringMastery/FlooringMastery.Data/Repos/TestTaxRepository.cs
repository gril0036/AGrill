using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Data.Repos
{
    public class TestTaxRepository : ITaxRepository
    {

        private static Tax _tax = new Tax
        {
            StateAbbreviation = "OH",
            StateName = "Ohio",
            TaxRate = 6.25m,
        };
               
        public List<Tax> LoadFromTxt()
        {
            List<Tax> taxes = new List<Tax>();

            taxes.Add(_tax);

            return taxes;
        }
    }
}
