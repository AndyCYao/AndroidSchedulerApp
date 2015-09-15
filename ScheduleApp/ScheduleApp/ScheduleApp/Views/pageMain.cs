using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;

//Created by Andy- Sept 4th 2015
//main page that will greet the user.
namespace ScheduleApp
{
	// The root page of your application
	public class Main: ContentPage
	{
		ListView listView; //This is to list all the current tasks in our XML memory.
		public Main(){
			Title = "Scheduler App 2015";
			listView = new ListView ();
			//below needs a DataTemplateType (typeOfScheduler probably)
			listView.ItemTemplate = new DataTemplate ();

			//On click of the item it pushs to a task page.
			listView.ItemSelected += (sender, e) => {
				// var selectedTask = xxx.SelectedItem();
				// create a new page with this task as the binding context
				// var TaskPage = new TaskPage();
				// Navigation.PushAsync(TaskPage);
			};

			var AddTask = new Button {
				Text = "Add Task"
				//Font = Font.SystemFontOfSize (NamedSize.Large),
				//BorderWidth = 1,
				//HorizontalOptions = LayoutOptions.Center,
				//VerticalOptions = LayoutOptions.CenterAndExpand
			};
			AddTask.Clicked += (sender, e) => {
				var pAddTask = new pageAddTask();
				this.Navigation.PushAsync(pAddTask);
			};
			var ConfigButton = new Button { Text = "Configuration" };
            ConfigButton.Clicked += (sender, e) => {
                var pAppSettings = new pageAppConfig();
                //				var todoItem = (TodoItem)BindingContext;
                //				this.Navigation.PopAsync();
                this.Navigation.PushAsync(pAppSettings);
            };

				
			Content = new StackLayout {
				VerticalOptions = LayoutOptions.Center,
				Children = {
					AddTask,
                    ConfigButton
                }
			};
		}
	};
}

