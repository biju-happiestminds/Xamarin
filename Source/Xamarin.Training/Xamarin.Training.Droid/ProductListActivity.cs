﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Training.shared;
using Xamarin.Training.Droid.Adapters;
using Training.shared.Core.Domain.Models;

namespace Xamarin.Training.Droid
{
	[Activity (Label = "ProductListActivity")]			
	public class ProductListActivity : Activity
	{
		//private ListView _listView;
		//private Product[] _items;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView(Resource.Layout.ProductList);

			//ProductService productservice = new ProductService();
			//products=productservice.GetProduct ();
			List<Product> products = new List<Product> ();

			products = ServiceWrapper.ProductService.getproduct ();
			var listView = FindViewById<ListView> (Resource.Id.lvProdList);
			listView.Adapter = new ProductListAdapter (this, products);

			var searchText = FindViewById<EditText>(Resource.Id.txtProdSearch);

			searchText.AfterTextChanged += (sender, e) =>
			{
				if (searchText.Text != "")
				{
					var matchingItems = products.Where(item => item.Name.ToLower().Contains(searchText.Text.ToLower())).ToList();
					listView.Adapter = new ProductListAdapter(this, matchingItems);
				}
				else
				{
					listView.Adapter = new ProductListAdapter(this, products);
				}
			};

		}
	}
}

