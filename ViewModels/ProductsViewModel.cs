using System;
using System.Collections.ObjectModel;
using WooCommerce.Api;
using System.Threading.Tasks;

namespace WooCommerce
{
	public class ProductsViewModel : BaseViewModel
	{
		public ProductsViewModel ()
		{
		}


		public async Task GetData()
		{
			IsBusy = true;
			var productsService = await App.Client.GetProducts ();
			Products.Clear ();
			foreach (var item in productsService) {
				Products.Add (item);
			}
			IsBusy = false;
		}


		ObservableCollection<Product> products = new ObservableCollection<Product>();
		public ObservableCollection<Product> Products {
			get{ return products; }
			set{ SetProperty (ref products, value); }
		}
	}
}

