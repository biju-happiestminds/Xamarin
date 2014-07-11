using Android.App;
using Android.Views;
using Android.Widget;
using inStoreApp.Android.AL;
using inStoreApp.Core.Domain.Models;
using Android.Graphics;
using Android.Runtime;
using Android.OS;
using System.Net;
using System;
using inStoreApp.Android.UI.ProductAppHelpers;
using System.Collections.Generic;
using System.Linq;
using LegacyBar.Library.BarActions;
using LegacyBar.Library.BarBase;

namespace inStoreApp.Android.UI.Adapters
{
	public class ProductListAdapter : BaseAdapter<Product>
	{
		readonly Product[] _items;
		readonly Activity _context;

		public ProductListAdapter(Activity context, Product[] items)
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
			Product product;
			TextView name;
			TextView qty;
			TextView price;
			ImageView imageView;
			ImageView addToCart;
			//TextView addText;
			int qnty;
//			ImageView removeFromCart;

			public void Initialize(View view)
			{
				name = view.FindViewById<TextView> (Resource.Id.txtProdName);
				qty = view.FindViewById<TextView> (Resource.Id.txtProdQty);
				price = view.FindViewById<TextView> (Resource.Id.txtProdPrice);
				imageView = view.FindViewById<ImageView> (Resource.Id.imgvProduct);


				addToCart = view.FindViewById<ImageView>(Resource.Id.imgvProdadd);
				addToCart.Click += new EventHandler(this.add_Click);

				//addText = view.FindViewById<TextView> (Resource.Id.txtAddToCart);
				//addText.Click += new EventHandler(this.add_Click);

//				removeFromCart = view.FindViewById<ImageView> (Resource.Id.imgvProdminus);
//				removeFromCart.Click += new EventHandler(this.remove_Click);
			}

			public void Bind(Activity myContext, Product item)
			{
				context = myContext;
				product = item;
				name.Text = item.Name;
				qnty = 0;
				qty.Text = "Qty : "+qnty.ToString();
				price.Text = item.Price.ToString();

				var sdCardPath = "/mnt/sdcard/InStore_Images";
				var imageFilePath = System.IO.Path.Combine (sdCardPath, item.Picture);
				if (System.IO.File.Exists (imageFilePath)) {
					try {
						BitmapFactory.Options opts=new BitmapFactory.Options();
						opts.InDither=false;
						opts.InPurgeable=true;                   //Tell to gc that whether it needs free memory, the Bitmap can be cleared
						opts.InInputShareable=true;              //Which kind of reference will be used to recover the Bitmap data after being clear, when it will be used in the future
						opts.InTempStorage=new byte[32 * 1024]; 
						var imageFile = new Java.IO.File (imageFilePath);
						Bitmap bitmap = BitmapFactory.DecodeFile (imageFile.AbsolutePath, opts);
						imageView.SetImageBitmap (bitmap);
						bitmap.Dispose ();
					} catch (OutOfMemoryException ex) {
						//ex.StackTrace;
					}
				}
				else {
					imageView.SetImageResource (Resource.Drawable.Icon);
				}
			}

			void add_Click(Object sender, EventArgs e)
			{
				var t = (ImageView)sender;
				AddProductToCart(product);
				Toast.MakeText(context, "Added to cart", ToastLength.Short).Show();
			}

			private void AddProductToCart(Product product)
			{
				qnty = qnty + 1;
				qty.Text = "Qty : "+qnty.ToString();
				var cart = new Cart()
				{
					ProductId = product.Id,
					Quantity = qnty,
					UserId = LoginInfo.User.Id,
				};
				ServiceWrapper.ProductService.SaveProductToCart(cart);
			}

//			void remove_Click(Object sender, EventArgs e)
//			{
//				List<Cart> cartList = ServiceWrapper.ProductService.GetCartItemsForUser(LoginInfo.User).ToList();
//				if (cartList.Count > 0) {
//					var t = (ImageView)sender;
//					RemoveProductFromCart (product);
//					Toast.MakeText (context, "Remove from cart", ToastLength.Short).Show ();
//				} else {
//					Toast.MakeText (context, "Cart is empty", ToastLength.Short).Show ();
//				}
//			}
//
//			private void RemoveProductFromCart(Product product)
//			{
//				var cart = new Cart()
//				{
//					ProductId = product.Id,
//					Quantity = -1,
//					UserId = LoginInfo.User.Id,
//				};
//
//				ServiceWrapper.ProductService.SaveProductToCart(cart);
//			}
		}

		private Bitmap GetImageBitmapFromUrl(string url)
		{
			Bitmap imageBitmap = null;

			using (var webClient = new WebClient())
			{
				var imageBytes = webClient.DownloadData(url);		
				if (imageBytes != null && imageBytes.Length > 0)
				{
					imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
				}
			}

			return imageBitmap;
		}

		public override int Count
		{
			get { return _items.Length; }
		}

		#endregion

		#region Overrides of BaseAdapter<Product>

		public override Product this[int position]
		{
			get { return _items[position]; }
		}

		#endregion
	}

}