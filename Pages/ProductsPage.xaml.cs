using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace WooCommerce
{
	public partial class ProductsPage : ContentPage
	{

		public ProductsViewModel ViewModel {
			get{
				return this.BindingContext as ProductsViewModel;
			}
		}

		public ProductsPage ()
		{
			InitializeComponent ();
			this.BindingContext = new ProductsViewModel();
		}
	}
}

