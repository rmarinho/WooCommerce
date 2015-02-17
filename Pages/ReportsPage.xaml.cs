using Xamarin.Forms;

namespace WooCommerce
{
	public partial class ReportsPage : ContentPage
	{

		public ReportsViewModel ViewModel {
			get{
				return this.BindingContext as ReportsViewModel;
			}
		}

		public ReportsPage ()
		{
			InitializeComponent ();
			this.BindingContext = new ReportsViewModel();
		}
	}
}

