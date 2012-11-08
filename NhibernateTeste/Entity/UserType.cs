using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NhibernateTeste.Entity
{
	public class UserType
	{
		public virtual int Code { get; set; }
		public virtual string Name { get; set; }
		public virtual ICollection<User> Users { get; set; }

		public UserType()
		{
			this.Users = new HashSet<User>();
		}

		public virtual void AddUser(User user)
		{
			user.UserType = this;
			this.Users.Add(user);
		}
	}
}
