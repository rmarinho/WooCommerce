using System;
using Xamarin.Forms;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using WooCommerce.Services;

namespace WooCommerce
{
	public class Database : ICacheService
	{
		SQLiteConnection database;

		public Database() {
			database = DependencyService.Get<ISQLite> ().GetConnection ();
			database.CreateTable<CacheItem>();
		}

		public IEnumerable<CacheItem> GetItems () {
			return (from i in database.Table<CacheItem>() select i).ToList();
		}
		public IEnumerable<CacheItem> GetItemsNotDone ()
		{
			return database.Query<CacheItem>("SELECT * FROM [CacheItem] WHERE [DateTime] NOT NULL");
		}
		public CacheItem GetItem (string id)
		{
			return database.Table<CacheItem>().FirstOrDefault(x => x.Id == id);
		}
		public int DeleteItem(string id)
		{
			return database.Delete<CacheItem>(id);
		}

		public int AddItem(string key, string value)
		{
			var cacheItem = new CacheItem ();
			cacheItem.Stamp = DateTime.UtcNow;
			cacheItem.Id = key;
			cacheItem.Value = value;
			return database.Insert (cacheItem);
		}

		#region ICacheService implementation

		public string GetFromCache (string endpoint)
		{
			var item = GetItem (endpoint);
			return item != null ? item.Value : "";
		}

		public void SetToCache (string endpoint, string value)
		{
			AddItem (endpoint, value);
		}

		public void ClearCache (bool clearProducts = true)
		{
			database.Table<CacheItem> ().Delete (c => true);
		}

		#endregion

		public class CacheItem
		{
			[PrimaryKey]
			public string Id { get; set;}
			public string Value { get; set;}
			public DateTime Stamp { get; set;}
		}
	}
}

