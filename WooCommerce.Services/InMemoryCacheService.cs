using System;
using System.Collections.Generic;

namespace WooCommerce.Services
{
	public class InMemoryCacheService : ICacheService
	{
		Dictionary<string,string> store;

		public InMemoryCacheService ()
		{
			store = new Dictionary<string, string> ();
		}

		public string GetFromCache (string endpoint)
		{
			string key = string.Format ("{0}-{1}", endpoint, DateTime.UtcNow.Date.ToString("dd-MM-yyyy"));
			if (store != null && store.ContainsKey (key))
				return store [key];
			return null;
		}

		public void SetToCache (string endpoint, string value)
		{
			string key = string.Format ("{0}-{1}", endpoint,  DateTime.UtcNow.Date.ToString("dd-MM-yyyy"));
			if (store != null) {
				if (store.ContainsKey (key))
					store [key] = value;
				else
					store.Add (key, value);
			}

		}

		public void ClearCache (bool clearProducts = true)
		{
			if (clearProducts)
				store.Clear ();
		}

	}
}

