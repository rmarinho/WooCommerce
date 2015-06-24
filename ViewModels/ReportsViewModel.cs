using System;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using WooCommerce.Api;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using OxyPlot;
using OxyPlot.Series;

namespace WooCommerce
{
	public class ReportsViewModel : BaseViewModel
	{
		public ReportsViewModel ()
		{
			periodFilterOptions = new List<WooCommerceFilterPeriod> {WooCommerceFilterPeriod.Day, WooCommerceFilterPeriod.Week, WooCommerceFilterPeriod.Month, WooCommerceFilterPeriod.Year};
			maxDate = DateTime.Now.Date;
			minDate = maxDate.AddDays (-7);
			GetData ();
		}

		public async Task GetData(bool useFilters = false)
		{
			IsBusy = true;
			if (useFilters) {
				topSellers = await App.Client.GetTopSellerReport (PeriodFilter, MinDate, MaxDate);
			} else {
				await UpdateTotals ();
			}

			IsBusy = false;
		}

		SalesReport currentSalesReport;
		List<Order> currentOrders;
		List<TopSeller> topSellers;


		Task UpdateTotals(){
			var updateSalesReportTask = UpdateSalesReport ();
			var updateOrdersTask = UpdateOrdersNumbers ();
			var updateTopSellersTask = UpdateTopSellers ();
			return Task.WhenAll (new [] { updateSalesReportTask, updateOrdersTask, updateTopSellersTask });
		}

		async Task UpdateNumbersCount ()
		{
			for (int i = 0; i < currentSalesReport.TotalOrders; i++) {
				await Task.Delay (200);
				NewOrdersCount++;
			}
			for (int i = 0; i < currentSalesReport.TotalCustomers; i++) {
				await Task.Delay (200);
				NewCustomersCount++;
			}
		}

		async Task UpdateSalesReport ()
		{
			currentSalesReport = await App.Client.GetSalesReport (WooCommerceFilterPeriod.Month);
			if (currentSalesReport == null)
				return;
			
			UpdateNumbersCount ();
			AverageSales = currentSalesReport.AverageSales;
			TotalSales = currentSalesReport.TotalSales;
			periodFilter = Extensions.ParseEnum<WooCommerceFilterPeriod> (currentSalesReport.TotalsGroupedBy);
			GeneratePlotModels (currentSalesReport);
		}

		async Task UpdateTopSellers ()
		{
			TopSellerProducts.Clear ();
			topSellers = await App.Client.GetTopSellerReport ();
			foreach (var topSeller in topSellers) {
				var product = new Product ();
				product.TotalSales = topSeller.Quantity;
				product.Title = topSeller.Title;
				TopSellerProducts.Add (product);
			}
		}

		async Task UpdateOrdersNumbers()
		{
			currentOrders = await App.Client.GetOrders ();
			if (currentOrders != null) {
				for (int i = 0; i < currentOrders.Count; i++) {
					await Task.Delay (200);
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

		void GeneratePlotModels (SalesReport report)
		{
			PlotDataReady = false;
			var splotModel = GetSalesSplotModel (report);
			var noplotModel = GetNewOrdersSplotModel (report);
			var ncplotModel = GetNewCustomersSplotModel (report);
			NewCustomersPlotModel = ncplotModel;
			SalesPlotModel = splotModel;
			NewOrdersPlotModel = noplotModel;
			PlotDataReady = true;
		}

		static PlotModel GetNewCustomersSplotModel (SalesReport report)
		{
			var splotModel =  CreateChartOnlyBars ();

			var columnSeries = new ColumnSeries ();
			for (int i = 0; i < report.Totals.Count; i++) {
				var total = report.Totals.ElementAt (i).Value;
				columnSeries.Items.Add (new ColumnItem (total.Customers) {
					Color = OxyColor.Parse ( i% 2 == 0 ? "#c7d9e6" : "#e4ecf3")
				});
			}
			splotModel.Series.Add (columnSeries);
			return splotModel;
		}

		static PlotModel GetNewOrdersSplotModel (SalesReport report)
		{
			var splotModel =  CreateChartOnlyBars ();

			var columnSeries = new ColumnSeries ();
			for (int i = 0; i < report.Totals.Count; i++) {
				var total = report.Totals.ElementAt (i).Value;
				columnSeries.Items.Add (new ColumnItem (total.Orders) {
					Color = OxyColor.Parse ( i% 2 == 0 ? "#f2e3c7" : "#f5ebde") 
				});
			}
			splotModel.Series.Add (columnSeries);
			return splotModel;
		}

		static PlotModel GetSalesSplotModel (SalesReport report)
		{
			var splotModel =  CreateChartOnlyBars ();

			var columnSeries = new ColumnSeries ();
			for (int i = 0; i < report.Totals.Count; i++) {
				var total = report.Totals.ElementAt (i).Value;
				double value;
				double.TryParse (total.Sales, out value);
				if (value == 0)
					value = 1;
				columnSeries.Items.Add (new ColumnItem (value) {
					Color = OxyColor.Parse ( i% 2 == 0 ? "#dee3c7" : "#ebedde") 
				});
			}
			splotModel.Series.Add (columnSeries);
			return splotModel;
		}

		static PlotModel CreateChartOnlyBars ()
		{
			var splotModel = new PlotModel ();


			splotModel.IsLegendVisible = false;
			splotModel.Background = OxyColor.FromArgb (0, 0, 0, 255);
			splotModel.TextColor = OxyColor.FromArgb (0, 0, 0, 255);
			splotModel.PlotAreaBorderColor = OxyColor.FromArgb (0, 0, 0, 255);

			var axis = new OxyPlot.Axes.LinearAxis ();
			axis.IsPanEnabled =  axis.IsAxisVisible = false;

			splotModel.Axes.Add (axis);

			var dateAxis = new OxyPlot.Axes.CategoryAxis ();
			dateAxis.IsPanEnabled =   dateAxis.IsAxisVisible = false;
			splotModel.Axes.Add (dateAxis);

			return splotModel;
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

		int newOrdersCount = 0;
		public int NewOrdersCount {
			get{ 
				return newOrdersCount;
			}
			set{ SetProperty (ref newOrdersCount, value); }
		}


		int newCustomersCount = 0;
		public int NewCustomersCount {
			get{ 
				return newCustomersCount;
			}
			set{ SetProperty (ref newCustomersCount, value); }
		}

		double totalSales;
		public string TotalSales {
			get{ 
				return string.Format("{0} {1}",totalSales, App.Client.Currency);
			}
			set{ 
				SetProperty (ref totalSales, double.Parse(value)); 
			}
		}


		string averageSales = "";
		public string AverageSales {
			get{ 
				return string.Format("{0:C}",averageSales);
			}
			set{ SetProperty (ref averageSales, value); }
		}

		bool plotDataReady;
		public bool PlotDataReady {
			get{ 
				return plotDataReady;
			}
			set{ SetProperty (ref plotDataReady, value); }
		}

		Product selectedProduct = null;
		public Product SelectedProduct {
			get{ return selectedProduct; }
			set{ 
				if (selectedProduct != value) {
					SetProperty (ref selectedProduct, value);
					if (selectedProduct != null) {
						var detailPage = new ProductDetailPage ();
						detailPage.ViewModel.Product = selectedProduct;
						App.Navigation.PushAsync (detailPage);
					}
				}
			}
		}

		PlotModel salesPlotModel;
		public PlotModel SalesPlotModel {
			get{ 
				return salesPlotModel;
			}
			set{ SetProperty (ref salesPlotModel, value); }
		}


		PlotModel newOrdersPlotModel;
		public PlotModel NewOrdersPlotModel {
			get{ 
				return newOrdersPlotModel;
			}
			set{ SetProperty (ref newOrdersPlotModel, value); }
		}


		PlotModel newCustomersPlotModel;
		public PlotModel NewCustomersPlotModel {
			get{ 
				return newCustomersPlotModel;
			}
			set{ SetProperty (ref newCustomersPlotModel, value); }
		}

		public string PageName {
			get {
				return "Reports";
			}
		}
	}
}

