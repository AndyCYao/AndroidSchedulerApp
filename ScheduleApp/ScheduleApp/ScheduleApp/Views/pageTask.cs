using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;

using ScheduleApp;

namespace ScheduleApp
{
	public class pageTask:ContentPage
	{
        protected override void OnAppearing()
		{
			base.OnAppearing();

			Style = Core.GetCore().GetConfig().GeneratePageStyle();
		}

		public pageTask(AppTask existingTask = null)
		{
			AppConfig config = Core.GetCore ().GetConfig ();
            
            Title = "Add Task";
			var nameLabel = new Label { Text = "Task Name", Style = config.GenerateLabelStyle () };

            Entry nameEntry = new Entry { Placeholder = "New Task Name" };
			nameEntry.Style = config.GenerateEntryStyle ();
			
			var noteLabel = new Label { Text = "Task Note", Style = config.GenerateLabelStyle () };
            Entry noteEntry = new Entry { Placeholder = "New Task Notes" };
			noteEntry.Style = config.GenerateEntryStyle ();
            
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

			Button ringTonePickerBtn = new Button
            {
                Text = "Select RingTone",
            };

            ringTonePickerBtn.Clicked += (sender, args) =>
            {
                DependencyService.Get<iRingTones>().GetRingTonePicker(existingTask);
            };

            var frequencyLabel = new Label { Text = "Select Frequency", Style = config.GenerateLabelStyle () };
			var frequencyPicker = new Picker { Style = config.GeneratePickerStyle () };

			for (int i = 1; i <= 10; i++)
            {
				frequencyPicker.Items.Add (i.ToString ());
			}
			
			var frequencyUnitLabel = new Label { Text = "Select Unit", Style = config.GenerateLabelStyle () };
			var frequencyUnitPicker = new Picker{ Style = config.GeneratePickerStyle () };

			frequencyUnitPicker.Items.Add ("Minutes");
			frequencyUnitPicker.Items.Add ("Hours");
			frequencyUnitPicker.Items.Add ("Days");
			frequencyUnitPicker.Items.Add ("Weeks");

            Button deleteButton = new Button
            {
                Text = "Delete",
                Style = config.GenerateButtonStyle()
            };

            deleteButton.IsVisible = false;

            deleteButton.Clicked += async (sender, e) =>
            {
                bool willDelete = await DisplayAlert("Delete Confirmation", "Are you sure you want to remove this task?", "Yes", "No");

                if (willDelete)
                {
                    Core.GetCore().GetScheduler().RemoveTask(existingTask.TaskID);
                    await Navigation.PopAsync();
                }
            };

            if (existingTask == null)
            {
                frequencyPicker.SelectedIndex = 5;
                frequencyUnitPicker.SelectedIndex = 2;
            }
            else
            {
                nameEntry.Text = existingTask.TaskName;
                noteEntry.Text = existingTask.TaskNotes;

                if (existingTask.Done)
                {
                    donePicker.SelectedIndex = 1;
                }
                else
                {
                    donePicker.SelectedIndex = 0;
                }

                reminderBeginDatePicker.Date = existingTask.ReminderBegin;
                reminderEndDatePicker.Date = existingTask.ReminderEnd;
                
                for (int i = 0; i < frequencyPicker.Items.Count; i++)
                {
                    if (existingTask.Frequency.ToString() == frequencyPicker.Items[i])
                    {
                        frequencyPicker.SelectedIndex = i;
                        break;
                    }
                }

                for (int i = 0; i < frequencyUnitPicker.Items.Count; i++)
                {
                    if (existingTask.FrequencyUnit == frequencyUnitPicker.Items[i])
                    {
                        frequencyUnitPicker.SelectedIndex = i;
                        break;
                    }
                }

                deleteButton.IsVisible = true;
            }

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
				int tFrequency;
				string tFrequencyUnit;
                string tNotificationSound;

				if (nameEntry.Text != null && noteEntry.Text != null){ 
					tTaskName = nameEntry.Text.ToString ();
					tTaskNotes = noteEntry.Text.ToString ();
                    tReminderBeginDate = reminderBeginDatePicker.Date;
					tReminderEndDate = reminderEndDatePicker.Date;
					tDone = Convert.ToBoolean (donePicker.Items [donePicker.SelectedIndex]);
                    tFrequency = Convert.ToInt32(frequencyPicker.Items[frequencyPicker.SelectedIndex]);
					tFrequencyUnit = frequencyUnitPicker.Items[frequencyUnitPicker.SelectedIndex];
                    tNotificationSound = DependencyService.Get<iRingTones>().GetSelectedRingTone();

                    if (existingTask == null)
                    {
                        Core.GetCore().GetScheduler().AddTaskWithInfo(
                        tTaskName, tTaskNotes, tReminderBeginDate,
                        tReminderEndDate, tNotificationSound, tFrequency, tFrequencyUnit);
                    }
                    else
                    {
                        Core.GetCore().GetScheduler().UpdateTaskWithInfo(
                            existingTask.TaskID, tTaskName, tTaskNotes,
                            tReminderBeginDate, tReminderEndDate, tNotificationSound,
                            tFrequency, tFrequencyUnit);
                    }

                    Navigation.PopAsync();
                }
				else{
				  DisplayAlert("But wait!","Please write a little about your Task name and task notes.","Ok");
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
						ringTonePickerBtn,
                        reminderBeginDateLabel,
                        reminderBeginDatePicker,
                        frequencyLabel,
						frequencyPicker,
						frequencyUnitLabel,
						frequencyUnitPicker,
						reminderEndDateLabel,
						reminderEndDatePicker,
						SaveButton,
                        deleteButton
					}
				}
			};
            
			this.Content = scrollView;
		}
	};
}