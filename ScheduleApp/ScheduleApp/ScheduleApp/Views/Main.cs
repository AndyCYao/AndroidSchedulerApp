using System;
//Created by Andy- Sept 4th 2015
//main page that will greet the user.
namespace ScheduleApp
{
	// The root page of your application
	public class Main: ContentPage
	{
		public Main(){
			Title = "Main";

			Button AddTask = new Button {
				Text = "Add Task",
				Font = Font.SystemFontOfSize (NamedSize.Large),
				BorderWidth = 1,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};
				
			var Content = new StackLayout ();
			{
				VerticalOptions = LayoutOptions.Center,
				Children = {
					AddTask
				}
			}
		}
	};
}

