using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace NhibernateTeste.Entity
{
	public class Service
	{
		public virtual int Code { get; set; }
		public virtual string Name { get; set; }
		public virtual ICollection<Product> Products { get; set; }

		public Service()
		{
			Products = new Collection<Product>();
		}
	}
}
