using System;

namespace WooCommerce
{
	public static class Extensions
	{
		public static T ParseEnum<T>( string value )
		{
			return (T) Enum.Parse( typeof( T ), value, true );
		}
	}


}

