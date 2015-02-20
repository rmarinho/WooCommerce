using Xamarin.Forms;
using WooCommerce.Api;

namespace WooCommerce
{
	public partial class App : Application
	{
		public static Color NavBarTint = Color.FromHex("#f8f8f8");
		public static Color NavBarTextTint = Color.FromHex("#333");
		public static Color NavBarButtonTint = Color.FromHex("#9b9b9b");

		public static WooCommerceClient Client;
		static MasterDetailPage mdMain;

		const string clientId = "ck_85fc8233163e4d288a876b02ad16451f";
		const string clientKey = "cs_7ccf7a5345d3f647a19a4054bbc5431a";
		const string baseStoreUrl = "https://xamstore.azurewebsites.net/";
		const string baseStoreUrl2 = "http://scespinho.pt/";
		const string baseStoreUrl3 = "http://skimstore.eu/";

		public static INavigation Navigation {
			get{ 
				return mdMain.Detail.Navigation;
			}
		}

		public App ()
		{
			InitializeComponent ();
			Client = new WooCommerceClient (baseStoreUrl,clientId,clientKey);
			mdMain = new MasterDetailPage ();
			//binding title doesn+t work.
			mdMain.Master = new MenuPage () {
				Icon = "nav_btn_md.png",
				Title = " "
			};
			mdMain.Detail = new NavigationPage(new ProductsPage());
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

