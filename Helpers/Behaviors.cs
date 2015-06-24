using System;
using Xamarin.Forms;

namespace WooCommerce
{
	public class FadeAnimationBehavior : Behavior<VisualElement>
	{
		protected override async void OnAttachedTo(VisualElement element)
		{
			element.Opacity = 0;
			await element.FadeTo (1,1000);
			base.OnAttachedTo(element);
		}

		protected override void OnDetachingFrom(VisualElement element)
		{
			element.Opacity = 1;
			base.OnDetachingFrom(element);
		}

	}
}

