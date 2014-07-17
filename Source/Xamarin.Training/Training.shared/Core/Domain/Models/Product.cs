using System;

namespace Training.shared.Core.Domain.Models

{
	public class Product
	{
		public Product ()
		{
		}
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Picture { get; set; }
		public int Stock { get; set; }
		public decimal Price { get; set; }
	}

	public class Productlist
	    {
		        public Productlist ()
		        {
			        }
		        public int ID { get; set; }
		        public string ProdName { get; set; }
		        public string ProdDesc { get; set; }
		        public string Image { get; set; }
		        public int Stock { get; set; }
		        public decimal Price { get; set; }
		    }
}

