using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;

//Add Task viewer
//Sept 4th 2015
//Controls are listed in this document below
//https://developer.xamarin.com/guides/cross-platform/xamarin-forms/controls/views/
namespace ScheduleApp
{
	public class pageAddTask:ContentPage
	{
		public pageAddTask ()
		{
			Title = "Add Task";
			var nameLabel = new Label { Text = "Task Name" };
			var nameEntry = new Entry ();
			nameEntry.SetBinding (Entry.TextProperty, "Task Name");

			var noteLabel = new Label { Text = "Task Note" };
			var noteEntry = new Entry ();
			nameEntry.SetBinding (Entry.TextProperty, "Task Note");

			var doneLabel = new Label { Text = "Done" };
			var donePicker = new Picker{};
			donePicker.Items.Add ("Yes");
			donePicker.Items.Add ("No");

			var reminderEndDateLabel = new Label{ Text = "Set End Date" };
			var reminderEndDatePicker = new DatePicker{ 
				Format= "D",
			};

			Content = new StackLayout {
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Padding = new Thickness (20),
				Children = {
					nameLabel,
					nameEntry,
					noteLabel,
					noteEntry,
					doneLabel,
					donePicker,
					reminderEndDateLabel,
					reminderEndDatePicker
				}
			};
		}
	}
}

