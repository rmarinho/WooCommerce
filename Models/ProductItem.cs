using System;

using Xamarin.Forms;

namespace WooCommerce
{
	public class ProductItem 
	{
		[SQLite.PrimaryKey]
		public int Id {
			get;
			set;
		}

		public string Title {
			get;
			set;
		}

		public string ImageUrl {
			get;
			set;
		}

		public string Payload {
			get;
			set;
		}
	}
}


