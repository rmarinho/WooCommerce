using System;
using System.Collections.ObjectModel;
using WooCommerce.Api;
using System.Threading.Tasks;

namespace WooCommerce
{
	public class OrdersViewModel : BaseViewModel
	{
		public OrdersViewModel ()
		{
			GetData ();
		}


		public async Task GetData()
		{
			IsBusy = true;
			var result = await App.Client.GetOrders ();
			IsBusy = false;
		}
		ObservableCollection<Order> orders = new ObservableCollection<Order>();
		public ObservableCollection<Order> Orders {
			get{ return orders; }
			set{ SetProperty (ref orders, value); }
		}

		int ordersCount;
		public int OrdersCount {
			get{ return ordersCount; }
			set{ SetProperty (ref ordersCount, value); }
		}


		public string PageName {
			get {
				return "Orders";
			}
		}
	}
}

