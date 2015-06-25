﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace WooCommerce
{
	public partial class MainPage : ContentPage
	{
		public MainViewModel ViewModel {
			get{
				return this.BindingContext as MainViewModel;
			}
		}

		public MainPage ()
		{
			NavigationPage.SetBackButtonTitle (this, "");
			InitializeComponent ();
			this.BindingContext = new MainViewModel ();
		}

	}
}

