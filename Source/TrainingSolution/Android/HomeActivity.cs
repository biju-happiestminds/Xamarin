
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

namespace TrainingSolution.Android
{
	[Activity (Label = "HomeActivity")]			
	public class HomeActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.home);

			// Create your application here
		}


		public  override void OnBackPressed ()
		{
			//base.OnBackPressed ();
			onBackLogout ();
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

