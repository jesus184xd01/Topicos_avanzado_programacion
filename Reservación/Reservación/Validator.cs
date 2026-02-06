using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservación
{
    public static class Validator
    {
        public static User Validate(string username, string email, string password)
        {
            // validacion de las credenciales del usuario admin para poder ingresar
            if (username == "admin" && password == "password")
            {
                return new User
                {
                    user_name = username,
                    role = "Admin",
                    email = email
                };
            }

            return null;
        }
    }
}
