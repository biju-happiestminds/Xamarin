using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Text;
using Training.shared.Core.Domain.Models;
using Training.shared.Core.Domain.Services.Interfaces;
using Newtonsoft.Json.Converters;
using System.Security.Cryptography;
using System.Threading.Tasks;


namespace Training.shared.Core.Domain.Services
{
	public class ProductService : IProductService
	{
		Product productslist = new Product();

		// get all productlist
		public Product getproductlist()
		{
			string base_url= "http://10.20.26.149/Xamarin/get_products.php";//"http://10.20.26.206/Xamarin/get_products.php"; 
			var jsonreq =  CreateRequest (base_url);
			string jsonresponse = ReadResponse (jsonreq);
			productslist = Newtonsoft.Json.JsonConvert.DeserializeObject<Product>(jsonresponse);
			return productslist;

		}

		// get the particular product using product ID
		public List<Productlist> GetProductslistById(int prodId)
		{
			var prods = getproductlist ();
			return prods.data.Any() ? prods.data.Where(item => item.ID.Equals(prodId)).ToList() : null;
		} 

		// create webrequest
		static HttpWebRequest CreateRequest(string url)
		{
			var request = (HttpWebRequest)WebRequest.Create (url);
			request.Method = "GET";
			request.ContentType = "application/json";
			request.Accept = "application/json";
			return request;
		}


		// json response
		protected static string ReadResponse  (HttpWebRequest req) {
			using (WebResponse resp = req.GetResponse ()) {
				using (Stream s = (resp).GetResponseStream ()) {
					using (var r = new StreamReader (s, Encoding.UTF8)) {
						return r.ReadToEnd ();
					}
				}
			}
		}

	}
}

