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

			OxyPlot.Xamarin.Forms.Platform.iOS.Forms.Init();

			ImageCircleRenderer.Init();
		
			global::Xamarin.Forms.Forms.Init ();

			LoadApplication (new App ());

			return base.FinishedLaunching (app, options);
		}

		void Preety ()
		{
			UINavigationBar.Appearance.SetTitleTextAttributes (new UITextAttributes () {
				Font = UIFont.FromName ("HelveticaNeue-Light", 20),
			});
		}
	}
}

