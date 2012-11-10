using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace NhibernateTeste.Entity
{
	public class CustomerMap : ClassMap<Customer>
	{
		public CustomerMap()
		{
			Id(c => c.Code).Index("PK_CUSTOMER").GeneratedBy.UuidHex("D").Length(36);
			Map(c => c.Name).Not.Nullable().Length(50);
			//HasOne(c => c.CustomerType).ForeignKey("FK_USER_USERTYPE").Cascade.All();
			References(c => c.CustomerType).ForeignKey("FK_CUSTOMER_CUSTOMERTYPE").Not.Nullable().Cascade.None();
		}
	}
}
