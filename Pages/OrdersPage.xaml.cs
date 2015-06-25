using Xamarin.Forms;

namespace WooCommerce
{
	public partial class OrdersPage : ContentPage
	{
		public OrdersViewModel ViewModel {
			get{
				return BindingContext as OrdersViewModel;
			}
		}

		public OrdersPage ()
		{
			InitializeComponent ();
			BindingContext = new OrdersViewModel();
			NavigationPage.SetBackButtonTitle (this, "");
		}
	}
}

