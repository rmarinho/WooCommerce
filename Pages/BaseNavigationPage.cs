using System;

using Xamarin.Forms;

namespace WooCommerce
{
	public class BaseNavigationPage : NavigationPage
	{
		public BaseNavigationPage (Page page) : base(page)
		{
			BarBackgroundColor =  App.NavBarTint;
			BarTextColor = App.NavBarTextTint;
		}
	}
}


