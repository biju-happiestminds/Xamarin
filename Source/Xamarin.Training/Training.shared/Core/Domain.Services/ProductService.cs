using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Text;
using Training.shared.Core.Domain.Models;
using Training.shared.Core.Domain.Services.Interfaces;
using Newtonsoft.Json.Converters;

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

		public List<Product> GetProductsById(int prodId)
		{
			var prods = getproduct();
			return prods.Any() ? prods.Where(item => item.Id.Equals(prodId)).ToList() : null;
		}


		List<Productlist> productslist = new List<Productlist>();
		public List<Productlist> getproductlist()
		{
			string url = "http://10.20.26.149/Xamarin/get_products.php";
			var jsonreq =  jsonCreateRequest (url);
			string jsonresponse = ReadResponsejson (jsonreq);
			productslist = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Productlist>>(jsonresponse);
			Console.WriteLine(productslist);
			return productslist;
		}
		// json webrequest
		static HttpWebRequest jsonCreateRequest(string url)
		{
			var request = (HttpWebRequest)WebRequest.Create (url);
			request.Method = "GET";
			request.ContentType = "application/json";
			request.Accept = "application/json";
			return request;
		}
		// json response
		protected static string ReadResponsejson  (HttpWebRequest req) {
			using (WebResponse resp = req.GetResponse ()) {
				using (Stream s = (resp).GetResponseStream ()) {
					using (var r = new StreamReader (s, Encoding.UTF8)) {
						return r.ReadToEnd ();
					}
				}
			}
		}

		public List<Productlist> GetProductslistById(int prodId)
		{
			var prods = getproductlist();
			return prods.Any() ? prods.Where(item => item.ID.Equals(prodId)).ToList() : null;
		} 


	}
}

