using System;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using WooCommerce.Api;
using System.Collections.Generic;
using System.Linq;

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
		SalesReport currentSalesReport;
		List<Order> currentOrders;
		public async Task GetData(bool useFilters = false)
		{
			IsBusy = true;
			var reports = await App.Client.GetReports ();
			var topSellers = new List<TopSeller> ();
			var orders = new List<Order> ();
			if (useFilters) {
				topSellers = await App.Client.GetTopSellerReport (PeriodFilter, MinDate, MaxDate);
			} else {
				topSellers = await App.Client.GetTopSellerReport ();
				currentSalesReport = await App.Client.GetSalesReport ();
				currentOrders = await App.Client.GetOrders ();
				UpdateTotals ();
			}

			TopSellerProducts.Clear ();
			foreach (var topSeller in topSellers) {
				var product = await App.Client.GetProductById (topSeller.ProductId);
				TopSellerProducts.Add (product);
			}
			IsBusy = false;
		}

		void UpdateTotals(){
			if (currentOrders != null) {
				for (int i = 0; i < currentOrders.Count; i++) {
				
					if (currentOrders [i].status == "processing") {
						ProcessingOrdersCount++;
					}
					if (currentOrders [i].status == "pending") {
						PendingOrdersCount++;
					}
					if (currentOrders [i].status == "on-hold") {
						PendingOrdersCount++;
					}
					if (currentOrders [i].status == "completed") {
						CompletedOrdersCount++;
					}
				}	
			
			}
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

		int pendingOrdersCount = 0;
		public int PendingOrdersCount {
			get{ 
				return pendingOrdersCount;
			}
			set{ SetProperty (ref pendingOrdersCount, value); }
		}

		int processingOrdersCount = 0;
		public int ProcessingOrdersCount {
			get{ 
				return processingOrdersCount;
			}
			set{ SetProperty (ref processingOrdersCount, value); }
		}

		int heldOrdersCount = 0;
		public int HeldOrdersCount {
			get{ 
				return heldOrdersCount;
			}
			set{ SetProperty (ref heldOrdersCount, value); }
		}

		int completedOrdersCount = 0;
		public int CompletedOrdersCount {
			get{ 
				return completedOrdersCount;
			}
			set{ SetProperty (ref completedOrdersCount, value); }
		}

		public string PageName {
			get {
				return "Products";
			}
		}
	}
}

