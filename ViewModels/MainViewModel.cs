using System;
using Xamarin.Forms;
using System.Threading.Tasks;
namespace WooCommerce
{
	public class MainViewModel : BaseViewModel
	{
		public MainViewModel ()
		{
			GetData ();
		}

		public async Task GetData()
		{
			IsBusy = true;
			var result = await App.Client.GetStoreInfo ();
			var ordersCountresult = await App.Client.GetOrdersCount ();

			for (int i = 0; i < ordersCountresult; i++) {
				OrdersCount++;
			}
			StoreName = result.Name;
			IsBusy = false;
		}

		string storeName;
		public string StoreName {
			get{ return storeName; }
			set{ SetProperty (ref storeName, value); }
		}

		int ordersCount = 0;
		public int OrdersCount {
			get{ return ordersCount; }
			set{ SetProperty (ref ordersCount, value); }
		}

		public string PageName {
			get {
				return "Overview";
			}
		}
	}
}


