using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Training.shared;
using ZXing;
using ZXing.Mobile;


namespace Xamarin.Training.Droid
{
	[Activity (Label = "HomeActivity")]			
	public class HomeActivity : Activity 
	{	
		MobileBarcodeScanner scanner;
		Button flashButton;
		View zxingOverlay;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
		
			SetContentView (Resource.Layout.Home);

			scanner = new MobileBarcodeScanner(this);

			Button searchButton = FindViewById<Button> (Resource.Id.searchBtn);

			searchButton.Click += delegate {

				//This is place to navigate one screen other screen by firing intent.//

				var homeActivity = new Intent(this, typeof(ProductListActivity));
				StartActivity(homeActivity);

				//Toast.MakeText(this, "Navigated to Home Screen", ToastLength.Short).Show(); 
			};

			Button scanButton = FindViewById<Button> (Resource.Id.scanBtn);

			scanButton.Click += async delegate {

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

				HandleScanResult(result);
			};

			// For registry managment
			Button button = FindViewById<Button> (Resource.Id.btnRegMgmt);

			button.Click += delegate {

				//This is place to navigate one screen other screen by firing intent.//

				var homeActivity = new Intent(this, typeof(RegistryManagementActivity));
				StartActivity(homeActivity);

				//Toast.MakeText(this, "Navigated to Home Screen", ToastLength.Short).Show(); 
			};
		}


		void HandleScanResult (ZXing.Result result)
		{
			string msg = "";

			if (result != null && !string.IsNullOrEmpty (result.Text)) {
				//msg = "Found Barcode: " + result.Text;
				var scanProdActivity = new Intent(this, typeof(ProductListByScanActivity));
				scanProdActivity.PutExtra("_items", result.Text);
				StartActivity(scanProdActivity);
			}
			else
				msg = "Scanning Canceled!";

			//this.RunOnUiThread(() => Toast.MakeText(this, msg, ToastLength.Short).Show());
		}

		public  override void OnBackPressed ()
		{
			base.OnBackPressed ();
			//onBackLogout ();
		}

		void onBackLogout()

		{
			var builder = new AlertDialog.Builder(this);
			builder.SetMessage(Resource.String.dialog_title);
			builder.SetTitle("Exit");
			//builder.SetIcon(Resource.Drawable.logout);
			builder.SetPositiveButton(Resource.String.yes, (s, e) => 
				{ 
					var intent = new Intent(this, typeof(MainActivity));
					intent.SetFlags(ActivityFlags.ClearTop); 
					StartActivity(intent);
					this.Finish();
				});

			builder.SetNegativeButton(Resource.String.no, (s, e) => { }).Create();
			builder.Show();
		}
	}
}