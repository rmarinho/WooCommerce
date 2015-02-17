using Xamarin.Forms;
using System;
using WooCommerce.Api;

namespace WooCommerce
{

	public class App : Application
	{
		public static WooCommerceClient Client;
		const string clientId = "ck_85fc8233163e4d288a876b02ad16451f";
		const string clientKey = "cs_7ccf7a5345d3f647a19a4054bbc5431a";

		public App ()
		{
			Client = new WooCommerceClient (clientId,clientKey);
			var mdMain = new MasterDetailPage ();
			//binding title doesn+t work.
			mdMain.Master = new MenuPage () { Title ="Start"};
			mdMain.Detail = new NavigationPage(new ReportsPage());
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

