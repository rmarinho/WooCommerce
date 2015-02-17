using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WooCommerce.Api
{
	[JsonObject("store")]
	public class Store
	{
		[JsonProperty("name")]
		public string Name { get; set; }
		public string description { get; set; }
		public string URL { get; set; }
		public string wc_version { get; set; }
	
	}
	public class PaymentDetails
	{
		public string method_id { get; set; }
		public string method_title { get; set; }
		public bool paid { get; set; }
	}

	public class BillingAddress
	{
		public string first_name { get; set; }
		public string last_name { get; set; }
		public string company { get; set; }
		public string address_1 { get; set; }
		public string address_2 { get; set; }
		public string city { get; set; }
		public string state { get; set; }
		public string postcode { get; set; }
		public string country { get; set; }
		public string email { get; set; }
		public string phone { get; set; }
	}

	public class ShippingAddress
	{
		public string first_name { get; set; }
		public string last_name { get; set; }
		public string company { get; set; }
		public string address_1 { get; set; }
		public string address_2 { get; set; }
		public string city { get; set; }
		public string state { get; set; }
		public string postcode { get; set; }
		public string country { get; set; }
	}

	public class LineItem
	{
		public int id { get; set; }
		public string subtotal { get; set; }
		public string subtotal_tax { get; set; }
		public string total { get; set; }
		public string total_tax { get; set; }
		public string price { get; set; }
		public int quantity { get; set; }
		public string tax_class { get; set; }
		public string name { get; set; }
		public int product_id { get; set; }
		public string sku { get; set; }
		public List<object> meta { get; set; }
	}

	public class ShippingLine
	{
		public int id { get; set; }
		public string method_id { get; set; }
		public string method_title { get; set; }
		public string total { get; set; }
	}

	public class TaxLine
	{
		public int id { get; set; }
		public string rate_id { get; set; }
		public string code { get; set; }
		public string title { get; set; }
		public string total { get; set; }
		public bool compound { get; set; }
	}

	public class Customer
	{
		public int id { get; set; }
		public string created_at { get; set; }
		public string email { get; set; }
		public string first_name { get; set; }
		public string last_name { get; set; }
		public string username { get; set; }
		public string last_order_id { get; set; }
		public string last_order_date { get; set; }
		public int orders_count { get; set; }
		public string total_spent { get; set; }
		public string avatar_url { get; set; }
		public BillingAddress billing_address { get; set; }
		public ShippingAddress shipping_address { get; set; }
	}

	public class Dimensions
	{
		public string length { get; set; }
		public string width { get; set; }
		public string height { get; set; }
		public string unit { get; set; }
	}

	public class Image
	{
		public int id { get; set; }
		public string created_at { get; set; }
		public string updated_at { get; set; }
		public string src { get; set; }
		public string title { get; set; }
		public string alt { get; set; }
		public int position { get; set; }
	}



}

