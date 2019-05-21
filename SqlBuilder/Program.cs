using SqlBuilder.Eval;
using SqlBuilder.SampleModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SqlBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            var users = new List<User> {
                new User { Id = 1, AccessCount = 2, Birth = DateTime.Now.AddYears(-18), Name = "Toothless Dragon", Email = "tooth@less.co", Password="123" },
                new User { Id = 2, AccessCount = 666, Birth = DateTime.Now.AddYears(-50), Name = "The Joker", Email = "joker@batman.com", Password="321" },
                new User { Id = 3, AccessCount = 10, Birth = DateTime.Now.AddYears(-40), Name = "Luke Skywalker", Email = "luke@rebel.com", Password="111222333" },
                new User { Id = 4, AccessCount = 2, Birth = DateTime.Now.AddYears(-40), Name = "Thanos", Email = "thanos@half.com", Password="halfofuniverse" },
                new User { Id = 5, AccessCount = 4, Birth = DateTime.Now.AddYears(-40), Name = "Tony Stark", Email = "imironman@stark.com", Password="@#$%qwtfQWT%q3t5" },
            };

            var whereTranslator = new WhereTranslator();

            var accessCount = 9;
            var mostAccessExpression1 = users.AsQueryable().Where(e => e.AccessCount > accessCount).Expression;
            var mostAccessWhere1 = whereTranslator.BuildExpression(mostAccessExpression1);
            Console.WriteLine("most access, sample #1:");
            Console.WriteLine(mostAccessWhere1);
            Console.WriteLine();

            var mostAccessExpression2 = users.AsQueryable().Where(e => e.AccessCount >= 10).Expression;
            var mostAccessWhere2 = whereTranslator.BuildExpression(mostAccessExpression2);
            Console.WriteLine("most access, sample #2:");
            Console.WriteLine(mostAccessWhere2);
            Console.WriteLine();

            var userByMailExpression = users.AsQueryable().Where(e => e.Email == "thanos@half.com").Expression;
            var userByMailWhere = whereTranslator.BuildExpression(userByMailExpression);
            Console.WriteLine("user by mail:");
            Console.WriteLine(userByMailWhere);
            Console.WriteLine();

            var loginExpression = users.AsQueryable().Where(e => e.Email == "thanos@half.com" && e.Password == "halfofuniverse").Expression;
            var loginWhere = whereTranslator.BuildExpression(loginExpression);
            Console.WriteLine("login:");
            Console.WriteLine(loginWhere);
            Console.WriteLine();

            var nameAndMailAreEqualExpression = users.AsQueryable().Where(e => e.Email == e.Name).Expression;
            var nameAndMailWhere = whereTranslator.BuildExpression(nameAndMailAreEqualExpression);
            Console.WriteLine("name and mail are equal:");
            Console.WriteLine(nameAndMailWhere);
            Console.WriteLine();
        }
    }
}
