using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NhibernateTeste.Entity
{
	public class OrderDetails
	{
		public virtual int Code { get; set; }
		public virtual Orders Orders { get; set; }
		public virtual Product Product { get; set; }
		public virtual double UnitPrice { get; set; }
		public virtual double Quantity { get; set; }
		public virtual double Discount { get; set; }
		public virtual double TotalPrice { get; set; }

		public virtual void SetProduct(Product product, double quantity, double discount = 0)
		{
			Product = product;
			Quantity = quantity;
			Discount = discount;
			TotalPrice = product.Price*quantity - discount;
		}

		// override object.Equals
		public override bool Equals(object obj)
		{
			var orderDetails = obj as OrderDetails;
			if (orderDetails == null)
			{
				return false;
			}

			return Orders.Equals(orderDetails.Orders) && Product.Equals(orderDetails.Product);
		}

		// override object.GetHashCode
		public override int GetHashCode()
		{
			return Orders.GetHashCode() | Product.GetHashCode();
		}
	}
}
