using System;
using Xamarin.Forms;

namespace WooCommerce
{
	public class InvertBoolean : IValueConverter
	{
		public object Convert (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			bool b;
			if (bool.TryParse (value.ToString(), out b))
				return !b;
			else
				return value;
		}

		public object ConvertBack (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return value;
		}
	}
}

