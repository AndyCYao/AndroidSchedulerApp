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

//Sept 26th 2015
//https://developer.xamarin.com/api/type/Xamarin.Forms.ScrollView/
//will use this to implement a scrolling action. 
//may have to put a table layout instead
//http://developer.xamarin.com/guides/cross-platform/xamarin-forms/user-interface/tableview/
namespace ScheduleApp
{
	public class pageAddTask:ContentPage
	{
		public pageAddTask ()
		{
			DateTime tempReminderEndDate;

			Title = "Add Task With ScrollView";
            var nameLabel = new Label { Text = "Task Name" };
			var nameEntry = new Entry {Placeholder = "New Task Name"};
			nameEntry.SetBinding (Entry.TextProperty, "Task Name");

			var noteLabel = new Label { Text = "Task Note" };
			var noteEntry = new Entry {Placeholder = "New Task Notes"};
			noteEntry.SetBinding (Entry.TextProperty, "Task Note");

			var doneLabel = new Label { Text = "Done" };
			var donePicker = new Picker{};
			donePicker.Items.Add ("Yes");
			donePicker.Items.Add ("No");

			var reminderEndDateLabel = new Label{ Text = "Set End Date" };
			var reminderEndDatePicker = new DatePicker{ 
				Format= "D",

			};

			var ringToneLabel = new Label { Text = "Select Ringtone" };
			var ringTonePicker = new Picker{ };

			//This part we can create a dictionary object that contains all the
			//ringtones, then load it into this picker, until then we just include
			//these three. 
			ringTonePicker.Items.Add ("Crazy Frog");
			ringTonePicker.Items.Add ("Minions");
			ringTonePicker.Items.Add ("Flight of the Valkryie");

			var frequencyLabel = new Label { Text = "Select Frequency" };
			var frequencyPicker = new Picker { };

			for (int i = 1; i <= 10; i++) {
			
				frequencyPicker.Items.Add(i.ToString());
			};

			var frequencyUnitLabel = new Label { Text = "Select Unit" };
			var frequencyUnitPicker = new Picker{ };

			frequencyUnitPicker.Items.Add ("Minutes");
			frequencyUnitPicker.Items.Add ("Hours");
			frequencyUnitPicker.Items.Add ("Days");
			frequencyUnitPicker.Items.Add ("Weeks");

			var SaveButton = new Button {
				Text = "Save!"
			};
			SaveButton.Clicked += (sender, e) => {

				string tTaskName;
				string tTaskNotes;
				DateTime tReminderEndDate;
				bool tDone;
				string tRingToneName;
				int tFrequency;
				string tFrequencyUnit;

				tTaskName= nameEntry.Text.ToString();
				tTaskNotes = noteEntry.Text.ToString();
				//tReminderEndDate = reminderEndDatePicker.DateSelected;
				//tDone = donePicker.Items[donePicker.SelectedIndex];
				tRingToneName = ringTonePicker.Items(ringTonePicker.SelectedIndex);
				tFrequency = frequencyPicker.GetValue();
				tFrequencyUnit = frequencyUnitPicker.Items[donePicker.SelectedIndex()];

				string Results;
				Results = tTaskName + " " +  tTaskNotes;
				DisplayAlert("Hello, Testing",Results,"Ok");
				//Push the information presented into Scheduler Class. 
				//Scheduler SchAdd = new Scheduler();
				//SchAdd.AddTaskWithInfo();	
			};

			ScrollView scrollView = new ScrollView {
				VerticalOptions = LayoutOptions.FillAndExpand,
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
						ringToneLabel,
						ringTonePicker,
						frequencyLabel,
						frequencyPicker,
						frequencyUnitLabel,
						frequencyUnitPicker,
						reminderEndDateLabel,
						reminderEndDatePicker,
						SaveButton
					}
				}
			};
			this.Content = scrollView;
	}
	}
};
