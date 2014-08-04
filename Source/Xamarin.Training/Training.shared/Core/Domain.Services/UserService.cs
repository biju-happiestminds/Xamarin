using System;
using System.Net;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using Training.shared.Core.Domain.Models;
using Training.shared.Core.Domain.Services.Interfaces;

namespace Training.shared.Core.Domain.Services
{
	public class UserService : IUserService
	{


		public User ValidateUser(string userName, string password)
		{
			var user = new User ();
			if ((userName == null || userName.Trim () == "") || (password == null || password.Trim () == ""))
				return null;
			else {
				var request = CreateRequest ("?username=" + userName, "&password=" + password);
				string response = ReadResponse (request);
				var xmlDocument = XDocument.Parse (response);

			// parsing xml data
				XElement generalElement = xmlDocument
						.Element ("login");
				int status =Convert.ToInt16 (generalElement.Element("status").Value);
				if (status == 200) {
					var DataList = (from e in generalElement.Descendants ("data")
					               select e);
						foreach (var value in DataList) {
							user.Id = Convert.ToInt16 (value.Element ("ID").Value);
							user.FirstName = value.Element ("FirstName").Value;
							user.LastName = value.Element ("LastName").Value;
							user.Email = value.Element ("Email").Value;
							user.UserName = value.Element ("UserName").Value;
							user.Mobile = Convert.ToInt64 (value.Element ("Mobile").Value);
						}

						return user;	
				}
					else {
					return null;
				}
			}

		}



		static HttpWebRequest CreateRequest(string username, string password)
		{
			var request = (HttpWebRequest)WebRequest.Create ("http://10.20.26.149/Xamarin/login.php"+ username + password);
			request.Method = "GET";
			request.ContentType = "application/xml";
			request.Accept = "application/xml";
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
		public void RegisterUser (User user)
		{

		}

	
	}
}

