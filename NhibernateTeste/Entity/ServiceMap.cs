using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace NhibernateTeste.Entity
{
	public class ServiceMap : ClassMap<Service>
	{
		public ServiceMap()
		{
			Id(c => c.Code).GeneratedBy.Identity().Length(10);
			Map(c => c.Name).Not.Nullable().Length(50);
			HasManyToMany(c => c.Products).Table("ServiceProduct").Cascade.None().AsSet();
		}
	}
}
