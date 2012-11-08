using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace NhibernateTeste.Entity
{
	public class UserMap : ClassMap<User>
	{
		public UserMap()
		{
			Id(c => c.Code).GeneratedBy.UuidHex("D").Length(36);
			Map(c => c.Name).Not.Nullable().Length(50);
			//HasOne(c => c.UserType).ForeignKey("FK_USER_USERTYPE").Cascade.All();
			References(c => c.UserType).ForeignKey("FK_USER_USERTYPE").Not.Nullable().Cascade.All();
		}
	}
}
