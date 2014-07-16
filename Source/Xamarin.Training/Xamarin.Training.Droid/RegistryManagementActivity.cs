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

/**
 * @Author Nithin
 * @Date Jul 15, 2014
 */

namespace Xamarin.Training.Droid
{
	[Activity (Label = "RegistryManagementActivity")]	
	public class RegistryManagementActivity  : Activity
	{
		public static List<String> sWishList = new List<String>();

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.RegistryManagement);

			// Create your application here

			ListView prodList = FindViewById<ListView>(Resource.Id.productList);
			//prodList.SetAdapter (new ProductListAdapter(this));
			prodList.Adapter = new RegistryListAdapter (this);

		}
	}
}

