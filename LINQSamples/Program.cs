using LINQSamples.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQSamples
{
    

    class CustomerModel
    {
        public CustomerModel()
        {
            this.Orders = new List<OrderModel>();
        }

        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int OrderCount { get; set; }
        public List<OrderModel> Orders { get; set; }
    }

    class OrderModel
    {
        public int OrderId { get; set; }
        public decimal Total { get; set; }
        public List<ProductModel> Products { get; set; }
    }

    public class ProductModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            using (var db = new CustomNorthwindContext())
            {
                //var sonuc = db.Database.ExecuteSqlRaw("delete from products where productId = 81");
                //var sonuc = db.Database.ExecuteSqlRaw("update products set unitprice = unitprice *1.2 where categoryId = 4");
                //var query = "4";
                //var products = db.Products.FromSqlRaw($"select * from products where categoryId = {query}").ToList();


                var products = db.ProductModels.FromSqlRaw("select ProductId, ProductName , UnitPrice from Products").ToList();

                foreach (var item in products) 
                {
                    Console.WriteLine(item.Name + "=>" + item.Price);
                }
            
            }

            Console.ReadLine();

        }






        private static void ders9(NorthwindContext db)
        {
            var customers = db.Customers.Where(cus => cus.CustomerId == "PERIC")
                .Select(cus => new CustomerModel
                {
                    CustomerId = cus.CustomerId,
                    CustomerName = cus.ContactName,
                    OrderCount = cus.Orders.Count(),
                    Orders = cus.Orders.Select(order => new OrderModel
                    {
                        OrderId = order.OrderId,
                        Total = order.OrderDetails.Sum(od => od.Quantity * od.UnitPrice),
                        Products = order.OrderDetails.Select(od => new ProductModel
                        {
                            ProductId = od.ProductId,
                            Name = od.Product.ProductName,
                            Price = od.Product.UnitPrice,
                            //Quantity = od.Quantity
                        }).ToList()
                    }).ToList()
                })
            .OrderBy(i => i.OrderCount)
            .ToList();

            foreach (var customer in customers)
            {
                Console.WriteLine(customer.CustomerId + " =>" + customer.CustomerName + "=> " + customer.OrderCount);
                Console.WriteLine("Siparişler");
                foreach (var order in customer.Orders)
                {
                    Console.WriteLine("***************");
                    Console.WriteLine(order.OrderId + "=>" + order.Total);
                    foreach (var product in order.Products)
                    {
                        Console.WriteLine(product.ProductId + "=>" + product.Name + "=>" + product.Price + "=>" );
                    }
                }

            }
        }

        private static void ders8(NorthwindContext db)
        {
            var products = (from p in db.Products
                            join s in db.Suppliers on p.SupplierId equals s.SupplierId
                            select new
                            {
                                p.ProductName,
                                contactName = s.ContactName,
                                companyName = s.CompanyName
                            })
                            .ToList();


            foreach (var item in products)
            {
                Console.WriteLine(item.companyName + " " + item.contactName + "=> " + item.ProductName);
            }
        }

        private static void ders7(NorthwindContext db)
        {
            var p1 = new Product() { ProductId = 85 };
            var p2 = new Product() { ProductId = 84 };

            var products = new List<Product>() { p1, p2 };


            //db.Entry(p).State = EntityState.Deleted;
            db.Products.RemoveRange(products);

            db.SaveChanges();
        }

        private static void ders6(NorthwindContext db)
        {
            var p = new Product() { ProductId = 1 };

            db.Products.Attach(p);

            p.UnitsInStock = 50;

            db.SaveChanges();
        }

        private static void ders5(NorthwindContext db)
        {
            // change tracking
            var product = db.Products
                //.AsNoTracking()
                .FirstOrDefault(p => p.ProductId == 1);

            if (product != null)
            {
                product.UnitsInStock += 10;

                db.SaveChanges();

                Console.WriteLine("veri güncellendi!");
            }
        }

        private static void ders4(NorthwindContext db)
        {
            var category = db.Categories.Where(i => i.CategoryName == "Beverages").FirstOrDefault();

            var p1 = new Product() { ProductName = "yeni ürün 11" };
            var p2 = new Product() { ProductName = "yeni ürün 12" };

            //var products = new List<Product>()
            //{
            //    p1, p2
            //};

            category.Products.Add(p1);
            category.Products.Add(p2);


            db.SaveChanges();

            Console.WriteLine("veri eklendi!");
            Console.WriteLine(p1.ProductId);
            Console.WriteLine(p2.ProductId);
        }

        private static void ders1(NorthwindContext db)
        {
            //var products = db.Products.ToList();
            //var products = db.Products.Select(p => new { p.ProductName, p.UnitPrice }).ToList();                         
            //var products = db.Products.Select(p => 
            //new ProductModel 
            //{
            //    Name = p.ProductName, 
            //    Price = p.UnitPrice 
            //}).ToList();

            //var product = db.Products.First();
            var product = db.Products.Select(p => new { p.ProductName, p.UnitPrice }).FirstOrDefault();

            Console.WriteLine(product.ProductName + ' ' + product.UnitPrice);

            //foreach (var p in products)
            //{
            //    Console.WriteLine(p.Name + ' ' + p.Price);
            //}
        }
    }
}


//var products = db.Products.Where(p => p.UnitPrice > 18 ).ToList();
//var products = db.Products.Select(p => new { p.ProductName, p.UnitPrice }).Where(p => p.UnitPrice > 18).ToList();
//var products = db.Products.Where(p => p.UnitPrice > 18 && p.UnitPrice < 30).ToList();
//var products = db.Products.Where(p => p.CategoryId >= 1 && p.CategoryId <= 5).ToList();
//var products = db.Products.Where(p => p.CategoryId == 1 || p.CategoryId == 5).ToList();           
//var products = db.Products.Where(i=>i.ProductName == "Chai").FirstOrDefault();
//var products = db.Products.Where(i => i.ProductName.Contains("Ch")).ToList();

//foreach (var p in products) 
//{
//    Console.WriteLine(p.ProductName + ' ' + p.UnitPrice);    
//}


//--------------------

//var customers = db.Customers.ToList();

//foreach (var customer in customers)
//{
//    Console.WriteLine(customer.ContactName);
//}

//var customers = db.Customers.Select(c => new {c.CustomerId , c.ContactName}).ToList();

//foreach (var customer in customers)
//{
//    Console.WriteLine(customer.CustomerId + " " + customer.ContactName );
//}

//var customers = db.Customers.Select(c => new { c.ContactName, c.Country })
//    .Where(c => c.Country == "Germany")
//    .ToList();

//foreach (var customer in customers)
//{
//    Console.WriteLine(customer.Country + ' ' + customer.ContactName);
//}

//var customer = db.Customers.Where(c => c.ContactName == "Diego Roel").FirstOrDefault();
//Console.WriteLine(customer.ContactName + " " + customer.CompanyName);

//var products = db.Products.Select(i => new { i.ProductName, i.UnitsInStock })
//    .Where(i=> i.UnitsInStock == 0)
//    .ToList();

//foreach (var p in products)
//{
//    Console.WriteLine(p.ProductName +" "+ p.UnitsInStock);
//}

//var employees = db.Employees
//    .Select(i => new {
//        FullName = i.FirstName + " "+ i.LastName,
//    }).ToList();

//foreach (var emp in employees)
//{
//    Console.WriteLine(emp.FullName);
//}


//var products = db.Products.Take(5).ToList();

//foreach (var p in products)
//{
//    Console.WriteLine(p.ProductName + " " + p.ProductId);
//}

//var products = db.Products.Skip(5).Take(5).ToList();

//foreach (var p in products)
//{
//    Console.WriteLine(p.ProductName + " " + p.ProductId);
//}


