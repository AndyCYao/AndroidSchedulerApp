using System;

using Xamarin.Forms;

namespace ScheduleApp.Droid
{
	public class Task : ContentPage
	{
		public Task ()
		{
			Content = new StackLayout { 
				Children = {
					new Label { Text = "Hello ContentPage" }
				}
			};
		}
	}
}


