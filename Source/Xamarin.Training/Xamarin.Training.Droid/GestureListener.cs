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

namespace Xamarin.Training.Droid
{
	public class GestureListener : Java.Lang.Object, GestureDetector.IOnGestureListener
	{
		public delegate void SwipeLeftEventHandler (MotionEvent first, MotionEvent second);
		public event SwipeLeftEventHandler SwipeLeftEvent;

		public delegate void SwipeRightEventHandler (MotionEvent first, MotionEvent second);
		public event SwipeRightEventHandler SwipeRightEvent;
		//public event Action LeftEvent;
		//public event Action RightEvent;
		public event Action longpress;
		private static int SWIPE_MAX_OFF_PATH = 250;
		private static int SWIPE_MIN_DISTANCE = 120;
		private static int SWIPE_THRESHOLD_VELOCITY = 200;
		public GestureListener ()
		{
		}

		public bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
		{
			try
			{
				if (Math.Abs(e1.GetY() - e2.GetY()) > SWIPE_MAX_OFF_PATH)
					return false;
				// right to left swipe
				if (e1.GetX() - e2.GetX() > SWIPE_MIN_DISTANCE && Math.Abs(velocityX) > SWIPE_THRESHOLD_VELOCITY && SwipeLeftEvent != null)
				{
					SwipeLeftEvent(e1,e2);

				}
				else if (e2.GetX() - e1.GetX() > SWIPE_MIN_DISTANCE && Math.Abs(velocityX) > SWIPE_THRESHOLD_VELOCITY && SwipeRightEvent != null)
				{
					SwipeRightEvent(e1,e2);
				
				}
			}
			catch (Exception e)
			{
				Console.WriteLine (e);

			}
			return false;
		}
		public bool OnDown(MotionEvent e) { return true; }
		public void OnLongPress(MotionEvent e) {
			longpress ();
		}
		public bool OnScroll(MotionEvent e1, MotionEvent e2, float distanceX, float distanceY) { return true; }
		public void OnShowPress(MotionEvent e) {}
		public bool OnSingleTapUp(MotionEvent e) { return true; }	


	}
}

