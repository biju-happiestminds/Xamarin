using System;
using Android.Widget;
using Android.Views;
using Android.App;

/**
 * @Author Nithin
 * @Date Jul 15, 2014
 */

namespace Xamarin.Training.Droid
{
	public class RegistryListAdapter : BaseAdapter
	{

		/**
	 	* Holder class to hold the widgets for each list item
	 	*/

		/*static class ViewHolder {
			ImageView ivRetailerLogo;
			TextView tvRetailerName;
		}*/

		readonly Activity _context;

		String[] productNames = {"Bat", "Ball", "Shoes"};
		
		public RegistryListAdapter (Activity context) : base() {
			_context = context;
		}


		public override int Count {
		get { return productNames.Length; }
		}

		public override Java.Lang.Object GetItem(int position) {
			return null;
		}

		public override long GetItemId(int position) {
			return position;
		}

		public override View GetView(int position, View convertView, ViewGroup parent) {
			View view = convertView;

			if (view == null) { // no view to re-use, create new
				view = _context.LayoutInflater.Inflate (Resource.Layout.RegistryListItemLayout, null);
			}

			view.FindViewById<TextView>(Resource.Id.productName).Text = productNames[position];
			var addToWish = view.FindViewById<Button> (Resource.Id.addToWishlist);
			addToWish.Tag = (int)position;
			addToWish.Click += addToWishList;
			
			return view;
		}

		void addToWishList(Object sender, EventArgs e) {
			var but = sender as Button;
			int t = (int)but.Tag;
			String selectedItem = productNames [t];
			Boolean found = false;
			foreach(String item in RegistryManagementActivity.sWishList) {
				if (item.Equals (selectedItem)) {
						found = true;
				}
			}
			
			if (found == false) {
				RegistryManagementActivity.sWishList.Add (selectedItem);
				Android.Widget.Toast.MakeText (_context, selectedItem + " is added to Wish list", Android.Widget.ToastLength.Short).Show ();	
			} else {
				Android.Widget.Toast.MakeText (_context, "This item is already added to Wish list", Android.Widget.ToastLength.Short).Show ();	
			}
		}
	}
}

