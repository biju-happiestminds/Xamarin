using System;
using System.Collections.Generic;
using Training.shared.Core.Domain.Models;
using Training.shared.Core.Domain.Services.Interfaces;

namespace Training.shared.Core.Domain.Services
{
	public class ProductService : IProductService
	{
		List<Product> products = new List<Product> ();
		public ProductService ()
		{
		}
		public List<Product> getproduct()
		{
			Product product;

			for (int i = 0; i < 10; i++) {
				product = new Product ();
				product.Id = i;
				product.Name="Android Mobiles"+i.ToString();
				product.Description = "Google";
				product.Stock = 10;
				product.Price = 1000;
				products.Add (product);

			}

			for (int i = 10; i < 20; i++) {
				product = new Product ();
				product.Id = i;
				product.Name="IPhones"+i.ToString();
				product.Description = "Apple";
				product.Stock = 50;
				product.Price = 100;
				products.Add (product);

			}

			for (int i = 20; i < 30; i++) {
				product = new Product ();
				product.Id = i;
				product.Name="Windows Phones"+i.ToString();
				product.Description = "Windows";
				product.Stock = 500;
				product.Price = 100;
				products.Add (product);

			}
			//Console.WriteLine (products);
			return products;
		}

	}
}

