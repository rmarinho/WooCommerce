using System;
using Newtonsoft.Json;

namespace WooCommerce.Api
{
//	"title": "Happy Ninja",
//	"product_id": "37",
//	"quantity": "24"
	public class TopSeller
	{
		[JsonProperty("title")]
		public string Title { get; set; }
		[JsonProperty("quantity")]
		public int Quantity { get; set; }
		[JsonProperty("product_id")]
		public int ProductId { get; set; }
	}
}

