using System;
using Xamarin.Forms;

namespace WooCommerce
{
	public class FadeTriggerAction : TriggerAction<VisualElement>
	{
		public FadeTriggerAction() {}

		public int Delay { set; get; }

		public int StartsFrom { set; get; }

		public int To { set; get; }

		public Action Exectute { set; get; }

		protected override void Invoke (VisualElement visual)
		{
			if (visual.BindingContext == null)
				return;

			visual.Opacity = StartsFrom;
			visual.FadeTo (To,(uint)Delay, Easing.Linear);

		}
	}

}

