using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace WooCommerce
{
	public partial class ProductDetailPage : TabbedPage	
	{

		public ProductDetailViewModel ViewModel {
			get{
				return this.BindingContext as ProductDetailViewModel;
			}
		}

		public ProductDetailPage ()
		{
			NavigationPage.SetBackButtonTitle (this, "");
			InitializeComponent ();
			this.BindingContext = new ProductDetailViewModel ();

		}
	}
}

