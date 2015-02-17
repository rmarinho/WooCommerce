using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace WooCommerce
{
	public partial class MenuPage : ContentPage
	{

		public MenuViewModel ViewModel {
			get{
				return this.BindingContext as MenuViewModel;
			}
		}

		public MenuPage ()
		{
			InitializeComponent ();
			this.BindingContext = new MenuViewModel ();
		}
	}
}

