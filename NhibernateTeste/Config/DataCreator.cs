using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Linq;
using NUnit.Framework;
using NhibernateTeste.Entity;

namespace NhibernateTeste.Config
{
	public class DataCreator
	{
		public static int[] Products { get; set; }

		public static string[] Customers { get; set; }

		public static int[] CustomerTypes { get; set; }

		public static int[] Services { get; set; }

		public static void TruncateDb()
		{
			var session = HibernateConfig.Factory.OpenSession();

			session.CreateQuery("delete ServiceProduct").ExecuteUpdate();
			session.CreateQuery("delete Customer").ExecuteUpdate();
			session.CreateQuery("delete CustomerType").ExecuteUpdate();
			session.CreateQuery("delete Product").ExecuteUpdate();
			session.CreateQuery("delete Orders").ExecuteUpdate();
			session.CreateQuery("delete Service").ExecuteUpdate();

			session.Flush();
			session.Close();
		}

		public static void RecreateDb()
		{
			TruncateDb();

			using (var session = HibernateConfig.Factory.OpenSession())
			{
				var type = new CustomerType { Name = "Good" };
				session.Save(type);
				session.Save(new Customer { Name = "Customer11", CustomerType = type });
				session.Save(new Customer { Name = "Customer12", CustomerType = type });

				var type2 = new CustomerType { Name = "Bad" };
				session.Save(type2);
				session.Save(new Customer { Name = "Customer21", CustomerType = type2 });
				session.Save(new Customer { Name = "Customer22", CustomerType = type2 });
				session.Save(new Customer { Name = "Customer23", CustomerType = type2 });

				session.Save(new Product
				             	{
				             		CreateTime = DateTime.Today.AddDays(-2),
				             		Name = "Product1",
				             		Price = 2.99
				             	});

				session.Save(new Product
				             	{
				             		CreateTime = DateTime.Today.AddDays(-1),
				             		Name = "Product2",
				             		Price = 3
				             	});

				session.Save(new Service
				             	{
				             		Name = "Service1"
				             	});

				session.Save(new Service
				             	{
				             		Name = "Service2"
				             	});

				session.Flush();
			}

			PopulateConsts();
			AssertCustomerType();
			AssertProduct();
		}

		public static void PopulateConsts()
		{
			using (var session = HibernateConfig.Factory.OpenSession())
			{
				CustomerTypes = session.QueryOver<CustomerType>().OrderBy(c => c.Name).Asc.List().Select(c => c.Code).ToArray();
				Customers = session.QueryOver<Customer>().OrderBy(c => c.Name).Asc.List().Select(c => c.Code).ToArray();
				Products = session.QueryOver<Product>().OrderBy(c => c.Name).Asc.List().Select(c => c.Code).ToArray();
				Services = session.QueryOver<Service>().OrderBy(c => c.Name).Asc.List().Select(c => c.Code).ToArray();
			}
		}

		public static void AssertCustomerType()
		{
			AssertCustomerType("Good", 2);
			AssertCustomerType("Bad", 3);
		}

		public static void AssertProduct()
		{
			AssertProduct("Product1", DateTime.Today.AddDays(-2), 2.99);
			AssertProduct("Product2", DateTime.Today.AddDays(-1), 3);
		}

		private static void AssertCustomerType(string name, int customerCount)
		{
			using(var session = HibernateConfig.Factory.OpenSession())
			{
				var customerType = session.QueryOver<CustomerType>()
					.Where(n => n.Name == name)
					.SingleOrDefault();

				Assert.NotNull(customerType, "CustomerType nao encontrado");
				Assert.AreEqual(name, customerType.Name, "Name diferente");
				Assert.AreEqual(customerCount, customerType.Users.Count, "Quantidade de customer diferente");
			}
		}

		private static void AssertProduct(string name, DateTime createTime, double price)
		{
			using(var session = HibernateConfig.Factory.OpenSession())
			{
				var prd = session.QueryOver<Product>()
					.Where(n => n.Name == name)
					.SingleOrDefault();

				Assert.NotNull(prd, "Produto nao encontrado");
				Assert.AreEqual(name, prd.Name, "Name diferente");
				Assert.AreEqual(createTime, prd.CreateTime, "CreateTime diferente");
				Assert.AreEqual(price, prd.Price, "Price diferente");
			}
		}
	}
}
