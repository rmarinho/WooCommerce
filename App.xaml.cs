using Xamarin.Forms;
using WooCommerce.Api;

namespace WooCommerce
{
	public partial class App : Application
	{
		public static WooCommerceClient Client;
		public static Database DB;

		public static INavigation Navigation {
			get{ 
				return mdMain.Detail.Navigation;
			}
		}

		static MasterDetailPage mdMain;

		public App ()
		{
			InitializeComponent ();
			DB = new Database ();
			Client = new WooCommerceClient (Constants.baseStoreUrl, Constants.clientId, Constants.clientKey, DB);
			mdMain = new MasterDetailPage ();
			mdMain.Master = new MenuPage () {
				Icon = "nav_btn_md.png",
				Title = " "
			};
			mdMain.Detail = new BaseNavigationPage(new ProductsPage(),"GreenColor","AlmostSilver");
			MainPage = mdMain;
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

