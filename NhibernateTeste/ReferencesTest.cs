using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Criterion;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using NhibernateTeste.Config;
using NhibernateTeste.Entity;
using log4net.Appender;
using log4net.Layout;

namespace NhibernateTeste
{
	/// <summary>
	/// Summary description for UnitTest1
	/// </summary>
	[TestFixture]
	public class ReferencesTest
	{

		[TestFixtureSetUp]
		public void FixtureSetup()
		{
			LogConfig.Init();
		}

		[SetUp]
		public void Setup()
		{
			LogConfig.Init();
			DataCreator.RecreateDb();
		}

		[Test]
		public void TestManyToMany()
		{
			using (var session = HibernateConfig.Factory.OpenSession())
			{
				var product = session.Get<Product>(DataCreator.Products[0]);
				var product2 = session.Get<Product>(DataCreator.Products[1]);
				var service = session.Get<Service>(DataCreator.Services[0]);
				var service2 = session.Get<Service>(DataCreator.Services[1]);
				product.Services.Add(service);
				product.Services.Add(service2);

				product2.Services.Add(service);
				session.Flush();
			}

			using (var session = HibernateConfig.Factory.OpenSession())
			{
				var product = session.Get<Product>(DataCreator.Products[0]);
				var product2 = session.Get<Product>(DataCreator.Products[1]);
				Assert.AreEqual(2, product.Services.Count);
				Assert.AreEqual(1, product2.Services.Count);

				var service = product.Services.ElementAt(0);
				product.Services.Remove(service);

				session.Flush();
			}

			using (var session = HibernateConfig.Factory.OpenSession())
			{
				var product = session.Get<Product>(DataCreator.Products[0]);
				Assert.AreEqual(1, product.Services.Count);
			}
		}

		[Test]
		public void TestOneToManyInverse()
		{
			int orderCode;
			using (var session = HibernateConfig.Factory.OpenSession())
			{
				var product = session.Get<Product>(DataCreator.Products[0]);
				var product2 = session.Get<Product>(DataCreator.Products[1]);
				var customer = session.Get<Customer>(DataCreator.Customers[0]);
				var order = new Orders
				            	{
				            		Customer = customer,
				            	};
				order.AddProduct(product, 1);
				order.AddProduct(product2, 2.5);
				session.Save(order);
				session.Flush();
				orderCode = order.Code;
			}

			using (var session = HibernateConfig.Factory.OpenSession())
			{
				var orders = session.Get<Orders>(orderCode);
				Assert.AreEqual(2, orders.OrderDetails.Count);

				var product2 = session.Get<Product>(DataCreator.Products[1]);
				orders.RemoveProduct(product2);
				session.Flush();
				Assert.AreEqual(1, orders.OrderDetails.Count);
			}

			using (var session = HibernateConfig.Factory.OpenSession())
			{
				var orders = session.Get<Orders>(orderCode);
				Assert.AreEqual(1, orders.OrderDetails.Count);
			}
		}
	}
}
