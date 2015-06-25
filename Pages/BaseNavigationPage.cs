using System;

using Xamarin.Forms;

namespace WooCommerce
{
	public class BaseNavigationPage : NavigationPage
	{
		public BaseNavigationPage (Page page, string resourceBarBackground, string resourceBarText) : base(page)
		{
			BarBackgroundColor = (Color)Application.Current.Resources [resourceBarBackground];
			BarTextColor = (Color)Application.Current.Resources [resourceBarText];
		}
	}
}


