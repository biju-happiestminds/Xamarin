using Android.App;
using Android.Views;
using Android.Widget;
using Training.shared;
using Training.shared.Core.Domain.Models;
using Android.Graphics;
using Android.Runtime;
using Android.OS;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Training.Droid.Helpers;
using System.Net.Http;

namespace Xamarin.Training.Droid.Adapters
{
	public class ProductListAdapter : BaseAdapter<Productlist>
	{
		readonly List<Productlist> _items;
		readonly Activity _context;

		public ProductListAdapter(Activity context, List<Productlist> items)
			: base()
		{
			_context = context;
			_items = items;
		}

		#region Overrides of BaseAdapter

		public override long GetItemId(int position)
		{
			return position;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			ViewHolder vh;
			var view = convertView;

			if (view == null)
			{
				// no view to re-use, create new
				view = _context.LayoutInflater.Inflate(Resource.Layout.ProductListItem, parent, false);
				vh = new ViewHolder();
				vh.Initialize(view);
				view.Tag = vh;
			}
			var item = _items[position];

			vh = (ViewHolder)view.Tag;
			vh.Bind(_context,item);

			return view;
		}

		private class ViewHolder : Java.Lang.Object
		{
			Activity context;
			Productlist product;
			TextView name;
			TextView qty;
			TextView price;
			ImageView imageView;
		
			//WebClient webClient;

			public void Initialize(View view)
			{
				name = view.FindViewById<TextView> (Resource.Id.txtProdName);
				qty = view.FindViewById<TextView> (Resource.Id.txtProdQty);
				price = view.FindViewById<TextView> (Resource.Id.txtProdPrice);
				imageView = view.FindViewById<ImageView> (Resource.Id.imgvProduct);
			}

			public async void Bind(Activity myContext, Productlist item)
			{

				context = myContext;
				product = item;
				name.Text = item.ProdName;
				qty.Text = item.Stock.ToString();
				price.Text = item.Price.ToString();

				 Task<int> sizeTask = DownloadHomepageAsync(item.Image,imageView);

				//imageView.SetImageResource (Resource.Drawable.Icon);
				// LoadProductImage (imageView, ImageURL);
			}


			public async Task<int> DownloadHomepageAsync(string ImageURL,ImageView imageViewdownload)
			{
				var httpClient = new HttpClient(); // Xamarin supports HttpClient!
				byte[] imageBytes  = await httpClient.GetByteArrayAsync(ImageURL); // async method!

				string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
				string localFilename = "team.jpg";
				string localPath = System.IO.Path.Combine (documentsPath, localFilename);
				File.WriteAllBytes (localPath, imageBytes); // writes to local storage   

				var localImage = new Java.IO.File (localPath);
				if (localImage.Exists ()) {
					var teamBitmap = BitmapFactory.DecodeFile (localImage.AbsolutePath);
					imageViewdownload.SetImageBitmap (teamBitmap);
					teamBitmap.Dispose ();
				}
					
				return 0; 
			}
				
		}

		public override int Count
		{
			get { return _items.Count; }
		}

		#endregion

		#region Overrides of BaseAdapter<Productlist>

		public override Productlist this[int position]
		{
			get { return _items[position]; }
		}

		#endregion
	}

}