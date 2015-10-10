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

			//Sept 21. 15 the List view needs to be populated with existings tasks. 
			//users will have the option to view the tasks, and do actions to them when they click the 
			//specific task. 
			Core MainCore = Core.GetCore();
			Scheduler MainScheduler = MainCore.GetScheduler ();
            //MainScheduler.scheduleTimer();
			List<Task> TasksList = MainScheduler.GetActiveTasks ();

			//Info on ListView are in this reference
			//https://developer.xamarin.com/guides/cross-platform/xamarin-forms/user-interface/listview/data-and-databinding/
			listView = new ListView ();
			//below needs a DataTemplateType (typeOfScheduler probably)
			//listView.ItemTemplate = new DataTemplate(TasksList);
			//I'm replacing above with .ItemSource to Tasks
			listView.ItemsSource = TasksList;


			//On click of the item it pushs to a task page.
			listView.ItemSelected += (sender, e) => {
				// var selectedTask = xxx.SelectedItem();
				// create a new page with this task as the binding context
				// var TaskPage = new TaskPage();
				// Navigation.PushAsync(TaskPage);
			};

			var AddTask = new Button {
				Text = "Add Task"
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

