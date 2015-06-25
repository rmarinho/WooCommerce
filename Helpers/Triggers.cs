using System;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace WooCommerce
{
	public class FadeTriggerAction : TriggerAction<VisualElement>
	{
		public FadeTriggerAction() {}

		public int Delay { set; get; }

		public int Duration { set; get; }

		public int StartsFrom { set; get; }

		public int To { set; get; }

		public Action Exectute { set; get; }

		protected override async void Invoke (VisualElement sender)
		{
			if (sender.BindingContext == null)
				return;

			sender.Opacity = StartsFrom;
			await Task.Delay(Delay);
			await sender.FadeTo (To,(uint)Duration, Easing.Linear);
		}
	}

}

