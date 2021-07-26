using System;
using System.Collections.Generic;
using System.Configuration;

namespace ADOPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            

            var expression7_1 = @"select p.ProductCategoryID, c.Name, Sum(d.LineTotal) as SoldSumma
                                 from SalesLT.Product p
                                    join SalesLT.ProductCategory c on p.ProductCategoryID = c.ProductCategoryID
                                    join SalesLT.SalesOrderDetail d on d.ProductID = p.ProductID
                                 group by p.ProductCategoryID, c.Name";

            var expression7_2 = @"select c.FirstName, Max(d.UnitPriceDiscount) as maxDiscount
                                 from SalesLT.Customer c
                                 join SalesLT.SalesOrderHeader h on h.CustomerID = c.CustomerID
                                 join SalesLT.SalesOrderDetail d on d.SalesOrderID = h.SalesOrderID
                                 where d.UnitPriceDiscount = @UnitPriceDiscount
                                 group by c.FirstName
                                 order by Max(d.UnitPriceDiscount)";

            var expression7_3 = @"select c.CustomerID, c.FirstName, c.MiddleName, c.LastName, Sum(h.TotalDue) as total
                                 from SalesLT.Customer c
                                 join SalesLT.SalesOrderHeader h on h.CustomerID = c.CustomerID
                                 group by c.CustomerID, c.FirstName, c.MiddleName, c.LastName
                                 having Sum(h.TotalDue)>@Value";

            var expression4_1 = @"select top(10) p.ProductID, p.Name, p.ProductNumber, p.Weight
                                  from SalesLT.Product p";

            var expression5_2 = @"select c.CustomerID, c.FirstName, c.MiddleName, c.LastName, c.EmailAddress, c.Phone, a.City, a.CountryRegion, a.PostalCode, a.AddressLine1, a.AddressLine2
                                 from SalesLT.Customer c
                                 	join SalesLT.CustomerAddress ca on ca.CustomerID = c.CustomerID
                                 	join SalesLT.Address a on a.AddressID = ca.AddressID";

            var expression6_6 = @"select MIN(p.Weight) as minimum, MAX(p.Weight) as maximum
                                 from SalesLT.Product p";


            var fourOne = CRUDOperations.GetResultNonParametrized(connectionString, expression4_1, 4);

            var fiveTwo = CRUDOperations.GetResultNonParametrized(connectionString, expression5_2, 11);

            var sixSix = CRUDOperations.GetResultNonParametrized(connectionString, expression6_6, 2);

            var sevenOne = CRUDOperations.GetResultNonParametrized(connectionString, expression7_1, 3);

            var discount = 0.40;
            var sevenTwo = CRUDOperations.GetResultParametrized(connectionString, expression7_2, 2, new Dictionary<string, object> { { "@UnitPriceDiscount", discount } });

            var value = 15000;
            var sevenThree = CRUDOperations.GetResultParametrized(connectionString, expression7_3, 5, new Dictionary<string, object> { { "@Value", value} });

            Console.WriteLine("Вывести 10 первых продуктов");
            CRUDOperations.ToConsole(fourOne);
            Console.WriteLine();

            Console.WriteLine("Вывести идентификтор кастомера, фио, емейл и телефон, город, страну, почтовый код и адрес");
            CRUDOperations.ToConsole(fiveTwo);
            Console.WriteLine();

            Console.WriteLine("Вычислить макс и мин вес для продуктов");
            CRUDOperations.ToConsole(sixSix);
            Console.WriteLine();

            Console.WriteLine("Вывести ид и имя категории и суммарную стоимость всех продуктов в ней, который были проданы");
            CRUDOperations.ToConsole(sevenOne);
            Console.WriteLine();

            Console.WriteLine("Вывести всех кастомеров у который макс скидка когда либо купленого товара составбляет 40 и более процентов");
            CRUDOperations.ToConsole(sevenTwo);
            Console.WriteLine();

            Console.WriteLine("Вывести ид и фио всех кастомеров, у который суммарная стоимость купленых продуктов более 15000");
            CRUDOperations.ToConsole(sevenThree);


            //Zadanie 8
            var newConnectionString = ConfigurationManager.ConnectionStrings["AdditionTask"].ConnectionString;

            var InsertBookExpression = @"insert into Books.dbo.Books (Title, SeriaId, PublisherId, ISBN, LangCode) values (@Title, @SeriaId, @PublisherId, @ISBN, @LangCode)";

            var bookBySeriaIdExpression = @"select * from Books.dbo.Books b where b.SeriaId = @SeriaId";

            //Enter a book with parameters
            var title = "1984";
            var seriaId = 6;
            var publisherId = 6;
            var isbn = "0-500-8-0-21";
            var lang = "rus";

            var firstExpressionParameters = new Dictionary<string, object>();
            firstExpressionParameters.Add("@Title", title);
            firstExpressionParameters.Add("@SeriaId", seriaId);
            firstExpressionParameters.Add("@PublisherId", publisherId);
            firstExpressionParameters.Add("@ISBN", isbn);
            firstExpressionParameters.Add("@LangCode", lang);

            var affected = CRUDOperations.Insert_Update_Delete_Parametrized(newConnectionString, InsertBookExpression, firstExpressionParameters);
            Console.WriteLine(affected);

            //Find book by seria Id with parameter
            var seria = 6;

            var secondExpressionParameters = new Dictionary<string, object>();
            secondExpressionParameters.Add("@SeriaId", seria);
            var result = CRUDOperations.GetResultParametrized(newConnectionString, bookBySeriaIdExpression, 6, secondExpressionParameters);
            CRUDOperations.ToConsole(result);
        }
    }
}
