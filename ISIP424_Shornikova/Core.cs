using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP424_Shornikova
{
    public static class Core
    {
        public static Shpulka_424Entities Context = new Shpulka_424Entities();
        public static string RegisterUser(string nickname, string password1, string password2)
        {
            if (password1 != password2)
            {
                return "Пароли не совпадают!";
            }
            

        }

        public static List<Product> GetProducts()
        {
            return Context.Product.ToList();
        }

        public static string AddToCart(int userID, int productID)
        {
            var product = Context.Product.Find(productID);
            if (product == null || !product.IsAvailable)
            {
                return "Товар не найден или отсутствует в наличии";
            }
            Context.CartItem.Add(new CartItem
            {
                UserID = userID,
                ProductID = productID
            });
            Context.SaveChanges();
            return "Товар добавлен в корзину!";
        }

        public static List<CartItem> GetCart(int userID)
        {
            return Context.CartItem.Include(p => p.Product).Where(p => p.UserID == userID).ToList();
        }

        public static void RemoveFromCart(int cartItemID)
        {
            var item = Context.CartItem.Find(cartItemID);
            if (item != null)
            {
                Context.CartItem.Remove(item);
                Context.SaveChanges();
            }
        }

        public static List<PickupPoint> GetPickupPoint()
        {
            return Context.PickupPoint.ToList();
        }

        public static string CreateOrder(int userID, int pickupPointID, List<CartItem> itemToBuy)
        {
            if (!Context.PickupPoint.Any(p => p.PickupPointID == pickupPointID))
            {
                return "ПВЗ не найден";
            }
            var order = new Order
            {
                UserID = userID,
                PickupPointID = pickupPointID,
                CreatedAt = DateTime.Now,
                IsCompleted = false
            };
            Context.Order.Add(order);
            Context.SaveChanges();

            foreach (var item in itemToBuy)
            {
                Context.OrderProduct.Add(new OrderProduct
                {
                    OrderID = order.OrderID,
                    ProductID = item.ProductID
                });
            }

            var cartItem = Context.CartItem.FirstOrDefault(p => p.CartItemID ==  )
        }
    }
}
