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
			string base_url= "http://10.20.26.149/Xamarin/get_products.php";
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

	public static class FileCache
	{

		public static string SaveLocation;
		public static async Task<string> Download(string url)
		{
			if (string.IsNullOrEmpty (SaveLocation))
				throw new Exception ("Save location is required");
			var fileName = md5 (url);

			return await Download (url, fileName);
		}

		static object locker = new object ();
		public static async Task<string> Download(string url, string fileName)
		{
			try{
				var path = Path.Combine (SaveLocation, fileName);
				if (File.Exists (path))
					return path;

				await GetDownload(url,path);

				return path;
			}
			catch(Exception ex) {
				Console.WriteLine (ex);
				return  "";
			}
		}

		static Dictionary<string,Task> downloadTasks = new Dictionary<string, Task> ();
		static Task GetDownload(string url, string fileName)
		{
			lock (locker) {
				Task task;
				if (downloadTasks.TryGetValue (fileName, out task))
					return task;
				var client = new WebClient ();
				downloadTasks.Add (fileName, task = client.DownloadFileTaskAsync (url, fileName));
				return task;

			}
		}
		static void removeTask(string fileName)
		{
			lock (locker) {
				downloadTasks.Remove (fileName);
			}
		}


		static MD5CryptoServiceProvider checksum = new MD5CryptoServiceProvider ();
		static int hex (int v)
		{
			if (v < 10)
				return '0' + v;
			return 'a' + v-10;
		}

		static string md5 (string input)
		{
			var bytes = checksum.ComputeHash (Encoding.UTF8.GetBytes (input));
			var ret = new char [32];
			for (int i = 0; i < 16; i++){
				ret [i*2] = (char)hex (bytes [i] >> 4);
				ret [i*2+1] = (char)hex (bytes [i] & 0xf);
			}
			return new string (ret);
		}
	}
}

