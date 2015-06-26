using System;
using UIKit;
using System.Collections.ObjectModel;
using CoreGraphics;
using Foundation;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Drawing;

namespace WooCommerce.iOS
{
	public class ImageGalleryUIView : UIView
	{
		UIPageControl pageControl;
		UIScrollView scroller;
		ObservableCollection<string> images;

		public ImageGalleryUIView (ObservableCollection<string> imgs)
			: this (default(CGRect), imgs)
		{
		
		}

		public ImageGalleryUIView (CGRect frame, ObservableCollection<string> imgs = null)
			: base (frame)
		{
			AutoresizingMask = UIViewAutoresizing.All;
			ContentMode = UIViewContentMode.ScaleToFill;

			FadeImages = true;

			BackgroundColor = UIColor.White;

			Frame = frame == default(CGRect) ? UIScreen.MainScreen.Bounds : frame;

			images = imgs ?? new ObservableCollection<string> ();
			images.CollectionChanged += HandleCollectionChanged;

			pageControl = new UIPageControl {
				AutoresizingMask = UIViewAutoresizing.All,
				ContentMode = UIViewContentMode.ScaleToFill
			};

			pageControl.ValueChanged += PageControlValueChanged;

			scroller = new UIScrollView {
				AutoresizingMask = UIViewAutoresizing.All,
				ContentMode = UIViewContentMode.ScaleToFill,
				PagingEnabled = true,
				Bounces = false,
				ShowsHorizontalScrollIndicator = false,
				ShowsVerticalScrollIndicator = false
			};

			scroller.Scrolled += ScrollChanged;

			NSNotificationCenter.DefaultCenter.AddObserver (
				UIApplication.DidChangeStatusBarOrientationNotification,
				not => {
					var orientation = UIDevice.CurrentDevice.Orientation;
					if ((UIDeviceOrientation.LandscapeLeft == orientation || UIDeviceOrientation.LandscapeRight == orientation)) {
						scroller.ContentSize = new CGSize (Frame.Height * images.Count - 1, Frame.Width);
					} else {
						scroller.ContentSize = new CGSize (Frame.Width * images.Count - 1, Frame.Height);
					}
					UpdateScrollPositionBasedOnPageControl ();
				});


			Add (scroller);
			Add (pageControl);
		}

		void PageControlValueChanged (object sender, EventArgs e)
		{
			UpdateScrollPositionBasedOnPageControl ();
		}

		public void ClearImages ()
		{
			images.Clear ();
		}

		public void AddImage (string url)
		{
			images.Add (url);
		}

		public void RemoveImage (string url)
		{
			images.Remove (url);
		}

		public bool FadeImages { get; set; }


		protected override void Dispose (bool disposing)
		{
			if (disposing && scroller != null) {
				foreach (var subview in scroller.Subviews) {
					subview.Dispose ();
				}
				scroller.Scrolled -= ScrollChanged;
				scroller.Dispose ();
				scroller = null;

				pageControl.ValueChanged -= PageControlValueChanged;
				pageControl = null;

				images.CollectionChanged -= HandleCollectionChanged;
				images.Clear ();
			}
			base.Dispose (disposing);
		}

		public override void LayoutSubviews ()
		{
			if(scroller.Bounds != Bounds)
				UpdateBounds (Bounds);

			base.LayoutSubviews ();
		}

		void UpdateBounds (CGRect rect)
		{
			pageControl.Frame = new CGRect (rect.Left, rect.Height - 40, rect.Width, 40);
			scroller.Frame = new CGRect (rect.Left, rect.Top, rect.Width, rect.Height);
			var curr = 0;
			foreach (var im in images) {
				try {
					AddImage (rect, curr, im);
					curr++;
				}
				catch (Exception) {
					// ignored
				}
			}
			scroller.ContentSize = new CGSize (scroller.Frame.Width * curr - 1, scroller.Frame.Height);
			pageControl.Pages = curr;
		}

		void AddImage (CGRect rect, nint position, string im)
		{
			var img = new UIImage ();
			var isRemote = Helpers.IsValidUrl (im);

			if (isRemote) {
				//dont await , fire and forget
				LoadImageAsync (position, im);
			} else {
				img = UIImage.FromFile (im);
			}

			var imgView = new UIImageView (img) {
				AutoresizingMask = UIViewAutoresizing.All,
				ContentMode = UIViewContentMode.ScaleAspectFill
			};

			if (FadeImages) {
				imgView.Alpha = 0;
			}

			//if first image is local, fade it in
			if (position == 0 && !isRemote) {
				FadeImageViewIn (imgView);
			}

			imgView.Frame = new CGRect (rect.Width * position, rect.Top, rect.Width, rect.Height);

			scroller.AddSubview (imgView);
		}


		Task LoadImageAsync (nint position, string url)
		{
			return Task.Run (
				() => {
					var img = Helpers.LoadFromUrl (url);

					InvokeOnMainThread (
						() => {
							var imgView = scroller.Subviews [position] as UIImageView;
							if (pageControl.CurrentPage == position && FadeImages) {
								FadeImageViewIn (imgView, img);
							} else {
								imgView.Image = img;
							}
						});
				});
		}

		void ScrollChanged (object sender, EventArgs e)
		{
			var pageWidth = double.Parse (scroller.Bounds.Width.ToString ());
			var oof = double.Parse (scroller.ContentOffset.X.ToString ());
			var pageNumber = int.Parse ((Math.Floor ((oof - pageWidth / 2) / pageWidth) + 1).ToString ());
			var imgView = scroller.Subviews [pageNumber] as UIImageView;
			FadeImageViewIn (imgView);
			pageControl.CurrentPage = pageNumber;
		}

			
		void HandleCollectionChanged (object sender, NotifyCollectionChangedEventArgs e)
		{
			if (e.Action == NotifyCollectionChangedAction.Add) {
				foreach (var newImage in e.NewItems) {
					try {
						BeginInvokeOnMainThread (
							() => {
								AddImage (Frame, pageControl.Pages, newImage as string);
								scroller.ContentSize = new CGSize (Frame.Width * (pageControl.Pages + 1), scroller.Frame.Height);
								pageControl.Pages = pageControl.Pages + 1;
							});
					} catch (Exception ex) {
					}
				}
			}
		}

			
		static void SetImage (UIImageView imgView, UIImage img)
		{
			if (img != null) {
				imgView.Image = img;
			}
			imgView.Alpha = 1;
		}


		void UpdateScrollPositionBasedOnPageControl ()
		{
			var off = pageControl.CurrentPage * scroller.Frame.Width;
			scroller.SetContentOffset (new CGPoint (off, 0), true);
		}


		void FadeImageViewIn (UIImageView imgView, UIImage img = null)
		{
			if (FadeImages) {
				Animate (0.3, 0, UIViewAnimationOptions.TransitionCrossDissolve, () => {
					SetImage (imgView, img);
				}, () => {
				});
			} else {
				SetImage (imgView, img);
			}
		}
	}
		
	static class Helpers
	{
		public static bool IsValidUrl (string urlString)
		{
			Uri uri;
			return Uri.TryCreate (urlString, UriKind.Absolute, out uri)
			&& (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps || uri.Scheme == Uri.UriSchemeFtp
			|| uri.Scheme == Uri.UriSchemeMailto);
		}

		public static UIImage LoadFromUrl (string uri)
		{
			using (var url = new NSUrl (uri))
			using (var data = NSData.FromUrl (url))
				return UIImage.LoadFromData (data);
		}
	}
}