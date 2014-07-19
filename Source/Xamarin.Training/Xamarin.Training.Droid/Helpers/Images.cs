using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Content.Res;
using Android.Widget;
using System.Security.Cryptography;
using System.IO;
using System.Net;
using Training.shared.Core.Domain.Services;
using Training.shared;

namespace Xamarin.Training.Droid.Helpers
{
	public interface IBitmapHolder
	{
		void SetImageBitmap (Bitmap bmp);
	}

	public static class Images
	{
//		public static float ScreenWidth = 320;
//		static Dictionary<string, Bitmap> bmpCache = new Dictionary<string, Bitmap> ();
//
//		public static async Task SetImageFromUrlAsync (this ImageView imageView, string url)
//		{
//			var bmp = FromUrl(url);
//			if (bmp.IsCompleted)
//				imageView.SetImageBitmap (bmp.Result);
//			else
//				imageView.SetImageBitmap (await bmp);
//		}
//
//		public static async Task SetImageFromUrlAsync (IBitmapHolder imageView, string url)
//		{
//			var bmp = FromUrl(url);
//			if (bmp.IsCompleted)
//				imageView.SetImageBitmap (bmp.Result);
//			else
//				imageView.SetImageBitmap (await bmp);
//		}
//
//		public static async Task<Bitmap> FromUrl (string url)
//		{
//
//
//			string url1 = url;
//			Bitmap bmp;
//			if (bmpCache.TryGetValue (url, out bmp))
//				return bmp;
//			var path = await FileCache.Download (url1); //"";// await FileCache.Download(url);
//			if (string.IsNullOrEmpty (path))
//				return null;
//			bmp = await BitmapFactory.DecodeFileAsync (path);
//			bmpCache [url] = bmp;
//			return bmp;
//		}
	}

}