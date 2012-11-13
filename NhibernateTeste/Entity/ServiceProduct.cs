using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NhibernateTeste.Entity
{
	public class ServiceProduct
	{
		public virtual int Code { get; set; }
		public virtual Product Product { get; set; }
		public virtual Service Service { get; set; }
	}
}
