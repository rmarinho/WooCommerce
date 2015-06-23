using System;

using Xamarin.Forms;

namespace WooCommerce.Api
{
	public interface ICacheService 
	{
		string GetFromCache (string endpoint);
		void SetToCache (string endpoint, string value);
		void ClearCache (bool clearProducts = true);
	}
}


