using System;
using System.Collections.Generic;
using TrainingSolution.Shared.Core.Domain;
using TrainingSolution.Shared;
using TrainingSolution.Shared.Core.Domain.Sercices.Interfaces;



namespace TrainingSolution.Shared
{
	public class ProductService : IProductService
	{

		public List<Product> GetProduct()
		{
			productData data = new productData ();
			//List<Product> products = new List<Product> ();
			return data.getProducts ();
			//return _productRepository.GetItems().ToList();
		}

	}

	public class productData
	{
		public List<Product> getProducts()
		{
			List<Product> products = new List<Product> ();
			Product product;

			for (int i = 0; i < 10; i++) {
				product = new Product ();
				product.Id = i;
				product.Name="product"+i.ToString();
				product.Stock = 10;
				products.Add (product);

			}
			return products;
		}

	}
}

