using System;
using System.Collections.Generic;
using Training.shared.Core.Domain.Models;
using Training.shared.Core.Domain.Services;
using Training.shared.Core.Domain.Services.Interfaces;

namespace Training.shared
{
	public class ServiceWrapper
	{
		public ServiceWrapper ()
		{
		}
		public static IProductService ProductService
		{
			get{
				var prodservc = new ProductService();
				return prodservc;
				}
		}
		public static IUserService UserService
		{
			get{
				var userservc = new UserService ();
				return userservc;
			}
		}

	}

}

