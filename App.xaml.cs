﻿using Xamarin.Forms;
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
		const string clientId = "ck_85fc8233163e4d288a876b02ad16451f";
		const string clientKey = "cs_7ccf7a5345d3f647a19a4054bbc5431a";
		const string baseStoreUrl = "https://xamstore.azurewebsites.net/";
		const string baseStoreUrl2 = "http://scespinho.pt/";
		const string baseStoreUrl3 = "http://skimstore.eu/";

		public App ()
		{
			InitializeComponent ();
			DB = new Database ();
			Client = new WooCommerceClient (baseStoreUrl, clientId, clientKey, DB);
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

