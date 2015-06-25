using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace WooCommerce
{
	public partial class OrdersPage : ContentPage
	{

		public OrdersViewModel ViewModel {
			get{
				return this.BindingContext as OrdersViewModel;
			}
		}

		public OrdersPage ()
		{
			NavigationPage.SetBackButtonTitle (this, "");
			InitializeComponent ();
			this.BindingContext = new OrdersViewModel();
		}
	}
}

