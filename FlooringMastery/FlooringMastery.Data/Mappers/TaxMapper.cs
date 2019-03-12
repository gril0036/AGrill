using FlooringMastery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Data.Mappers
{
    public class TaxMapper
    {
        public static Tax StringToTax(string row)
        {
            Tax t = new Tax();
            string[] fields = row.Split(',');

            t.StateAbbreviation = fields[0];
            t.StateName = fields[1];
            t.TaxRate = decimal.Parse(fields[2]);
            return t;
        }

    }
}
