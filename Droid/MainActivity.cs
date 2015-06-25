using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;
using WooCommerce.Droid;


[assembly: ExportRenderer (typeof (Label), typeof (MyLabelRenderer))]
namespace WooCommerce.Droid
{
	[Activity (Label = "WooXam", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);

			OxyPlot.Xamarin.Forms.Platform.Android.Forms.Init();

			LoadApplication (new App ());
		}
	}

	public class MyLabelRenderer : LabelRenderer {
			protected override void OnElementChanged (ElementChangedEventArgs<Label> e) {
				base.OnElementChanged (e);
				var label = (TextView)Control; // for example
				Typeface font = Typeface.CreateFromAsset (Forms.Context.Assets, "RobotoLight.ttf");
				label.Typeface = font;
			}
	}

}

