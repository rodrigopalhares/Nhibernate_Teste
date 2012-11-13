using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace NhibernateTeste.Entity
{
	public class ProductMap : ClassMap<Product>
	{
		public ProductMap()
		{
			Id(c => c.Code).GeneratedBy.Identity().Length(10);
			Map(c => c.CreateTime).Not.Nullable();
			Map(c => c.Name).Not.Nullable().Length(50);
			Map(c => c.Price).Not.Nullable().Length(10).Precision(2).Default("0");
			HasManyToMany(c => c.Services).Table("ServiceProduct").Cascade.AllDeleteOrphan().AsSet();
		}
	}
}
