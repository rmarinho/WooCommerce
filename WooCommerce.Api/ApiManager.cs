﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WooCommerce.Api
{

	public class WooCommerceEndpoints
	{
		public static string Index = "wc-api/v2";
	}

	public class WooCommerceClient
	{
		bool _isSecureConnection;
		string _version;
		string _appUrl;
		static string _defaultVersion;
		HttpClient _client;

		public WooCommerceClient()
		{
			_version = _defaultVersion;
			_appUrl = "http://xamstore.azurewebsites.net/";
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
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(String.Format("{0}:{1}", appId, appSecret))));

		}

		public async Task<Store> GetStoreInfo()
		{
			var request = PrepareRequest (HttpMethod.Get, WooCommerceEndpoints.Index, null); 
			var response = await ExecuteRequest (request);
			var result = await ProcessResponse<Store>(response);
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
			get { return _defaultVersion; }
			set { _defaultVersion = value; }
		}

		public virtual string Version
		{
			get { return _version; }
			set { _version = value; }
		}

	}
}
