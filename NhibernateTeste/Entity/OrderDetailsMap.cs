using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace NhibernateTeste.Entity
{
	public class OrderDetailsMap : ClassMap<OrderDetails>
	{
		public OrderDetailsMap()
		{
			Id(c => c.Code).GeneratedBy.Identity().Length(10);
			Map(c => c.Discount).Not.Nullable().Length(10).Precision(2);
			Map(c => c.Quantity).Not.Nullable().Length(5).Precision(3);
			Map(c => c.TotalPrice).Not.Nullable().Length(10).Precision(2);
			Map(c => c.UnitPrice).Not.Nullable().Length(10).Precision(2);
			References(c => c.Product).ForeignKey("FK_OrderDetails_Product").Not.Nullable().Cascade.None();
			References(c => c.Orders).ForeignKey("FK_OrderDetails_Order").Not.Nullable().Cascade.None();
		}
	}
}
