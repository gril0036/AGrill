using FlooringMastery.Models.Responses;
using FlooringMastery.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;
using FlooringMastery.Data.Repos;

namespace FlooringMastery.BLL
{
    public class OrderManager
    {
        private IOrderRepository _orderRepository;
        private ITaxRepository _taxRepository;
        private IProductRepository _productRepository;

        public OrderManager(IOrderRepository orderRepository, ITaxRepository taxRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _taxRepository = taxRepository;
            _productRepository = productRepository;
        }

        public CheckTaxStateResponse CheckTax(string state)
        {
            CheckTaxStateResponse response = new CheckTaxStateResponse();
            List<Tax> taxes = _taxRepository.LoadFromTxt();

            foreach (Tax item in taxes)
            {
                if (state == item.StateAbbreviation)
                {
                    response.Message = "Success";
                    response.Success = true;
                    response.Tax = item;
                    return response;
                }
                else
                {
                    response.Message = "Invalid entry";
                    response.Success = false;
                }
            }
            return response;           
        }

        public CheckProductResponse CheckProduct(string userProduct)
        {
            CheckProductResponse response = new CheckProductResponse();
            List<Product> products = _productRepository.LoadFromTxt();

            foreach (Product item in products)
            {
                if (userProduct.ToUpper() == item.ProductType.ToUpper())
                {
                    response.Message = "Success";
                    response.Success = true;
                    response.product = item;
                    return response;
                }
                else
                {
                    response.Message = "Invalid entry";
                    response.Success = false;
                }                
            }
            return response;
        }

        public List<Product> GetProducts()
        {
            List<Product> results = _productRepository.LoadFromTxt();
            
            return results;
        }
        
        public NextOrderNumberResponse NextOrderNumber(string date)
        {
            NextOrderNumberResponse response = new NextOrderNumberResponse();

            response.OrderNumber = _orderRepository.NextOrderNumber(date);

            return response;
        }

        public void SaveNewOrder (OrderResponse orderResponse)
        {
            _orderRepository.Create(orderResponse);
        }

        public void SaveEdit (OrderResponse orderResponse)
        {
            _orderRepository.Update(orderResponse);
        }

        public void RemoveOrder(OrderResponse orderResponse)
        {
            _orderRepository.Delete(orderResponse);
        }


        public DisplayOrderResponse DisplayOrders (string date)
        {
            DisplayOrderResponse response = new DisplayOrderResponse();

            response.Orders = _orderRepository.LoadOrders(date);

            if (response.Orders.Count == 0)
            {
                response.Success = false;
            }
            else
            {
                response.Success = true;
            }
            
            return response;
        }
    }
}
