using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;
using PCLStorage;
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

            //Feb 18 2016 - below is doc. for listview itemsource
            //Goal this week is to 
            //- add a refresh pull down function.
            //  best to read this first
            //http://motzcod.es/post/87917979362/pull-to-refresh-for-xamarinforms-ios
            //- Swipe left to delete, 
            //-DONE  Click to enter and pass the ID to the Edit 

            //List<AppTask> TasksList = MainScheduler.GetTasks(false);


            //Method 1
            //listView.ItemTemplate = new DataTemplate(typeof(AppTask));
            //listView.ItemsSource = TasksList;
            System.Collections.ObjectModel.ObservableCollection<AppTask> oTasksList = new System.Collections.ObjectModel.ObservableCollection<AppTask>(MainScheduler.GetTasks(false));


            listView = new ListView ();
            listView.ItemsSource = from x in oTasksList select x.TaskName;

            //Method 2 - this works but i want to bind the object instead
            //listView.ItemsSource = from x in TasksList select x.TaskName;


            //On click of the item it pushs to a task page.
            listView.ItemSelected += (sender, e) => {
                foreach(var Tasks in oTasksList)
                {
                   if (Tasks.TaskName == listView.SelectedItem.ToString()){
                        //DisplayAlert("Check Check", Tasks.TaskID.ToString(), "Ok");
                        Navigation.PushAsync(new pageViewTask(Tasks.TaskID.ToString()));
                    }
                } 
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

