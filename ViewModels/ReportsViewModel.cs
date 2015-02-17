using System;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using WooCommerce.Api;
using System.Collections.Generic;

namespace WooCommerce
{
	public class ReportsViewModel : BaseViewModel
	{
		public ReportsViewModel ()
		{
			periodFilterOptions = new List<WooCommerceFilterPeriod> {WooCommerceFilterPeriod.Week, WooCommerceFilterPeriod.Month, WooCommerceFilterPeriod.Year};
			maxDate = DateTime.Now.Date;
			minDate = maxDate.AddDays (-7);
			GetData ();
		}

		public async Task GetData(bool useFilters = false)
		{
			IsBusy = true;
			var reports = await App.Client.GetReports ();
			var topSellers = new List<TopSeller> ();
			if (useFilters) {
				topSellers = await App.Client.GetTopSellerReport (PeriodFilter, MinDate, MaxDate);
			} else {
				topSellers = await App.Client.GetTopSellerReport ();
			}

			TopSellerProducts.Clear ();
			foreach (var topSeller in topSellers) {
				var product = await App.Client.GetProductById (topSeller.ProductId);
				TopSellerProducts.Add (product);
			}
			IsBusy = false;
		}


		WooCommerceFilterPeriod periodFilter = WooCommerceFilterPeriod.None;
		public WooCommerceFilterPeriod PeriodFilter {
			get{ return periodFilter; }
			set{ 
				if (periodFilter != value) {
					SetProperty (ref periodFilter, value);
					minDate = maxDate = default(DateTime);
					GetData (true);
				}
			}
		}

		DateTime minDate;
		public DateTime MinDate {
			get{ return minDate; }
			set{ 
				if (minDate.Day != value.Day || minDate.Month != value.Month || minDate.Year != value.Year) {
					SetProperty (ref minDate, value);
					periodFilter = WooCommerceFilterPeriod.None;
					GetData (true);
				}
			}
		}

		DateTime maxDate;
		public DateTime MaxDate {
			get{ return maxDate; }
			set{ 
				if (minDate.Day != value.Day || minDate.Month != value.Month || minDate.Year != value.Year) {
					SetProperty (ref maxDate, value);
					periodFilter = WooCommerceFilterPeriod.None;
					GetData (true);
				}
			}
		}


		List<WooCommerceFilterPeriod> periodFilterOptions = new List<WooCommerceFilterPeriod>();
		public List<WooCommerceFilterPeriod> PeriodFilterOptions {
			get{ return periodFilterOptions; }
			set{ SetProperty (ref periodFilterOptions, value); }
		}


		ObservableCollection<Product> topSellerProducts = new ObservableCollection<Product>();
		public ObservableCollection<Product> TopSellerProducts {
			get{ return topSellerProducts; }
			set{ SetProperty (ref topSellerProducts, value); }
		}


		public string PageName {
			get {
				return "Products";
			}
		}
	}
}

