using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using UIKit;
using WooCommerce.iOS;
using WooCommerce;
using System.Collections.ObjectModel;
using System.Collections;

[assembly: ExportRenderer(typeof(ListView), typeof(ExtendedListViewRenderer))]
[assembly: ExportRenderer(typeof(ImageGalleryView), typeof(ImageGalleryViewRenderer))]
namespace WooCommerce.iOS
{
	public class ImageGalleryViewRenderer : ViewRenderer<ImageGalleryView,ImageGalleryUIView>
	{
		public override SizeRequest GetDesiredSize (double widthConstraint, double heightConstraint)
		{
			return base.GetDesiredSize (widthConstraint, heightConstraint);
		}
		protected override void OnElementChanged (ElementChangedEventArgs<ImageGalleryView> e)
		{
			base.OnElementChanged (e);
			if (e.OldElement != null) {
				UnBind (e.OldElement);
			}
			if (e.NewElement != null) {
				if (Control == null) {
					var imageGalleryView = new ImageGalleryUIView (new ObservableCollection<string>());
					Bind (e.NewElement);
					SetNativeControl(imageGalleryView);
				}
			}

		}

		void UnBind(ImageGalleryView newElement)
		{
			if (newElement != null)
			{
				newElement.PropertyChanged -= ElementPropertyChanged;
			}
		}

		void Bind(ImageGalleryView newElement)
		{
			if (newElement != null)
			{
				newElement.PropertyChanged += ElementPropertyChanged;
			}
		}

		void ElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "ItemsSource")
			{
				if (Element.ItemsSource != null && !string.IsNullOrEmpty(Element.Path)) {
					Control.ClearImages ();
					foreach (var item in (IList)Element.ItemsSource) {
						var str = item.GetType ().GetProperty (Element.Path).GetValue(item) as string;
						if (str != null) {
							Control.AddImage (str);
						}
					}
				}
			}
		}
	}

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
}

