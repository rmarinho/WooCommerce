﻿using System;

using Xamarin.Forms;
using System.Collections;

namespace WooCommerce
{
	public class ImageGalleryView : View
	{
		public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create ("ItemsSource", typeof(IEnumerable), typeof(ImageGalleryView), null, BindingMode.OneWay, null, null, null, null);

		public IEnumerable ItemsSource {
			get {
				return (IEnumerable)GetValue (ImageGalleryView.ItemsSourceProperty);
			}
			set {
				SetValue (ImageGalleryView.ItemsSourceProperty, value);
			}
		}


		public static readonly BindableProperty PathProperty = BindableProperty.Create ("Path", typeof(string), typeof(ImageGalleryView), null, BindingMode.OneWay, null, null, null, null);

		public string Path {
			get {
				return (string)GetValue (ImageGalleryView.PathProperty);
			}
			set {
				SetValue (ImageGalleryView.PathProperty, value);
			}
		}

	}
}


