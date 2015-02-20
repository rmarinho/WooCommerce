using System;
using WooCommerce.Api;

namespace WooCommerce
{
	public class ProductDetailViewModel : BaseViewModel
	{
		public ProductDetailViewModel ()
		{
		}

		Product product = null;
		public Product Product {
			get{ return product; }
			set{ 
				SetProperty (ref product, value);
			}
		}
	}
}

