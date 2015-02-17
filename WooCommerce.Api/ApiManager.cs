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

	public static class WooCommerceEndpoints
	{
		public static string Index = "wc-api/v2";
		public static string Products = "wc-api/v2/products";
		public static string Orders = "wc-api/v2/orders";
		public static string OrdersCount = "wc-api/v2/orders/count";
		public static string Reports = "wc-api/v2/reports";
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


		public async Task<List<Product>> GetReports()
		{
			var request = PrepareRequest (HttpMethod.Get, WooCommerceEndpoints.Reports, null); 
			var response = await ExecuteRequest (request);
			var result = await ProcessResponse<List<Product>>(response);
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

		HttpRequestMessage PrepareRequest(HttpMethod httpMethod, string path, object parameters)
		{
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

	}
}

