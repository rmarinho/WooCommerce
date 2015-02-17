using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WooCommerce.Api
{
	public class Order
	{
		[JsonProperty("id")]
		public int Id { get; set; }
		public int order_number { get; set; }
		public string created_at { get; set; }
		public string updated_at { get; set; }
		public string completed_at { get; set; }
		public string status { get; set; }
		public string currency { get; set; }
		public string total { get; set; }
		public string subtotal { get; set; }
		public int total_line_items_quantity { get; set; }
		public string total_tax { get; set; }
		public string total_shipping { get; set; }
		public string cart_tax { get; set; }
		public string shipping_tax { get; set; }
		public string total_discount { get; set; }
		public string shipping_methods { get; set; }
		public PaymentDetails payment_details { get; set; }
		public BillingAddress billing_address { get; set; }
		public ShippingAddress shipping_address { get; set; }
		public string note { get; set; }
		public string customer_ip { get; set; }
		public string customer_user_agent { get; set; }
		public int customer_id { get; set; }
		public string view_order_url { get; set; }
		public List<LineItem> line_items { get; set; }
		public List<ShippingLine> shipping_lines { get; set; }
		public List<TaxLine> tax_lines { get; set; }
		public List<object> fee_lines { get; set; }
		public List<object> coupon_lines { get; set; }
		public Customer customer { get; set; }
	}
}

