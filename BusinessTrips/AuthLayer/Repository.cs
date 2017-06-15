using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessTrips.AuthLayer
{
    public static class Repository
    {
        static List<User> users = new List<User>() {

        new User() {Email="admin@mailSibCemAdmin.ru",Roles="Admin,Editor",Password="admin" },
        new User() {Email="kadr@mailSibCemKadr.ru",Roles="Editor",Password="editor" }
    };

        public static User GetUserDetails(User user)
        {
            return users.Where(u => u.Email.ToLower() == user.Email.ToLower() &&
            u.Password == user.Password).FirstOrDefault();
        }
    }
}