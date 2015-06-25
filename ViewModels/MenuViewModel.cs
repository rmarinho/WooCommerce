using System;

using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace WooCommerce
{
	public class MenuViewModel : BaseViewModel
	{
		public MenuViewModel ()
		{
			MenuItems.Add (new MenuItem { Title = "Start", Color = (Color)Application.Current.Resources["GrayColor"], Navigate = (md) => md.Detail = new BaseNavigationPage(new MainPage(), "GrayColor","AlmostBlack") });
			MenuItems.Add (new MenuItem { Title = "Products", Color = (Color)Application.Current.Resources["GreenColor"], Navigate = (md) => md.Detail = new BaseNavigationPage(new ProductsPage(),"GreenColor","AlmostWhite") });
			MenuItems.Add (new MenuItem { Title = "Orders", Color = (Color)Application.Current.Resources["BlueColor"], Navigate = (md) => md.Detail = new BaseNavigationPage(new OrdersPage(),"BlueColor","AlmostSilver")  });
			MenuItems.Add (new MenuItem { Title = "Reports", Color =  (Color)Application.Current.Resources["OrangeColor"], Navigate = (md) => md.Detail = new BaseNavigationPage(new ReportsPage(),"OrangeColor","AlmostBlack") });
			MenuItems.Add (new MenuItem { Title = "Settings", Color =  (Color)Application.Current.Resources["DarkGrayColor"], Navigate = (md) => md.Detail = new BaseNavigationPage(new SettingsPage(),"DarkGrayColor","AlmostWhite") });

		}

		ObservableCollection<MenuItem> menuItems = new ObservableCollection<MenuItem>();
		public ObservableCollection<MenuItem> MenuItems {
			get{ return menuItems; }
			set{ SetProperty (ref menuItems, value); }
		}

		MenuItem selectedMenuItem;
		public MenuItem SelectedMenuItem {
			get{ return selectedMenuItem; }
			set{ 
				if (value != selectedMenuItem) {
					SetProperty (ref selectedMenuItem, value);
					if (selectedMenuItem != null) {
						var mdp = (MasterDetailPage)Application.Current.MainPage;
						if (mdp != null) {
							selectedMenuItem.Navigate (mdp);
							SelectedMenuItem = null;
							mdp.IsPresented = false;
						}
					}
				}
			}
		}

		public string PageName {
			get {
				return "Menu";
			}
		}
	}
}


