using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WooCommerce.Api
{
	public class Product
	{
		[JsonProperty("title")]
		public string Title { get; set; }
		[JsonProperty("id")]
		public int Id { get; set; }
		public string created_at { get; set; }
		public string updated_at { get; set; }
		public string type { get; set; }
		public string status { get; set; }
		public bool downloadable { get; set; }

		public string permalink { get; set; }
		[JsonProperty("sku")]
		public string SKU { get; set; }
		[JsonProperty("price")]
		public string Price { get; set; }
		public string regular_price { get; set; }
		public object sale_price { get; set; }
		public string price_html { get; set; }
		public bool taxable { get; set; }
		public string tax_status { get; set; }
		[JsonProperty("tax_class")]
		public string TaxClass { get; set; }
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
		public List<string> tags { get; set; }
		[JsonProperty("images")]
		public List<Image> Images { get; set; }
		[JsonProperty("featured_src")]
		public string FeaturedImage { get; set; }
		public List<object> attributes { get; set; }
		public List<object> downloads { get; set; }
		public int download_limit { get; set; }
		public int download_expiry { get; set; }
		public string download_type { get; set; }
		public string purchase_note { get; set; }
		[JsonProperty("total_sales")]
		public int TotalSales { get; set; }
		public List<object> variations { get; set; }
		public List<object> parent { get; set; }

		public string Categories {
			get { return categories == null ? "" : String.Join (",", categories); }
		}
		public string Tags {
			get { return tags == null ? "" : String.Join (",", tags); }
		}

		public string Image {
			get { 
				if(!string.IsNullOrEmpty(this.FeaturedImage))
					return this.FeaturedImage;
				if (this.Images != null && this.Images.Count > 0)
					return this.Images [0].src;
				return "icon-76";
			}

		}
	}
}

