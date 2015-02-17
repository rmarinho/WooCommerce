using System;
using Xamarin.Forms;

namespace WooCommerce
{
	public class MenuItem
	{
		public MenuItem ()
		{
		}

		public string Title {
			get;
			set;
		}

		public Action<MasterDetailPage> Navigate
		{
			get;
			set;
		}
	}
}

