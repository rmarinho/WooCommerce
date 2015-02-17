using System;

namespace WooCommerce.Api
{



	public class SalesReport
	{
		public string total_sales { get; set; }
		public string average_sales { get; set; }
		public int total_orders { get; set; }
		public int total_items { get; set; }
		public string total_tax { get; set; }
		public string total_shipping { get; set; }
		public string total_discount { get; set; }
		public string totals_grouped_by { get; set; }
		public Totals totals { get; set; }
		public int total_customers { get; set; }
	}

	public class Totals
	{
	}
}

