using System;
using Xamarin.Forms;
using WooCommerce.Droid;
using System.IO;

[assembly: Dependency (typeof (SQLite_Android))]

namespace WooCommerce.Droid
{
	public class SQLite_Android : ISQLite {
		public SQLite_Android () {}
//		public SQLiteConnection GetConnection () {
//			var sqliteFilename = "WooCommerce.db3";
//			string documentsPath = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal); // Documents folder
//			var path = Path.Combine(documentsPath, sqliteFilename);
//			// Create the connection
//			var conn = new SQLite.SQLiteConnection(path);
//			// Return the database connection
//			return conn;
//		}}

		#region ISQLite implementation

		public SQLite.SQLiteConnection GetConnection ()
		{
			var sqliteFilename = "WooCommerce.db3";
			string documentsPath = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal); // Documents folder
			var path = Path.Combine(documentsPath, sqliteFilename);
			// Create the connection
			var conn = new SQLite.SQLiteConnection(path);
			// Return the database connection
			return conn;
		}

		#endregion
	}
}

