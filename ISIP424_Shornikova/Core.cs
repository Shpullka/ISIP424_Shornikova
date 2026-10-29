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

        public static User CurrentUser = null; 
        public static string RegisterUser(string nickname, string password1, string password2)
        {
            if (password1 != password2)
            {
                return "Пароли не совпадают!";
            }
            if (Context.User.Any(p => p.Nickname == nickname))
            {
                return "Пользователь с таким никнеймом уже существует!";
            }

            User newUser = new User
            {
                Nickname = nickname, 
                PasswordHash = password1
            };
            Context.User.Add(newUser);
            Context.SaveChanges();
            return "Успешно";
        }

        public static bool Login(string nickname, string password)
        {
            CurrentUser = Context.User.FirstOrDefault(p => p.Nickname == nickname && p.PasswordHash == password);
            return CurrentUser != null;
        }

        public static void Logout()
        {
            CurrentUser = null;
            Console.WriteLine("Вы вышли из аккаунта");
        }

        public static List<Product> GetProducts()
        {
            return Context.Product.ToList();
        }

        public static string AddToCart(int userID, int productID)
        {
            if (CurrentUser == null)
            {
                return "Необходимо войти в аккаунт";
            }
            var product = Context.Product.FirstOrDefault(p => p.ProductID == productID);
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

        public static List<CartItem> GetCartItems()
        {
            if (CurrentUser == null)
            {
                return new List<CartItem>();
            }
            return Context.CartItem.Where(p => p.UserID == CurrentUser.UserID).ToList();
        }

        public static bool RemoveFromCart(int productID)
        {
            var item = Context.CartItem.FirstOrDefault(p => p.UserID == CurrentUser.UserID && p.ProductID == productID);
            if (item != null)
            {
                Context.CartItem.Remove(item);
                Context.SaveChanges();
                Console.WriteLine("Товар успешно удален из корзины");
                return true;
            }
            Console.WriteLine($"Товара с ID {item.ProductID} нет в вашей корзине");
            return false;
        }

        public static List<PickupPoint> GetPickupPoint()
        {
            return Context.PickupPoint.ToList();
        }

        public static string CheckoutSelected(List<int> selectedProductsID, int pickupPointID)
        {
            var cartItems = GetCartItems();
            if (cartItems.Count == 0)
            {
                return "Корзина пуста";
            }
            var itemsToBuy = Context.CartItem.Where(p => p.UserID == CurrentUser.UserID && selectedProductsID.Contains(p.ProductID)).ToList();
            if (itemsToBuy.Count == 0)
            {
                return "Вы не выбрали ни одного товара из корзины";
            }

            Order newOrder = new Order
            {
                UserID = CurrentUser.UserID,
                PickupPointID = pickupPointID,
                CreatedAt = DateTime.Now,
                IsCompleted = false
            };
            Context.Order.Add(newOrder);
            Context.SaveChanges();

            foreach (var item in itemsToBuy)
            {
                Context.OrderProduct.Add(new OrderProduct
                {
                    OrderID = newOrder.OrderID,
                    ProductID = item.ProductID
                });
            }
            Context.CartItem.RemoveRange(itemsToBuy);
            Context.SaveChanges();
            return $"Заказ успешно оформлен! Куплено товаров: {itemsToBuy.Count}";
        }

        public static List<Order> GetOrderHistory()
        {
            if (CurrentUser == null)
            {
                return new List<Order>();
            }
            return Context.Order.Where(p => p.OrderID == CurrentUser.UserID).OrderByDescending(p => p.CreatedAt).ToList();
        }

        public static string GetProductName(int productID)
        {
            var product = Context.Product.FirstOrDefault(p => p.ProductID == productID);
            if (product != null)
            {
                return product.Name;
            }
            return "Неизвестный товар";
        }

        public static string GetPickupPointAddress(int pickupPointID)
        {
            var pickupPoint = Context.PickupPoint.FirstOrDefault(p => p.PickupPointID == pickupPointID);
            if (pickupPoint != null)
            {
                return pickupPoint.Address;
            }
            return "Неизвестный адрес";
        }

        public static List<string> GetProductInOrder(int orderID)
        {
            List<string> productsName = new List<string>();
            foreach (var product in Context.OrderProduct)
            {
                if (product.OrderID == orderID)
                {
                    var products = Context.Product.FirstOrDefault(p => p.ProductID == product.ProductID);
                    if (products != null)
                    {
                        productsName.Add(products.Name);
                    }
                }
            }
            return productsName;
        }
    }
}
