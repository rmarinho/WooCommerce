using Xamarin.Forms;

namespace WooCommerce
{
	public partial class MenuPage : ContentPage
	{
		public MenuViewModel ViewModel {
			get {
				return BindingContext as MenuViewModel;
			}
		}

		public MenuPage ()
		{
			InitializeComponent ();
			BindingContext = new MenuViewModel ();
		}

	}
}

