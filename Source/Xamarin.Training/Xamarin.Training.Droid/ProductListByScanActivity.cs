using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ZXing;
using ZXing.Mobile;
using Xamarin.Training.Droid.Adapters;
using Training.shared.Core.Domain.Models;
using Training.shared;

namespace Xamarin.Training.Droid
{
	[Activity (Label = "ProductListByScanActivity")]			
	public class ProductListByScanActivity : Activity
	{
		private ListView _listView;
		MobileBarcodeScanner scanner;
		Button flashButton;
		View zxingOverlay;

		//private Product[] _items;
		//private Product[] scannedItem;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView(Resource.Layout.ProductListScan);

			scanner = new MobileBarcodeScanner(this);
			_listView = FindViewById<ListView>(Resource.Id.lvProdList); // get reference to the ListView in the layout
			string msg1 = "";
			var scanProductId = Intent.GetStringExtra("_items");
			string msg = "Found Barcode: " + scanProductId;

			//this.RunOnUiThread(() => Toast.MakeText(this, msg, ToastLength.Short).Show());
			List<Productlist> products = new List<Productlist> ();
			products = ServiceWrapper.ProductService.GetProductslistById (Convert.ToInt32 (scanProductId)).ToList();
			_listView.Adapter = new ProductListAdapter(this, products);


			var button1 = FindViewById<Button> (Resource.Id.button1);
			button1.Click += async delegate 
			{
				//Tell our scanner we want to use a custom overlay instead of the default
				scanner.UseCustomOverlay = true;

				//Inflate our custom overlay from a resource layout
				zxingOverlay = LayoutInflater.FromContext(this).Inflate(Resource.Layout.ZxingOverlay, null);

				//Find the button from our resource layout and wire up the click event
				flashButton = zxingOverlay.FindViewById<Button>(Resource.Id.buttonZxingFlash);
				flashButton.Click += (s, e) => scanner.ToggleTorch();

				//Set our custom overlay
				scanner.CustomOverlay = zxingOverlay;

				//Start scanning!
				var result = await scanner.Scan();
				//var result = await scanner.Scan();
				msg1 = result.Text;
				List<Productlist> scanproducts = new List<Productlist> ();
				scanproducts = ServiceWrapper.ProductService.GetProductslistById (Convert.ToInt32 (msg1)).ToList();
				products = products.Concat(scanproducts).ToList();
				_listView.Adapter = new ProductListAdapter(this, products);
			};
		}
	}
}

