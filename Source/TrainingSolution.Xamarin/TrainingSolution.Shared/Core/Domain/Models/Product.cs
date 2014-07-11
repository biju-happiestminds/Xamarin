using System;
using System.Collections.Generic;

namespace TrainingSolution.Shared
{
	//TrainingSolution.Shared.Core.Domain.Models
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
}

