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
            // -> example of how to implement a LINQ provider:
            //      https://blogs.msdn.microsoft.com/mattwar/2008/11/18/linq-building-an-iqueryable-provider-series/

            // -> Image of Expression:
            //      http://ryanohs.com/wp-content/uploads/2016/04/ExpressionTree.png

            var users = new List<User> {
                new User { Id = 1, AccessCount = 2, Birth = DateTime.Now.AddYears(-18), Name = "Toothless Dragon", Email = "tooth@less.co", Password="123" },
                new User { Id = 2, AccessCount = 666, Birth = DateTime.Now.AddYears(-50), Name = "The Joker", Email = "joker@batman.com", Password="321" },
                new User { Id = 3, AccessCount = 10, Birth = DateTime.Now.AddYears(-40), Name = "Luke Skywalker", Email = "luke@rebel.com", Password="111222333" },
                new User { Id = 4, AccessCount = 2, Birth = DateTime.Now.AddYears(-40), Name = "Thanos", Email = "thanos@half.com", Password="halfofuniverse" },
                new User { Id = 5, AccessCount = 4, Birth = DateTime.Now.AddYears(-40), Name = "Tony Stark", Email = "imironman@stark.com", Password="@#$%qwtfQWT%q3t5" },
            };

            var usersQuery = users.AsQueryable();

            var translator = new SqlTranslator();

            var accessCount = 9;
            var mostAccessQuery1 = usersQuery.Where(e => e.AccessCount > accessCount);
            var mostAccessSQL1 = translator.BuildExpression(mostAccessQuery1);
            Console.WriteLine("most access sql, sample #1:");
            Console.WriteLine(mostAccessSQL1);
            Console.WriteLine();

            var mostAccessQuery2 = usersQuery.Where(e => e.AccessCount >= 10);
            var mostAccessSQL2 = translator.BuildExpression(mostAccessQuery2);
            Console.WriteLine("most access sql, sample #2:");
            Console.WriteLine(mostAccessSQL2);
            Console.WriteLine();

            var mostAccessOrderedQuery = usersQuery.OrderBy(e => e.AccessCount);
            var mostAccessOrderedSQL = translator.BuildExpression(mostAccessOrderedQuery);
            Console.WriteLine("most access ordered sql:");
            Console.WriteLine(mostAccessOrderedSQL);
            Console.WriteLine();

            var userByMailQuery = usersQuery.Where(e => e.Email == "thanos@half.com");
            var userByMailSQL = translator.BuildExpression(userByMailQuery);
            Console.WriteLine("user by mail sql:");
            Console.WriteLine(userByMailSQL);
            Console.WriteLine();

            var loginQuery = usersQuery.Where(e => e.Email == "thanos@half.com" && e.Password == "halfofuniverse");
            var loginSQL = translator.BuildExpression(loginQuery);
            Console.WriteLine("login sql:");
            Console.WriteLine(loginSQL);
            Console.WriteLine();

            var nameAndMailAreEqualQuery = usersQuery.Where(e => e.Email == e.Name);
            var nameAndMailSQL = translator.BuildExpression(nameAndMailAreEqualQuery);
            Console.WriteLine("name and mail are equal sql:");
            Console.WriteLine(nameAndMailSQL);
            Console.WriteLine();
        }
    }
}
