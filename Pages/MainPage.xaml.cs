using Xamarin.Forms;

namespace WooCommerce
{
	public partial class MainPage : ContentPage
	{
		public MainViewModel ViewModel {
			get{
				return BindingContext as MainViewModel;
			}
		}

		public MainPage ()
		{
			InitializeComponent ();
			BindingContext = new MainViewModel ();
			NavigationPage.SetBackButtonTitle (this, "");
		}
	}
}

