package crc6425bf5147caf3c8be;


public class AddStaffActivity
	extends androidx.appcompat.app.AppCompatActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("AdminBooking.AddStaffActivity, OCPIO", AddStaffActivity.class, __md_methods);
	}


	public AddStaffActivity ()
	{
		super ();
		if (getClass () == AddStaffActivity.class)
			mono.android.TypeManager.Activate ("AdminBooking.AddStaffActivity, OCPIO", "", this, new java.lang.Object[] {  });
	}


	public AddStaffActivity (int p0)
	{
		super (p0);
		if (getClass () == AddStaffActivity.class)
			mono.android.TypeManager.Activate ("AdminBooking.AddStaffActivity, OCPIO", "System.Int32, mscorlib", this, new java.lang.Object[] { p0 });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
