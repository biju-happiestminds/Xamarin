using System;
using System.Collections.Generic;
using TrainingSolution.Shared;

namespace TrainingSolution.Shared.Core.Domain.Sercices.Interfaces
{
	public interface IProductService
	{
		List<Product> GetProduct ();
//		List<Product> GetProductByName (sting productName);
//		List<Product> GetProductById (int productId);
	}
}

