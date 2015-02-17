using System;
using Xamarin.Forms;
using System.Threading.Tasks;
namespace WooCommerce
{
	public class MainViewModel : BaseViewModel
	{
	
		public MainViewModel ()
		{
	
		}

		public async Task GetData()
		{
			IsBusy = true;
			var result = await App.Client.GetStoreInfo ();
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


