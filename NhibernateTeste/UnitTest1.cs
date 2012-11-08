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
	public class UnitTest1
	{
		public UnitTest1()
		{
			LogConfig.Init();

			var session = HibernateConfig.Factory.OpenSession();
			var userType = new UserType {Name = "Comprador"};
			session.Save(new User {Name = "User11", UserType = userType});
			session.Save(new User {Name = "User12", UserType = userType});

			var userType2 = new UserType {Name = "Comprador2"};
			userType2.AddUser(new User {Name = "User21"});
			userType2.AddUser(new User {Name = "User22"});
			session.Save(userType2);

			session.Flush();
		}

		[Test]
		public void TestMethod1()
		{
			var session = HibernateConfig.Factory.OpenSession();
			var userType = session.QueryOver<UserType>()
				.Where(n => n.Name == "Comprador")
				.SingleOrDefault();

			Assert.NotNull(userType);
			Assert.AreEqual("Comprador", userType.Name);
			Assert.AreEqual(2, userType.Users.Count);

			userType = session.QueryOver<UserType>()
				.Where(n => n.Name == "Comprador2")
				.SingleOrDefault();

			Assert.NotNull(userType);
			Assert.AreEqual("Comprador2", userType.Name);
			Assert.AreEqual(2, userType.Users.Count);
		}
	}
}
