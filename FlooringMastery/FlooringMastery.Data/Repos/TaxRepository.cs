using FlooringMastery.Models;
using FlooringMastery.Data.Mappers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models.Interfaces;

namespace FlooringMastery.Data.Repos
{
    public class TaxRepository : ITaxRepository
    {
        private readonly string FILENAME = "Taxes.txt";

        public TaxRepository()
        {
            LoadFromTxt();
        }

        public List<Tax> LoadFromTxt()
        {
            List<Tax> results = new List<Tax>();
            StreamReader sr = null;

            try
            {
                sr = new StreamReader(FILENAME);
                sr.ReadLine();
                string row = "";
                while ((row = sr.ReadLine()) != null)
                {
                    Tax c = TaxMapper.StringToTax(row);
                    results.Add(c);
                }

            }
            catch (FileNotFoundException fileNotFound)
            {
                Console.WriteLine(fileNotFound.FileName + " was not found");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (sr != null) sr.Close();
            }
            return results;
        }
    }
}
