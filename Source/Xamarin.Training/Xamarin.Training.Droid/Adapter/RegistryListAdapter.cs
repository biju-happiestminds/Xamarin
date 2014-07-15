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

		public RegistryListAdapter (Activity context) : base() {

		}


		public override int Count {
			get { return 0; }
		}

		public override Java.Lang.Object GetItem(int position) {
			return null;
		}

		public override long GetItemId(int position) {
			return position;
		}

		public override View GetView(int position, View convertView, ViewGroup parent) {
			return null;
		}
	}
}

