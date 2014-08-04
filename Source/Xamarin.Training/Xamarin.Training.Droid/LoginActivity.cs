
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
using Training.shared;

namespace Xamarin.Training.Droid
{
	[Activity (Label = "LoginActivity")]			
	public class LoginActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Login);

			Button login = FindViewById<Button> (Resource.Id.textLogin);

			login.Click += delegate {

				var userName = FindViewById<EditText>(Resource.Id.textUsername).Text;
				var password = FindViewById<EditText>(Resource.Id.textPassword).Text;

			

				var userdetail = ServiceWrapper.UserService.ValidateUser(userName,password);
				if (userName == "")
					Toast.MakeText(this, "Please Enter UserName", ToastLength.Short).Show();

				else if (password == "")
					Toast.MakeText(this, "Please Enter Password", ToastLength.Short).Show();

				else if (userdetail ==null)
					Toast.MakeText(this, "Invalid credentials", ToastLength.Short).Show();
				else{
				Toast.MakeText(this,"Welcome " + userdetail.UserName, ToastLength.Short).Show();
				var homeActivity = new Intent(this, typeof(HomeActivity));
					StartActivity(homeActivity);}


				//Toast.MakeText(this, "Navigated to Home Screen", ToastLength.Short).Show(); 
			};


			var register = FindViewById<TextView> (Resource.Id.register);

			register.Click += delegate {
				var RegistrationActivity = new Intent(this, typeof(RegistrationActivity));
				StartActivity(RegistrationActivity);

				//Toast.MakeText(this, "Navigated to Home Screen", ToastLength.Short).Show(); 
			};



			// Create your application here
		}
	}
}

