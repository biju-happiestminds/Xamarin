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
using System.Xml;
using System.Xml.Linq;


namespace Training.shared.Core.Domain.Services
{
	public class ProductService : IProductService
	{
		//Product products = new Product();

		// get all productlist
		public Product getproductlist()
		{
			var products = new Product();
			string base_url= "http://10.20.26.149/Xamarin/productlistClient.php";//"http://10.20.26.206/Xamarin/get_products.php"; 
			var request =  CreateRequest (base_url);
			string response = ReadResponse (request);
			var xmlDocument = XDocument.Parse(response);

			// parsing xml data
			XElement generalElement = xmlDocument
				.Element("products");
			products.status = generalElement.Element("status").Value;
			products.status_message = generalElement.Element("status_message").Value;

			var DataList = (from e in generalElement.Descendants ("data")
			              select e );

			List<Productlist> prdList = new List<Productlist> ();
			Productlist productsTest;
			foreach (var value in DataList) {
				productsTest= new Productlist();
				productsTest.ID =Convert.ToInt16( value.Element ("ID").Value);
				productsTest.ProdName = value.Element ("ProdName").Value;
				productsTest.ProdDesc =  value.Element("ProdDesc").Value;
				productsTest.Image =  value.Element("Image").Value;
				productsTest.Stock = Convert.ToInt16( value.Element("Stock").Value);
				productsTest.Price = Convert.ToDecimal( value.Element("Price").Value);
				prdList.Add (productsTest);
			}
			products.data = prdList;
			return products;
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
			//request.ContentType = "application/xml";
			//request.Accept = "application/xml";
			return request;
		}


		// web response
		protected static string ReadResponse  (HttpWebRequest req) {
			using (WebResponse resp = req.GetResponse ()) {	
				using (Stream s = (resp).GetResponseStream ()) {
					using (var r = new StreamReader (s, Encoding.UTF8)) {	
						return r.ReadToEnd();
					}
				}
			}
		}

	}
}

