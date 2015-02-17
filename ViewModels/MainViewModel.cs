using System;

using Xamarin.Forms;
using WooCommerce.Api;
using System.Threading.Tasks;

namespace WooCommerce
{
	public class MainViewModel : BaseViewModel
	{
		WooCommerceClient client;
		public MainViewModel ()
		{
			var clientId = "ck_85fc8233163e4d288a876b02ad16451f";
			var clientKey = "cs_7ccf7a5345d3f647a19a4054bbc5431a";
			client = new WooCommerceClient (clientId,clientKey);
			GetData ();
		}

		public async Task GetData()
		{
			IsBusy = true;
			var result = await client.GetStoreInfo ();
			StoreName = result.Name;
			IsBusy = false;
		}

		string storeName;
		public string StoreName {
			get{ return storeName; }
			set{ SetProperty (ref storeName, value); }
		}
	}
}


