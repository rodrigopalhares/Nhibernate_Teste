using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace NhibernateTeste.Entity
{
	public class ServiceProductMap : ClassMap<ServiceProduct>
	{
		public ServiceProductMap()
		{
			Id(c => c.Code).GeneratedBy.Identity();
			References(c => c.Product).ForeignKey("FK_SERVICEPRODUCT_PRODUCT").Not.Nullable().Cascade.None();
			References(c => c.Service).ForeignKey("FK_SERVICEPRODUCT_SERVICE").Not.Nullable().Cascade.None();
		}
	}
}
