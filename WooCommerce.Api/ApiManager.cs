using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace WooCommerce.Api
{


	public enum WooCommerceFilterPeriod
	{
		None = 0,
		Day = 1,
		Week = 2,
		Month = 3,
		// Analysis disable once InconsistentNaming
		Last_month = 4,
		Year = 5,
	}

	public static class WooCommerceFilter
	{
		public static string Period = "filter[period]";
		public static string MinDate = "filter[date_min]";
		public static string MaxDate = "filter[date_max]";

	}

	public static class WooCommerceEndpoints
	{
		public static string Index = "wc-api/v2";
		public static string Products = "wc-api/v2/products";
		public static string Product = "wc-api/v2/products/{0}";
		public static string ProductSku = "wc-api/v2/products/sku/{0}";
		public static string Orders = "wc-api/v2/orders";
		public static string OrdersCount = "wc-api/v2/orders/count";
		public static string OrdersStatuses = "wc-api/v2/orders/statuses";
			public static string Reports = "wc-api/v2/reports";
		public static string ReportSales = "wc-api/v2/reports/sales";
		public static string ReportTopSellers = "wc-api/v2/reports/sales/top_sellers";
	}

	public class WooCommerceClient
	{
		string _appUrl;
		HttpClient _client;

		public WooCommerceClient()
		{
			Version = DefaultVersion;
			_appUrl = "https://xamstore.azurewebsites.net/";
			_client = new HttpClient ();
			_client.BaseAddress = new Uri(_appUrl);
			Currency = "€";
		}

		public WooCommerceClient(string appId, string appSecret)
			: this()
		{
			if (string.IsNullOrEmpty(appId))
				throw new ArgumentNullException("appId");

			if (string.IsNullOrEmpty(appSecret))
				throw new ArgumentNullException("appSecret");

			AppId = appId;
			AppSecret = appSecret;
			_client.BaseAddress = new Uri(_appUrl);
			var byteArray = Encoding.UTF8.GetBytes(string.Format("{0}:{1}", appId, appSecret));

			_client.DefaultRequestHeaders.Authorization =  new AuthenticationHeaderValue("Basic",  Convert.ToBase64String(byteArray));

		}


		public async Task<List<string>> GetReports()
		{
			var request = PrepareRequest (HttpMethod.Get, WooCommerceEndpoints.Reports, null); 
			var response = await ExecuteRequest (request);
			var result = await ProcessResponse<List<string>>(response);
			return result;
		}

		public async Task<List<TopSeller>> GetTopSellerReport(WooCommerceFilterPeriod period = default(WooCommerceFilterPeriod), 
			DateTime minDate = default(DateTime), DateTime maxDate = default(DateTime))
		{
			var parameters = HandleFilters (period, minDate, maxDate);
			var request = PrepareRequest (HttpMethod.Get, WooCommerceEndpoints.ReportTopSellers, parameters); 
			var response = await ExecuteRequest (request);
			var result = await ProcessResponse<List<TopSeller>>(response);
			return result;
		}

		public async Task<SalesReport> GetSalesReport(WooCommerceFilterPeriod period = default(WooCommerceFilterPeriod), 
			DateTime minDate = default(DateTime), DateTime maxDate = default(DateTime))
		{
			var parameters = HandleFilters (period, minDate, maxDate);
			var request = PrepareRequest (HttpMethod.Get, WooCommerceEndpoints.ReportSales, parameters); 
			var response = await ExecuteRequest (request);
			var result = await ProcessResponse<SalesReport>(response);
			return result;
		}

		public async Task<Product> GetProductBySku(string sku)
		{
			var request = PrepareRequest (HttpMethod.Get, string.Format(WooCommerceEndpoints.ProductSku,sku), null); 
			var response = await ExecuteRequest (request);
			var result = await ProcessResponse<Product>(response);
			return result;
		}

		public async Task<Product> GetProductById(int id)
		{
			var request = PrepareRequest (HttpMethod.Get, string.Format(WooCommerceEndpoints.Product,id), null); 
			var response = await ExecuteRequest (request);
			var result = await ProcessResponse<Product>(response);
			return result;
		}

		public async Task<List<Product>> GetProducts()
		{
			var request = PrepareRequest (HttpMethod.Get, WooCommerceEndpoints.Products, null); 
			var response = await ExecuteRequest (request);
			var result = await ProcessResponse<List<Product>>(response);
			return result;
		}

		public async Task<List<Order>> GetOrders()
		{
			var request = PrepareRequest (HttpMethod.Get, WooCommerceEndpoints.Orders, null); 
			var response = await ExecuteRequest (request);
			var result = await ProcessResponse<List<Order>>(response);
			return result;
		}

		public async Task<int> GetOrdersCount()
		{
			var request = PrepareRequest (HttpMethod.Get, WooCommerceEndpoints.OrdersCount, null); 
			var response = await ExecuteRequest (request);
			var result = await ProcessResponse<int>(response);
			return result;
		}

		public async Task<OrderStatus> GetOrdersStatuses()
		{
			var request = PrepareRequest (HttpMethod.Get, WooCommerceEndpoints.OrdersStatuses, null); 
			var response = await ExecuteRequest (request);
			var result = await ProcessResponse<OrderStatus>(response);
			return result;
		}

		public async Task<Store> GetStoreInfo()
		{
			var request = PrepareRequest (HttpMethod.Get, WooCommerceEndpoints.Index, null); 
			var response = await ExecuteRequest (request);
			var result = await ProcessResponse<Store>(response);
			Version = result.wc_version;
			return result;
		}

		async Task<HttpResponseMessage> ExecuteRequest (HttpRequestMessage request)
		{
			var response = await _client.SendAsync (request);
			return response;
		}

		HttpRequestMessage PrepareRequest(HttpMethod httpMethod, string path, Dictionary<string,string> parameters)
		{
			if (parameters != null && parameters.Count > 0) {
				path += ToQueryString (parameters);
			}
			var request = new HttpRequestMessage (httpMethod,path);
			return request;
		}

		async Task<T> ProcessResponse<T>(HttpResponseMessage httpMessage)
		{
			Exception exception = null;
			if (httpMessage.IsSuccessStatusCode) {
				try {
					var strResult = await httpMessage.Content.ReadAsStringAsync (); 
					JObject obj = JObject.Parse(strResult);
					T data = default(T);
						if (obj != null)
					{
						var root = obj.First;
						if (root != null)
						{
							var rootJson = root.First;
							if (rootJson != null)
							{
								data = rootJson.ToObject<T>();
							}
						}
					}
					return data;
				} catch (Exception e) {
					exception = e;
				}
			}
			if (exception != null) {
				//TODO:handle this
			}
			return default(T);
		}

		static Dictionary<string, string> HandleFilters (WooCommerceFilterPeriod period, DateTime minDate, DateTime maxDate)
		{
			var parameters = new Dictionary<string, string> ();
			if (period != WooCommerceFilterPeriod.None) {
				parameters.Add (WooCommerceFilter.Period, period.ToString ().ToLower ());
			}
			else {
				if (minDate != default(DateTime)) {
					parameters.Add (WooCommerceFilter.MinDate, minDate.ToString ("YYYY-MM-dd"));
				}
				if (maxDate != default(DateTime)) {
					parameters.Add (WooCommerceFilter.MaxDate, maxDate.ToString ("YYYY-MM-dd"));
				}
			}
			return parameters;
		}

		string ToQueryString(Dictionary<string,string> parameters)
		{
			var array = new List<string> ();
			foreach (var parameter in parameters) {
				array.Add (string.Format ("{0}={1}",parameter.Key,parameter.Value));
			}
			return "?" + string.Join("&", array);
		}

		public string AccessToken {
			get;
			set;
		}

		public  string AppId {
			get;
			set;
		}

		public  string AppSecret {
			get;
			set;
		}

		public static string DefaultVersion
		{
			get;
			set;
		}

		public virtual string Version
		{
			get;
			set;
		}

		public string Currency
		{
			get;
			set;
		}
	}
}

