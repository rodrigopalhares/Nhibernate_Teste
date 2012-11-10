using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace NhibernateTeste.Entity
{
	public class OrdersMap : ClassMap<Orders>
	{
		public OrdersMap()
		{
			Id(c => c.Code).GeneratedBy.Identity().Length(10);
			Map(c => c.SaleDate).Not.Nullable();
			Map(c => c.TotalValue).Not.Nullable().Length(10).Precision(2).Default("0");
			References(c => c.Customer).ForeignKey("FK_ORDER_CUSTOMER").Not.Nullable().Cascade.None();
			HasMany(c => c.OrderDetails).Cascade.AllDeleteOrphan().AsSet().Inverse();
		}
	}
}
