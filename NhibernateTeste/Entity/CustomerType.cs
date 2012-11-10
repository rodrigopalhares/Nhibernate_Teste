using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NhibernateTeste.Entity
{
	public class CustomerType
	{
		public virtual int Code { get; set; }
		public virtual string Name { get; set; }
		public virtual ICollection<Customer> Users { get; set; }

		public CustomerType()
		{
			this.Users = new HashSet<Customer>();
		}

		public virtual void AddUser(Customer customer)
		{
			customer.CustomerType = this;
			this.Users.Add(customer);
		}

		// override object.Equals
		public override bool Equals(object obj)
		{
			var cstType = obj as CustomerType;
			if (cstType == null)
			{
				return false;
			}

			return Code.Equals(cstType.Code);
			
		}

		// override object.GetHashCode
		public override int GetHashCode()
		{
			return Code.GetHashCode();
		}
	}
}
