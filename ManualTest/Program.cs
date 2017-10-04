using Logic.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ManualTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = new FastFoodContext())
            {
                User admin = new User();
                admin.UserName = "admin";
                admin.MotDePass = "admin";
                admin.UserType = typeUser.Admin;
                ctx.Users.Add(admin);

                User simple = new User();
                simple.UserName = "simple";
                simple.MotDePass = "simple";
                simple.UserType = typeUser.Simple;
                ctx.Users.Add(simple);
                ctx.SaveChanges();
            }
        }
    }
}
