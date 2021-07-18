package crc6425bf5147caf3c8be;


public class PokazViewHolder
	extends androidx.recyclerview.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("AdminBooking.PokazViewHolder, OCPIO", PokazViewHolder.class, __md_methods);
	}


	public PokazViewHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == PokazViewHolder.class)
			mono.android.TypeManager.Activate ("AdminBooking.PokazViewHolder, OCPIO", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
	}

	private java.util.ArrayList refList;
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
