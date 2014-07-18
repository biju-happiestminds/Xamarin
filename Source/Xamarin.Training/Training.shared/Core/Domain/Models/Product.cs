using System;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;

namespace Training.shared.Core.Domain.Models

{
	    public class Product
	    {

		        public string status { get; set; }

		        public string status_message { get; set; }

		        public List<Productlist> data { get; set; }

		    }

	    public class Productlist
	    {        
		        public int ID { get; set; }

		        public string ProdName { get; set; }

		        public string ProdDesc { get; set; }

		        public string Image { get; set; }

		        public int Stock { get; set; }

		        public decimal Price { get; set; }
		    }

}

