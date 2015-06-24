using System;
using SQLite;

namespace WooCommerce
{
	public interface ISQLite {
		SQLiteConnection GetConnection();
	}
}

