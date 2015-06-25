using Xamarin.Forms;

namespace WooCommerce
{
	public partial class ProductsPage : ContentPage
	{
		public ProductsViewModel ViewModel {
			get{
				return BindingContext as ProductsViewModel;
			}
		}

		public ProductsPage ()
		{
			InitializeComponent ();
			BindingContext = new ProductsViewModel();
			NavigationPage.SetBackButtonTitle (this,"" );
		}
	}
}

