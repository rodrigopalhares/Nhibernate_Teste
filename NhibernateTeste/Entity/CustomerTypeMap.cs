using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Mapping;

namespace NhibernateTeste.Entity
{
	public class CustomerTypeMap : ClassMap<CustomerType>
	{
		public CustomerTypeMap()
		{
			Id(c => c.Code).GeneratedBy.Identity().Not.Nullable().Length(5);
			Map(c => c.Name).Not.Nullable().Length(20);
			HasMany(c => c.Customers).NotFound.Ignore().LazyLoad().Cascade.None().AsSet().ReadOnly();
		}
	}
}
