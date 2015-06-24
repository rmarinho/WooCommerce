using System;
using Xamarin.Forms;

namespace WooCommerce
{
	public class FadeTriggerAction : TriggerAction<VisualElement>
	{
		public FadeTriggerAction() {}

		public int StartsFrom { set; get; }

		public Action Exectute { set; get; }

		protected override void Invoke (VisualElement visual)
		{
			if (Exectute != null) {
				Exectute.Invoke ();
			}
		}
	}

}

