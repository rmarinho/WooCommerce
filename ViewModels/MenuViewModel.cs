using System;

using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace WooCommerce
{
	public class MenuViewModel : BaseViewModel
	{
		public MenuViewModel ()
		{
			MenuItems.Add (new MenuItem { Title = "Start", Navigate = (md) => md.Detail = new BaseNavigationPage(new MainPage()) });
			MenuItems.Add (new MenuItem { Title = "Products", Navigate = (md) => md.Detail = new BaseNavigationPage(new ProductsPage()) });
			MenuItems.Add (new MenuItem { Title = "Orders", Navigate = (md) => md.Detail = new BaseNavigationPage(new OrdersPage()) });
			MenuItems.Add (new MenuItem { Title = "Reports", Navigate = (md) => md.Detail = new BaseNavigationPage(new ReportsPage()) });
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
							selectedMenuItem.Navigate (mdp	);
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


