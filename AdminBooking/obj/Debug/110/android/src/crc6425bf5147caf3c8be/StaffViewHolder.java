package crc6425bf5147caf3c8be;


public class StaffViewHolder
	extends androidx.recyclerview.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("AdminBooking.StaffViewHolder, OCPIO", StaffViewHolder.class, __md_methods);
	}


	public StaffViewHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == StaffViewHolder.class)
			mono.android.TypeManager.Activate ("AdminBooking.StaffViewHolder, OCPIO", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
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
