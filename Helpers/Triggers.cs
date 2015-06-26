using System;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace WooCommerce
{
	public class FadeTriggerAction : TriggerAction<VisualElement>
	{
		int defaultDuration = 300;
	
		public FadeTriggerAction() {}

		public int Delay { set; get; }

		public int Duration { set; get; }

		public int From { set; get; }

		public int To { set; get; }

		protected override async void Invoke (VisualElement sender)
		{
			if (sender.BindingContext == null)
				return;

			var delaySender = FadeTrigger.GetDelay (sender);
			if (delaySender != -1)
				Delay = delaySender;

			var durationSender = FadeTrigger.GetDuration (sender);
			if (durationSender != -1)
				Duration = durationSender;


			if (Duration == 0)
				Duration = defaultDuration;

			sender.Opacity = From;
			await Task.Delay(Delay);
			await sender.FadeTo (To,(uint)Duration, Easing.Linear);
		}

	}

	public static class FadeTrigger {

		public static readonly BindableProperty DelayProperty = 
			BindableProperty.CreateAttached<VisualElement, int> (bindable => GetDelay (bindable), -1);

		public static int GetDelay (BindableObject bindable)
		{
			return (int)bindable.GetValue (DelayProperty);
		}

		public static void SetDelay (BindableObject bindable, int value)
		{
			bindable.SetValue (DelayProperty, value);
		}


		public static readonly BindableProperty DurationProperty = 
			BindableProperty.CreateAttached<VisualElement, int> (bindable => GetDelay (bindable), -1);

		public static int GetDuration (BindableObject bindable)
		{
			return (int)bindable.GetValue (DurationProperty);
		}

		public static void SetDuration (BindableObject bindable, int value)
		{
			bindable.SetValue (DurationProperty, value);
		}
	}

}

