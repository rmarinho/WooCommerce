using System;
using System.Collections.Generic;

using Xamarin.Forms;
using ImageCircle.Forms.Plugin.Abstractions;

namespace WooCommerce
{
	public partial class MenuPage : ContentPage
	{

		public MenuViewModel ViewModel {
			get {
				return this.BindingContext as MenuViewModel;
			}
		}

		public MenuPage ()
		{
			InitializeComponent ();
			this.BindingContext = new MenuViewModel ();
			lstItems.ItemTemplate = new DataTemplate (typeof(MenuCell));
		}

		public class MenuCell : ViewCell
		{
			public MenuCell ()
			{
			
				var grid = new Grid ();
				grid.VerticalOptions = LayoutOptions.FillAndExpand;
				grid.ColumnSpacing = 1;
				grid.RowSpacing = 0;
				grid.Padding = new Thickness (1, 0, 0, 0);
				grid.ColumnDefinitions.Add (new ColumnDefinition { Width = new GridLength (5, GridUnitType.Absolute) });
				grid.ColumnDefinitions.Add (new ColumnDefinition { Width = new GridLength (0.8, GridUnitType.Star) });
				grid.RowDefinitions.Add (new RowDefinition { Height = new GridLength (1, GridUnitType.Star) });
				grid.RowDefinitions.Add (new RowDefinition { Height = new GridLength (1, GridUnitType.Absolute) });
				var box = new BoxView ();
				box.SetBinding (BoxView.ColorProperty, new Binding ("Color"));

				grid.Children.Add (box);
				Grid.SetColumn (box, 0);

				var stack = new StackLayout ();
				stack.Padding = new Thickness (10, 10, 0, 0);
				var stack1 = new StackLayout ();
				stack1.Padding = new Thickness (10, 10, 0, 0);
				stack.BackgroundColor = Color.FromHex ("#404040");
		
				var label = new Label ();
				label.SetBinding (Label.TextProperty, new Binding ("Title"));
				label.TextColor = Color.White;
				label.FontFamily = "HelveticaNeue-Light";
				label.VerticalOptions = LayoutOptions.Center;
				stack1.Children.Add (label);


				grid.Children.Add (stack);
				grid.Children.Add (stack1);
				Grid.SetColumn (stack, 1);
				Grid.SetColumn (stack1, 1);
				grid.BackgroundColor = Color.FromHex("060606");
				View = grid;
			}
		}
	}
}

