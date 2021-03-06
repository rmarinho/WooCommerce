﻿using System;
using System.Collections.ObjectModel;
using WooCommerce.Api;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WooCommerce
{
	public class ProductsViewModel : BaseViewModel
	{
		public ProductsViewModel ()
		{
			GetData ();
		}

		public async Task GetData()
		{
			IsBusy = true;
			var productsService = await App.Client.GetProducts ();
			Products.Clear ();
			foreach (var item in productsService) {
				Products.Add (item);
			}
			IsBusy = false;
		}

		Product selectedProduct = null;
		public Product SelectedProduct {
			get{ return selectedProduct; }
			set{ 
				if(selectedProduct != value) {
					SetProperty (ref selectedProduct, value);
					var detailPage = new ProductDetailPage();
					detailPage.ViewModel.Product = selectedProduct;
					App.Navigation.PushAsync (detailPage);
				}
			}
		}

		ObservableCollection<Product> products = new ObservableCollection<Product>();
		public ObservableCollection<Product> Products {
			get{ return products; }
			set{ SetProperty (ref products, value); }
		}

		public string PageName {
			get {
				return "Products";
			}
		}
	}
}

