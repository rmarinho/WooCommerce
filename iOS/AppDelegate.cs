using Foundation;
using UIKit;
using ImageCircle.Forms.Plugin.iOS;


namespace WooCommerce.iOS
{

	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			Preety ();

			OxyPlot.Xamarin.Forms.Platform.iOS.Forms.Init();

			ImageCircleRenderer.Init();
		
			global::Xamarin.Forms.Forms.Init ();

			LoadApplication (new App ());

			return base.FinishedLaunching (app, options);
		}

		static void Preety ()
		{
			UINavigationBar.Appearance.SetTitleTextAttributes (new UITextAttributes () {
				Font = UIFont.FromName ("HelveticaNeue-Light", 20),
			});
		}
	}
}

