using Xamarin.Forms;

namespace WooCommerce
{
	public partial class ProductDetailPage : TabbedPage	
	{

		public ProductDetailViewModel ViewModel {
			get{
				return BindingContext as ProductDetailViewModel;
			}
		}

		public ProductDetailPage ()
		{
			InitializeComponent ();
			BindingContext = new ProductDetailViewModel ();
			NavigationPage.SetBackButtonTitle (this, "");
		}
	}
}

