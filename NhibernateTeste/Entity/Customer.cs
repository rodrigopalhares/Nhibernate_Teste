using System;
using FluentNHibernate.Mapping;

namespace NhibernateTeste.Entity
{
	public class Customer
	{
		public virtual string Code { get; set; }
		public virtual string Name { get; set; }
		public virtual CustomerType CustomerType { get; set; }

		// override object.Equals
		public override bool Equals(object obj)
		{
			var customer = obj as Customer;
			if (customer == null)
			{
				return false;
			}

			return Code.Equals(customer.Code);
			
		}

		// override object.GetHashCode
		public override int GetHashCode()
		{
			return Code.GetHashCode();
		}
	}
}
