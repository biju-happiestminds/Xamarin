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
		public ListView _listView;
		MobileBarcodeScanner scanner;
		Button flashButton;
		View zxingOverlay;
		List<Productlist> products = new List<Productlist> ();

		// swipe
		GestureDetector _gestureDetector;
		GestureListener _gestureListener;


		public ProductListAdapter adapter;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView(Resource.Layout.ProductListScan);

			// swipe
			_gestureListener = new GestureListener ();

			_gestureListener.SwipeLeftEvent += GestureLeft;
			_gestureListener.SwipeRightEvent += GestureRight;

			_gestureListener.longpress += OnLongPress;
			_gestureDetector = new GestureDetector (this, _gestureListener);

			scanner = new MobileBarcodeScanner(this);
			_listView = FindViewById<ListView>(Resource.Id.lvProdList); // get reference to the ListView in the layout

			var scanProductId = Intent.GetStringExtra("_items");
			string msg = "Found Barcode: " + scanProductId;

			products= ServiceWrapper.ProductService.GetProductslistById (Convert.ToInt32 (scanProductId)).ToList();
			adapter = new ProductListAdapter(this, products);
			_listView.Adapter = adapter;
			ScanProduct ();   // scan the product

		}

		// swipe left
		void GestureLeft(MotionEvent first, MotionEvent second)
		{
			int position = _listView.PointToPosition ((int)second.GetX (), (int)second.GetY ());
			//Toast.MakeText (this, "You swipe left on the  "+ position + " PArhti " , ToastLength.Short).Show ();
		}

		// swipe right
		void GestureRight(MotionEvent first, MotionEvent second)
		{
			int position = _listView.PointToPosition ((int)second.GetX (), (int)second.GetY ());
			removeProduct (position,products);
		}

		// long press
		void OnLongPress()
		{
			Toast.MakeText (this, "OnLongPress", ToastLength.Short).Show ();
		}
		public override bool DispatchTouchEvent (MotionEvent ev)
		{
			_gestureDetector.OnTouchEvent (ev);
			return base.DispatchTouchEvent (ev);
		}


		public void removeProduct(int position,List<Productlist> products)
		{    
			SetContentView(Resource.Layout.ProductListScan);
			if (position == -1) {
				//Toast.MakeText (this, "list is empty", ToastLength.Short).Show ();
				_listView = FindViewById<ListView>(Resource.Id.lvProdList);
				adapter = new ProductListAdapter (this, products);
				_listView.Adapter = adapter;
				ScanProduct ();
			}
			else {
				string Name = products [position].ProdName;
				products.RemoveAt (position);
				Toast.MakeText (this, Name + " is deleted", ToastLength.Short).Show ();
				_listView = FindViewById<ListView>(Resource.Id.lvProdList);
				adapter = new ProductListAdapter (this, products);
				_listView.Adapter = adapter;
				ScanProduct ();
			}
			Console.WriteLine(products.Count ());
		}

		public void ScanProduct()
		{
			string msg1 = "";
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
				var scanproducts = ServiceWrapper.ProductService.GetProductslistById (Convert.ToInt32 (msg1)).ToList();
				products = products.Concat(scanproducts).ToList();
				adapter = new ProductListAdapter(this, products);
				_listView.Adapter = adapter;
			};
		}

	}
}

