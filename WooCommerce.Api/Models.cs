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

	public class BillingAddress2
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

	public class ShippingAddress2
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
		public BillingAddress2 billing_address { get; set; }
		public ShippingAddress2 shipping_address { get; set; }
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

	public class Product
	{
		public string title { get; set; }
		public int id { get; set; }
		public string created_at { get; set; }
		public string updated_at { get; set; }
		public string type { get; set; }
		public string status { get; set; }
		public bool downloadable { get; set; }
		public bool @virtual { get; set; }
		public string permalink { get; set; }
		public string sku { get; set; }
		public string price { get; set; }
		public string regular_price { get; set; }
		public object sale_price { get; set; }
		public string price_html { get; set; }
		public bool taxable { get; set; }
		public string tax_status { get; set; }
		public string tax_class { get; set; }
		public bool managing_stock { get; set; }
		public int stock_quantity { get; set; }
		public bool in_stock { get; set; }
		public bool backorders_allowed { get; set; }
		public bool backordered { get; set; }
		public bool sold_individually { get; set; }
		public bool purchaseable { get; set; }
		public bool featured { get; set; }
		public bool visible { get; set; }
		public string catalog_visibility { get; set; }
		public bool on_sale { get; set; }
		public object weight { get; set; }
		public Dimensions dimensions { get; set; }
		public bool shipping_required { get; set; }
		public bool shipping_taxable { get; set; }
		public string shipping_class { get; set; }
		public object shipping_class_id { get; set; }
		public string description { get; set; }
		public string short_description { get; set; }
		public bool reviews_allowed { get; set; }
		public string average_rating { get; set; }
		public int rating_count { get; set; }
		public List<int> related_ids { get; set; }
		public List<object> upsell_ids { get; set; }
		public List<object> cross_sell_ids { get; set; }
		public int parent_id { get; set; }
		public List<string> categories { get; set; }
		public List<object> tags { get; set; }
		public List<Image> images { get; set; }
		public string featured_src { get; set; }
		public List<object> attributes { get; set; }
		public List<object> downloads { get; set; }
		public int download_limit { get; set; }
		public int download_expiry { get; set; }
		public string download_type { get; set; }
		public string purchase_note { get; set; }
		public int total_sales { get; set; }
		public List<object> variations { get; set; }
		public List<object> parent { get; set; }
	}

}

