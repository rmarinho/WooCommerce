using System;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using WooCommerce.Api;

namespace WooCommerce
{
	public class ReportsViewModel : BaseViewModel
	{
		public ReportsViewModel ()
		{
			GetData ();
		}

		public async Task GetData()
		{
			IsBusy = true;
			var reports = await App.Client.GetReports ();
			var topSellers = await App.Client.GetTopSellerReport ();

			foreach (var topSeller in topSellers) {
				var product = await App.Client.GetProductById (topSeller.ProductId);
				TopSellerProducts.Add (product);
			}
			IsBusy = false;
		}


		ObservableCollection<Product> topSellerProducts = new ObservableCollection<Product>();
		public ObservableCollection<Product> TopSellerProducts {
			get{ return topSellerProducts; }
			set{ SetProperty (ref topSellerProducts, value); }
		}


		public string PageName {
			get {
				return "Products";
			}
		}
	}
}

