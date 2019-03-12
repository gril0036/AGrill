using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models.Interfaces
{
    public interface IOrderRepository
    {
        int NextOrderNumber(string date);
        void Create(OrderResponse orderResponse);
        void Update(OrderResponse orderResponse);
        void Delete(OrderResponse orderResponse);
        List<Order> LoadOrders(string userDate);
        void SaveOrder(OrderResponse orderResponse);
    }
}
