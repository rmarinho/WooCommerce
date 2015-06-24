using Xamarin.Forms;
using WooCommerce.Api;
using OxyPlot;
using System.Threading.Tasks;

namespace WooCommerce
{
	public partial class ReportsPage : ContentPage
	{

		public ReportsViewModel ViewModel {
			get{
				return this.BindingContext as ReportsViewModel;
			}
		}

		public ReportsPage ()
		{
			InitializeComponent ();
			this.BindingContext = new ReportsViewModel();

			foreach (var periodFilterOption in ViewModel.PeriodFilterOptions) {
				filterOptions.Items.Add (periodFilterOption.ToString ());
			}
			filterOptions.SelectedIndex = 1;
		
			oxyPlotViewSales.Opacity  = oxyPlotViewOrders.Opacity = oxyPlotViewCustomers.Opacity = 0;
			oxyPlotViewSales.BackgroundColor = Color.Transparent;
		}

		void HandleSelectedIndexChanged (object sender, System.EventArgs e)
		{
			var selectedPeriod = Extensions.ParseEnum<WooCommerceFilterPeriod>(filterOptions.Items [filterOptions.SelectedIndex]);
			ViewModel.PeriodFilter =  selectedPeriod;
		}

		protected override void OnAppearing ()
		{
			ViewModel.PropertyChanged += ViewModel_PropertyChanged;
			filterOptions.SelectedIndexChanged += HandleSelectedIndexChanged;
			base.OnAppearing ();
		}

		protected override void OnDisappearing ()
		{
			ViewModel.SelectedProduct = null;
			filterOptions.SelectedIndexChanged -= HandleSelectedIndexChanged;
			ViewModel.PropertyChanged -= ViewModel_PropertyChanged;
			base.OnDisappearing ();
		}

		async void ViewModel_PropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "PlotDataReady") {
				await FadeChartsIn ();
			}
		}

		Task FadeChartsIn ()
		{
			var inOrOut = ViewModel.PlotDataReady ? 1 : 0;
			uint length = 400;

			return Task.WhenAll (new Task[] {
				oxyPlotViewSales.FadeTo (inOrOut, length, Easing.Linear),
				oxyPlotViewOrders.FadeTo (inOrOut, length * 2, Easing.Linear),
				oxyPlotViewCustomers.FadeTo (inOrOut, length * 3, Easing.Linear)
			});
		}
	}
}

