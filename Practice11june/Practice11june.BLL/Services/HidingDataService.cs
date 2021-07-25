using Practice11june.Domain.Model;
using Practice11june.Infrastructure.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Practice11june.Domain;

namespace Practice11june.BLL.Services
{
    public class HidingDataService
    {
        public void HideInfo(string email)
        {
            var userRepo = new DapperRepository<User>();
            var orderRepo = new DapperRepository<Order>();
            var merchantRepo = new DapperRepository<Merchant>();
            var productRepo = new DapperRepository<Product>();
            var orderItemsRepo = new DapperRepository<OrderItem>();

            var user = userRepo.Get(u => u.Email == email).First();
            var merchant = merchantRepo.GetById(user.User_Id);

            if (user != null)
            {
                HideUserInfo();

                if (merchant != null)
                {
                    HideMerchantInfo();
                }
            }

            void HideUserInfo()
            {
                user.FullName = HashString(user.FullName);
                user.Gender = HashString(user.Gender);
                userRepo.Update(user);

                var userId = user.User_Id;

                var userOrders = orderRepo.Get(o => o.User_Id == userId);

                foreach (var order in userOrders)
                {
                    var jsonOrder = JsonSerializer.Deserialize<JsonOrder>(order.OrderJson);

                    if (jsonOrder != null)
                    {
                        jsonOrder.User.FullName = HashString(jsonOrder.User.FullName);
                        order.OrderJson = JsonSerializer.Serialize(jsonOrder);
                        orderRepo.Update(order);
                    }
                }
            }

            void HideMerchantInfo()
            {
                merchant.Name = HashString(merchant.Name);
                merchantRepo.Update(merchant);

                var merchantId = merchant.Merchant_Id;

                var products = productRepo.Get(p => p.Merchant_Id == merchantId);

                var prodId = products.Select(p => p.Product_Id).ToList();

                var orderItems = new List<OrderItem>();

                foreach (var productId in prodId)
                {
                    orderItems.AddRange(orderItemsRepo.Get(oi => oi.Product_Id == productId));
                }

                var ordersId = orderItems.Select(oi => oi.Order_Id).Distinct();

                var merchantOrders = new List<Order>();

                foreach (var id in ordersId)
                {
                    merchantOrders.Add(orderRepo.Get(o => o.Order_Id == id).First());
                }

                foreach (var merchantOrder in merchantOrders)
                {
                    var jsonOrder = JsonSerializer.Deserialize<JsonOrder>(merchantOrder.OrderJson);

                    if (jsonOrder != null)
                    {
                        foreach (var product in jsonOrder.Products)
                        {
                            product.Merchant.Name = HashString(product.Merchant.Name);
                        }

                        merchantOrder.OrderJson = JsonSerializer.Serialize(jsonOrder);
                        orderRepo.Update(merchantOrder);
                    }
                }
            }
        }

        private string HashString(string origin)
        {
            byte[] bytes;

            using (HashAlgorithm algorithm = SHA256.Create())
            {
                bytes = algorithm.ComputeHash(Encoding.UTF8.GetBytes(origin));
            }

            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes)
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
    }
}
