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


    }
}
