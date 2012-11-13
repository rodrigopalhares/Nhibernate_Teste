using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace NhibernateTeste.Entity
{
	public class Product
	{
		public virtual int Code { get; set; }
		public virtual string Name { get; set; }
		public virtual double Price { get; set; }
		public virtual DateTime CreateTime { get; set; }
		public virtual ICollection<Service> Services { get; set; }

		public Product()
		{
			Services = new Collection<Service>();
		}

		// override object.Equals
		public override bool Equals(object obj)
		{
			var product = obj as Product;
			if (product == null)
			{
				return false;
			}

			return Code.Equals(product.Code);
			
		}

		// override object.GetHashCode
		public override int GetHashCode()
		{
			return Code.GetHashCode();
		}
	}
}
