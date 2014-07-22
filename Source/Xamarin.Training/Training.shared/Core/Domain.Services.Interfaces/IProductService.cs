using System;
using System.Collections.Generic;
using Training.shared.Core.Domain.Models;

namespace Training.shared.Core.Domain.Services.Interfaces
{
	public interface IProductService
	{
		Product getproductlist ();
        List<Productlist> GetProductslistById(int prodId);
	}

}

