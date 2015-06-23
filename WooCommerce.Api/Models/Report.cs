using System;
using Newtonsoft.Json;

namespace WooCommerce.Api
{



	public class SalesReport
	{
		[JsonProperty("total_sales")]
		public string TotalSales { get; set; }

		[JsonProperty("average_sales")]
		public string AverageSales { get; set; }

		[JsonProperty("total_orders")]
		public int TotalOrders { get; set; }

		[JsonProperty("total_items")]
		public int TotalItems { get; set; }

		[JsonProperty("total_tax")]
		public string TotalTax { get; set; }

		[JsonProperty("total_shipping")]
		public string TotalShipping { get; set; }

		[JsonProperty("total_discount")]
		public string TotalDiscount { get; set; }

		[JsonProperty("totals_grouped_by")]
		public string TotalsGroupedBy { get; set; }

		[JsonProperty("totals")]
		public Totals Totals { get; set; }

		[JsonProperty("total_customers")]
		public int TotalCustomers { get; set; }
	}

	public class Totals
	{
		[JsonProperty("orders")]
		public int Orders { get; set; }

		[JsonProperty("items")]
		public int Items { get; set; }

		[JsonProperty("customers")]
		public int Customers { get; set; }

		[JsonProperty("tax")]
		public string Tax { get; set; }

		[JsonProperty("shipping")]
		public string Shipping { get; set; }

		[JsonProperty("sales")]
		public string Sales { get; set; }

		[JsonProperty("discount")]
		public string Discount { get; set; }
	}
}

