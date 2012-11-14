using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using NhibernateTeste.Entity;

namespace NhibernateTeste.Config
{
	class HibernateConfig
	{
		private static ISessionFactory factory = null;

		public static ISessionFactory Factory
		{
			get
			{
				if (factory == null)
				{
					CreateFactory();
				}

				return factory;
			}
		}

		private static void CreateFactory()
		{
			var config = Fluently.Configure()
				.Database(MsSqlConfiguration.MsSql2008
				          	.ConnectionString("Server=PPP-HP;Database=nhibernate;Trusted_Connection=True;")
				          	.ShowSql())
				.Mappings(m => m.FluentMappings
				               	.Add<CustomerMap>()
				               	.Add<CustomerTypeMap>()
				               	.Add<OrdersMap>()
				               	.Add<ProductMap>()
								.Add<ServiceMap>()
								.Add<OrderDetailsMap>()
								// .Add<ServiceProductMap>()
								)
				.BuildConfiguration();

			var schema = new SchemaExport(config);
			schema.Drop(true, true);
			schema.Create(true, true);

			factory = config.BuildSessionFactory();
		}
	}
}
