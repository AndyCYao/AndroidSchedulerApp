using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;
using PCLStorage;
//Created by Andy- Sept 4th 2015
//main page that will greet the user.
namespace ScheduleApp
{

	// The root page of your application
	public class Main: ContentPage
	{
		ListView listView; //This is to list all the current tasks in our XML memory.
        Button ConfigButton, AddTask;

        public Main(){
			Title = "Scheduler App 2015";
            
			//Sept 21. 15 the List view needs to be populated with existings tasks. 
			//users will have the option to view the tasks, and do actions to them when they click the 
			//specific task. 
			Core MainCore = Core.GetCore();
			Scheduler MainScheduler = MainCore.GetScheduler();

            //Feb 13 2016 - below is doc. for listview itemsource
 
            List<AppTask> TasksList = MainScheduler.GetTasks(false);
            //System.Collections.ObjectModel.ObservableCollection<AppTask> oTasksList = new System.Collections.ObjectModel.ObservableCollection<TasksList>;
            //Info on ListView are in this reference
            //https://developer.xamarin.com/guides/cross-platform/xamarin-forms/user-interface/listview/data-and-databinding/
            listView = new ListView ();
		   
            //Method 1
            /*
            foreach(var Tasks in TasksList)
            {
                listView.ItemsSource.Add(Tasks.TaskName);
                    (Tasks.TaskName());
            }
            */

            //Method 2
            listView.ItemsSource = from x in TasksList select x.TaskName;

            //suppose to get the items from an observable collection .
			//listView.ItemsSource = TasksList;
    


			//On click of the item it pushs to a task page.
			listView.ItemSelected += (sender, e) => {
				// var selectedTask = xxx.SelectedItem();
				// create a new page with this task as the binding context
				// var TaskPage = new TaskPage();
				// Navigation.PushAsync(TaskPage);
			};


            AddTask = new Button {
                Text = "Add Task",
                Style = MainCore.GetConfig().GenerateButtonStyle()
			};
			AddTask.Clicked += (sender, e) => {
				var pAddTask = new pageAddTask();
				this.Navigation.PushAsync(pAddTask);
			};
			ConfigButton = new Button { Text = "Configuration", Style = MainCore.GetConfig().GenerateButtonStyle() };
            ConfigButton.Clicked += (sender, e) => {
                var pAppSettings = new pageAppConfig();
                //				var todoItem = (TodoItem)BindingContext;
                //				this.Navigation.PopAsync();
                this.Navigation.PushAsync(pAppSettings);
            };

				
			Content = new StackLayout {
				VerticalOptions = LayoutOptions.Center,
				Children = {
                    //pathLabel,
                    listView,
  					AddTask,
                    ConfigButton
                }
			};
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            AppConfig appConfig = Core.GetCore().GetConfig();

            Style = appConfig.GeneratePageStyle();
            AddTask.Style = appConfig.GenerateButtonStyle(); 
            ConfigButton.Style = appConfig.GenerateButtonStyle();
        }
    };
}

