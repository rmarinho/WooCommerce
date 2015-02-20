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
			InitializeComponent ();
			this.BindingContext = new ProductDetailViewModel ();

		}
	}
}

