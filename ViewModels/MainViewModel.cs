using System;

using Xamarin.Forms;
using WooCommerce.Api;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

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
			var productsService = await client.GetProducts ();
			Products.Clear ();
			foreach (var item in productsService) {
				Products.Add (item);
			}
			IsBusy = false;
		}

		string storeName;
		public string StoreName {
			get{ return storeName; }
			set{ SetProperty (ref storeName, value); }
		}


		ObservableCollection<Product> products = new ObservableCollection<Product>();
		public ObservableCollection<Product> Products {
			get{ return products; }
			set{ SetProperty (ref products, value); }
		}


	}
}


