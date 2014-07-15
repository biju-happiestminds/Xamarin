package xamarin.training.droid.adapters;


public class ProductListAdapter_ViewHolder
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Xamarin.Training.Droid.Adapters.ProductListAdapter/ViewHolder, Xamarin.Training.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ProductListAdapter_ViewHolder.class, __md_methods);
	}


	public ProductListAdapter_ViewHolder () throws java.lang.Throwable
	{
		super ();
		if (getClass () == ProductListAdapter_ViewHolder.class)
			mono.android.TypeManager.Activate ("Xamarin.Training.Droid.Adapters.ProductListAdapter/ViewHolder, Xamarin.Training.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
