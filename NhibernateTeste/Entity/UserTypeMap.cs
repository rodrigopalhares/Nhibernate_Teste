using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace NhibernateTeste.Entity
{
	public class UserTypeMap : ClassMap<UserType>
	{
		public UserTypeMap()
		{
			Id(c => c.Code).GeneratedBy.Identity().Not.Nullable().Length(5);
			Map(c => c.Name).Not.Nullable().Length(20);
			HasMany(c => c.Users).Inverse().NotFound.Ignore().LazyLoad().Cascade.All().AsSet();
		}
	}
}
