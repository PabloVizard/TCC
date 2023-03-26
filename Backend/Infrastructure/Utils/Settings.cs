using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Utils

{
    public static class Settings
    {
        public static string Secret = "78efb610b603db9e9d8641202efe06fa5caf14c9429e13f0a4eabb37c996f7cf";
        public static string connectionDev = "Server=127.0.0.1;Port=3306;Database=DataBase;User Id=root;Password=1234;Persist Security Info=False; Connect Timeout=300;";
        public static string connectionProd = "";
    }
}
