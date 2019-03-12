using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly string FILENAME = "Products.txt";

        public ProductRepository()
        {
            LoadFromTxt();
        }

        public List<Product> LoadFromTxt()
        {
            List<Product> results = new List<Product>();
            StreamReader sr = null;

            try
            {
                sr = new StreamReader(FILENAME);
                sr.ReadLine();
                string row = "";
                while ((row = sr.ReadLine()) != null)
                {
                    Product c = ProductMapper.StringToProduct(row);
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
