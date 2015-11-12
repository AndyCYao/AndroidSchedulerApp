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

		protected override void OnAppearing()
		{
			base.OnAppearing();

			Style = Core.GetCore().GetConfig().GeneratePageStyle();
		}

		public pageAddTask()
		{
			AppConfig config = Core.GetCore ().GetConfig ();

			Title = "Add Task With ScrollView";
			var nameLabel = new Label { Text = "Task Name", Style = config.GenerateLabelStyle () };

			var nameEntry = new Entry { Placeholder = "New Task Name" };
			nameEntry.Style = config.GenerateEntryStyle ();
			nameEntry.SetBinding (Entry.TextProperty, "Task Name");

			var noteLabel = new Label { Text = "Task Note", Style = config.GenerateLabelStyle () };
			var noteEntry = new Entry { Placeholder = "New Task Notes" };
			noteEntry.Style = config.GenerateEntryStyle ();
			noteEntry.SetBinding (Entry.TextProperty, "Task Note");

			var doneLabel = new Label { Text = "Done", Style = config.GenerateLabelStyle () };
			var donePicker = new Picker{ Style = config.GeneratePickerStyle () };
			donePicker.Items.Add ("False");
            donePicker.Items.Add("True");
            donePicker.SelectedIndex = 0;

            var reminderBeginDateLabel = new Label { Text = "Set Begin Date", Style = config.GenerateLabelStyle() };
            var reminderBeginDatePicker = new DatePicker
            {
                Format = "D",
                Style = config.GeneratePickerStyle()
            };

            var reminderEndDateLabel = new Label{ Text = "Set End Date", Style = config.GenerateLabelStyle () };
			var reminderEndDatePicker = new DatePicker { 
				Format = "D",
				Style = config.GeneratePickerStyle ()
			};

			var ringToneLabel = new Label { Text = "Select Ringtone", Style = config.GenerateLabelStyle () };
			var ringTonePicker = new Picker{ Style = config.GeneratePickerStyle () };

			//This part we can create a dictionary object that contains all the
			//ringtones, then load it into this picker, until then we just include
			//these three. 
			ringTonePicker.Items.Add ("Crazy Frog");
			ringTonePicker.Items.Add ("Minions");
			ringTonePicker.Items.Add ("Flight of the Valkryie");

			//This is subject to change, need to speak to Peter
			//on how the default ringtone in the config page gets updated here
			ringTonePicker.SelectedIndex = 0;

			var frequencyLabel = new Label { Text = "Select Frequency", Style = config.GenerateLabelStyle () };
			var frequencyPicker = new Picker { Style = config.GeneratePickerStyle () };

			for (int i = 1; i <= 10; i++) {

				frequencyPicker.Items.Add (i.ToString ());
			}
			;
			frequencyPicker.SelectedIndex = 5;

			var frequencyUnitLabel = new Label { Text = "Select Unit", Style = config.GenerateLabelStyle () };
			var frequencyUnitPicker = new Picker{ Style = config.GeneratePickerStyle () };

			frequencyUnitPicker.Items.Add ("Minutes");
			frequencyUnitPicker.Items.Add ("Hours");
			frequencyUnitPicker.Items.Add ("Days");
			frequencyUnitPicker.Items.Add ("Weeks");

			frequencyUnitPicker.SelectedIndex = 2;

			var SaveButton = new Button {
				Text = "Save!",
				Style = config.GenerateButtonStyle ()
			};

			SaveButton.Clicked += (sender, e) => {

				string tTaskName;
				string tTaskNotes;
                DateTime tReminderBeginDate;
                DateTime tReminderEndDate;
				bool tDone;
				string tRingToneName;
				int tFrequency;
				string tFrequencyUnit;

				if (nameEntry.Text != null && noteEntry.Text != null){ 
					tTaskName = nameEntry.Text.ToString ();
					tTaskNotes = noteEntry.Text.ToString ();
                    tReminderBeginDate = reminderBeginDatePicker.Date;
					tReminderEndDate = reminderEndDatePicker.Date;
					tDone = Convert.ToBoolean (donePicker.Items [donePicker.SelectedIndex]);
					tRingToneName = ringTonePicker.Items [ringTonePicker.SelectedIndex];
					tFrequency = Convert.ToInt32(frequencyPicker.Items [frequencyPicker.SelectedIndex]);
					tFrequencyUnit = frequencyUnitPicker.Items [frequencyUnitPicker.SelectedIndex];

					//string Results;
					//Results = tReminderEndDate + " " + tTaskName + " " + tTaskNotes + " " + tDone + " " + tRingToneName + " "  + tFrequency + " " + tFrequencyUnit ;
					//DisplayAlert ("Hello, Testing", Results, "Ok");

				//Push the information presented into Scheduler Class. 
					Scheduler SchAdd = new Scheduler();
					SchAdd.AddTaskWithInfo(tTaskName, tTaskNotes, tReminderBeginDate, tReminderEndDate, tRingToneName, tFrequency, tFrequencyUnit);    				
				}
				else{
				  DisplayAlert("But wait!","Please write a little about your Task name and task notes","Ok");
				};
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
                        reminderBeginDateLabel,
                        reminderBeginDatePicker,
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
	};
}