using System;
using System.Collections.Generic;

namespace Training.shared.Core.Domain.Models
{
	[Serializable]
	public class User 
	{
		public User ()
		{
		}

		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public Int64 Mobile { get; set; }
	}
}

