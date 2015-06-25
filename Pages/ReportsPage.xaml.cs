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
		
		}

		void HandleSelectedIndexChanged (object sender, System.EventArgs e)
		{
			var selectedPeriod = Extensions.ParseEnum<WooCommerceFilterPeriod>(filterOptions.Items [filterOptions.SelectedIndex]);
			ViewModel.PeriodFilter =  selectedPeriod;
		}

		protected override async void OnAppearing ()
		{
			filterOptions.SelectedIndexChanged += HandleSelectedIndexChanged;
			base.OnAppearing ();
		}

		protected override void OnDisappearing ()
		{
			ViewModel.SelectedProduct = null;
			filterOptions.SelectedIndexChanged -= HandleSelectedIndexChanged;
			base.OnDisappearing ();
		}

	}
}

