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
            AppConfig config = Core.GetCore().GetConfig();
            
            Title = "Add Task With ScrollView";
            var nameLabel = new Label { Text = "Task Name", Style = config.GenerateLabelStyle() };
            
            var nameEntry = new Entry {Placeholder = "New Task Name"};
            nameEntry.Style = config.GenerateEntryStyle();
            nameEntry.SetBinding (Entry.TextProperty, "Task Name");

            var noteLabel = new Label { Text = "Task Note", Style = config.GenerateLabelStyle() };
            var noteEntry = new Entry {Placeholder = "New Task Notes"};
            noteEntry.Style = config.GenerateEntryStyle();
            noteEntry.SetBinding (Entry.TextProperty, "Task Note");

            var doneLabel = new Label { Text = "Done", Style = config.GenerateLabelStyle() };
            var donePicker = new Picker{ Style = config.GeneratePickerStyle() };
            donePicker.Items.Add ("Yes");
            donePicker.Items.Add ("No");

            var reminderEndDateLabel = new Label{ Text = "Set End Date", Style = config.GenerateLabelStyle() };
            var reminderEndDatePicker = new DatePicker{ 
                Format= "D",
                Style = config.GeneratePickerStyle()
            };

            var ringToneLabel = new Label { Text = "Select Ringtone", Style = config.GenerateLabelStyle() };
            var ringTonePicker = new Picker{ Style = config.GeneratePickerStyle() };

            //This part we can create a dictionary object that contains all the
            //ringtones, then load it into this picker, until then we just include
            //these three. 
            ringTonePicker.Items.Add ("Crazy Frog");
            ringTonePicker.Items.Add ("Minions");
            ringTonePicker.Items.Add ("Flight of the Valkryie");

            var frequencyLabel = new Label { Text = "Select Frequency", Style = config.GenerateLabelStyle() };
            var frequencyPicker = new Picker { Style = config.GeneratePickerStyle() };

            for (int i = 1; i <= 10; i++) {
            
                frequencyPicker.Items.Add(i.ToString());
            };

            var frequencyUnitLabel = new Label { Text = "Select Unit", Style = config.GenerateLabelStyle() };
            var frequencyUnitPicker = new Picker{ Style = config.GeneratePickerStyle() };

            frequencyUnitPicker.Items.Add ("Minutes");
            frequencyUnitPicker.Items.Add ("Hours");
            frequencyUnitPicker.Items.Add ("Days");
            frequencyUnitPicker.Items.Add ("Weeks");

            var SaveButton = new Button {
                Text = "Save!",
                Style = config.GenerateButtonStyle()
            };

            SaveButton.Clicked += (sender, e) => {

                string tTaskName;
                string tTaskNotes;
                DateTime tReminderEndDate;
                bool tDone;
                string tRingToneName;
                string tFrequency;
                string tFrequencyUnit;

                //tTaskName= nameEntry.Text.ToString();
                //tTaskNotes = noteEntry.Text.ToString();
                //tReminderEndDate = reminderEndDatePicker.DateSelected;
                //tDone = donePicker.Items[donePicker.SelectedIndex];
                tRingToneName = ringTonePicker.Items[ringTonePicker.SelectedIndex];
                //tFrequency = frequencyPicker.Items[frequencyPicker.SelectedIndex];
                //tFrequencyUnit = frequencyUnitPicker.Items[donePicker.SelectedIndex];

                string Results;
                Results =  tRingToneName + " ";
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
