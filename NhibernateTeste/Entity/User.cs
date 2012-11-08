using FluentNHibernate.Mapping;

namespace NhibernateTeste.Entity
{
	public class User
	{
		public virtual string Code { get; set; }
		public virtual string Name { get; set; }
		public virtual UserType UserType { get; set; }
	}
}
