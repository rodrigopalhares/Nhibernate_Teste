using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NhibernateTeste.Entity
{
	public class Orders
	{
		public virtual int Code { get; set; }
		public virtual double TotalValue { get; set; }
		public virtual DateTime SaleDate { get; set; }
		public virtual Customer Customer { get; set; }
		public virtual ICollection<OrderDetails> OrderDetails { get; set; }

		public Orders()
		{
			this.OrderDetails = new List<OrderDetails>();
			SaleDate = DateTime.Now;
			TotalValue = 0;
		}

		public virtual void AddProduct(Product product, double quantity, double discount = 0)
		{
			var orderDetails = new OrderDetails {Orders = this};
			orderDetails.SetProduct(product, quantity, discount);
			TotalValue += orderDetails.TotalPrice;
			OrderDetails.Add(orderDetails);
		}

		public virtual bool RemoveProduct(Product product)
		{
			var orderDetails = OrderDetails.First(c => c.Product == product);
			if (orderDetails == null)
			{
				return false;
			}

			OrderDetails.Remove(orderDetails);
			TotalValue -= orderDetails.TotalPrice;

			return true;
		}

		// override object.Equals
		public override bool Equals(object obj)
		{
			var orders = obj as Orders;
			if (orders == null)
			{
				return false;
			}

			return Code.Equals(orders.Code);
			
		}

		// override object.GetHashCode
		public override int GetHashCode()
		{
			return Code.GetHashCode();
		}
	}
}
