using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace WooCommerce
{
	public class SettingsViewModel : BaseViewModel
	{
		public SettingsViewModel ()
		{
			ClearCacheCommand = new Command (() => App.DB.ClearCache ());
		}

		public ICommand ClearCacheCommand { protected set; get; }

		public string PageName {
			get {
				return "Settings";
			}
		}
	}
}

