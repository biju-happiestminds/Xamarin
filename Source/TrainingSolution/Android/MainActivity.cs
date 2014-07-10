using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Xamarin.Forms.Platform.Android;


namespace TrainingSolution.Android
{
	[Activity (Label = "TRU Training", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);

			button.Click += delegate {

				//This is place to navigate one screen other screen by firing intent.//

				var homeActivity = new Intent(this, typeof(HomeActivity));
				StartActivity(homeActivity);

				Toast.MakeText(this, "Navigated to Home Screen", ToastLength.Short).Show(); 
			};
		}
			
	}
}

