using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using System.Drawing;
using CoreGraphics;
using WooCommerce.iOS;
using Xamarin.Forms;
using ImageCircle.Forms.Plugin.iOS;

[assembly: ExportRenderer(typeof(ListView), typeof(ExtendedListViewRenderer))]
namespace WooCommerce.iOS
{

	public class ExtendedListViewRenderer : ListViewRenderer 
	{
		protected override void OnElementChanged (ElementChangedEventArgs<ListView> e)
		{
			base.OnElementChanged (e);
			if (e.OldElement == null) {
				Control.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			}

		}
	}

	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			Preety ();

			ImageCircleRenderer.Init();

			global::Xamarin.Forms.Forms.Init ();

			LoadApplication (new App ());

			return base.FinishedLaunching (app, options);
		}

		void Preety ()
		{
			UIBarButtonItem.Appearance.TintColor = App.NavBarButtonTint.ToUIColor ();
			UIBarButtonItem.Appearance.SetTitleTextAttributes (new UITextAttributes () {
				TextColor = App.NavBarButtonTint.ToUIColor ()
			}, UIControlState.Normal);
			UIBarButtonItem.Appearance.SetTitleTextAttributes (new UITextAttributes () {
				TextColor = App.NavBarTextTint.ToUIColor ()
			}, UIControlState.Application);
			UINavigationBar.Appearance.TintColor = App.NavBarButtonTint.ToUIColor ();
			UINavigationBar.Appearance.SetTitleTextAttributes (new UITextAttributes () {
				Font = UIFont.FromName ("HelveticaNeue-Light", 20),
				TextColor = App.NavBarTextTint.ToUIColor ()
			});
			UINavigationBar.Appearance.SetBackgroundImage (GetColoredImage (), UIBarMetrics.Default);
		}

		private UIImage GetColoredImage()
		{

			var size = new SizeF (320, 150);

			UIImage image;

			UIGraphics.BeginImageContext (size);
			using (CGContext context = UIGraphics.GetCurrentContext ()) {

				var rect = new RectangleF (0, 0, size.Width, size.Height);

				context.SetFillColor (App.NavBarTint.ToCGColor());
				context.FillRect (rect);

				image = UIGraphics.GetImageFromCurrentImageContext ();
				UIGraphics.EndImageContext ();
			}
			return image;
		}
	}
}

