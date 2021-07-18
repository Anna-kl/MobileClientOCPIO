package crc6425bf5147caf3c8be;


public class MainStaffActivity
	extends androidx.appcompat.app.AppCompatActivity
	implements
		mono.android.IGCUserPeer,
		com.google.android.material.navigation.NavigationView.OnNavigationItemSelectedListener,
		com.google.android.material.bottomnavigation.BottomNavigationView.OnNavigationItemSelectedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onNavigationItemSelected:(Landroid/view/MenuItem;)Z:GetOnNavigationItemSelected_Landroid_view_MenuItem_Handler:Google.Android.Material.Navigation.NavigationView/IOnNavigationItemSelectedListenerInvoker, Xamarin.Google.Android.Material\n" +
			"";
		mono.android.Runtime.register ("AdminBooking.MainStaffActivity, OCPIO", MainStaffActivity.class, __md_methods);
	}


	public MainStaffActivity ()
	{
		super ();
		if (getClass () == MainStaffActivity.class)
			mono.android.TypeManager.Activate ("AdminBooking.MainStaffActivity, OCPIO", "", this, new java.lang.Object[] {  });
	}


	public MainStaffActivity (int p0)
	{
		super (p0);
		if (getClass () == MainStaffActivity.class)
			mono.android.TypeManager.Activate ("AdminBooking.MainStaffActivity, OCPIO", "System.Int32, mscorlib", this, new java.lang.Object[] { p0 });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public boolean onNavigationItemSelected (android.view.MenuItem p0)
	{
		return n_onNavigationItemSelected (p0);
	}

	private native boolean n_onNavigationItemSelected (android.view.MenuItem p0);

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
