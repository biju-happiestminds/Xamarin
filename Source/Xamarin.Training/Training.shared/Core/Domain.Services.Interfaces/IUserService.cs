using System;
using System.Collections.Generic;
using Training.shared.Core.Domain.Models;

namespace Training.shared.Core.Domain.Services.Interfaces
{
	public interface IUserService
	{
		User ValidateUser(string userName, string pwd);
		void RegisterUser (User user);
	}
}


