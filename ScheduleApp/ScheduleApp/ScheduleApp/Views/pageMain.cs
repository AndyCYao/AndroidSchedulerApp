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
        //Mar 1st 2016
        //to be fleshed out in the refresh list view object. 
        //Read through this git https://github.com/mhalkovitch/Xamarim/blob/dc595559c24c649135ccdc21b210f94aa2559634/Chapter%205%20-%20Lists/ListViewExample/ListViewExample/ListViewExample/HomePage.cs
        //basically, the context action needs a viewCell 
        //1.) the refreshListViewSource will now need to update the ListItem
        //2.) the ListView calls the ListItems as the source
        //3.) the ListItemCells fleshes out the ListItems with the actual
        // context action details, like more / details etc. 

            /*
        public class ListItem
        {
            public string Task_Name { get; set; }
        }
        */

        class ListItemCell: ViewCell{
            public ListItemCell()
            {
                var moreAction = new MenuItem { Text = "More" };
                moreAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
                moreAction.Clicked += (sender, e) =>
                {
                    var mi = ((MenuItem)sender);
                    var item = (AppTask)mi.CommandParameter;
                    //((ContentPage)((ListView)viewLayout.ParentView).ParentView).DisplayAlert("More Clicked", "On row: " + item.Title.ToString(), "OK");             
                };
                var deleteAction = new MenuItem { Text = "Delete", IsDestructive = true };
                deleteAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
                deleteAction.Clicked += (sender, e) =>
                {
                    var mi = ((MenuItem)sender);
                    var item = (AppTask)mi.CommandParameter;
                    //((ContentPage)((ListView)viewLayout.ParentView).ParentView).DisplayAlert("Delete Clicked", "On row: " + item.Title.ToString(), "OK");
                };
                ContextActions.Add(moreAction);
                ContextActions.Add(deleteAction);
                Label titleLabel = new Label { Text = "Task Name" };
                titleLabel.SetBinding(Label.TextProperty, "TaskName");

                StackLayout viewLayoutItem = new StackLayout()
                {
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    Orientation = StackOrientation.Vertical,
                    Children = { titleLabel }

                };

                View = viewLayoutItem;

            }
        }

        public void RefreshListViewSource()
        {
            Core MainCore = Core.GetCore();
            Scheduler MainScheduler = MainCore.GetScheduler();
            //System.Collections.ObjectModel.ObservableCollection<AppTask> oTasksList = new System.Collections.ObjectModel.ObservableCollection<AppTask>(MainScheduler.GetTasks(false));
            //listView.ItemsSource = from x in oTasksList select x.TaskName;

            var TasksLists = MainScheduler.GetTasks(false);
            listView.ItemTemplate = new DataTemplate(typeof(ListItemCell)); //This line is causing the code to crash. 
            listView.ItemsSource = TasksLists;
        }

        ListView listView; //This is to list all the current tasks in our XML memory.
        Button ConfigButton, AddTask;
        //System.Collections.ObjectModel.ObservableCollection<AppTask> oTasksList;
        AppTask TasksLists;

        public Main(){
			Title = "Scheduler App 2016";
            
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

            listView = new ListView ();
      

            //RefreshListViewSource(); // since appears in OnAppearing dont need this anymore. 


            //On click of the item it pushs to a task page.
            /*
            listView.ItemSelected += (sender, e) => {
                foreach(var Tasks in oTasksList)
                {
                   if (Tasks.TaskName == listView.SelectedItem.ToString()){
                        //DisplayAlert("Check Check", Tasks.TaskID.ToString(), "Ok");
                        //Feb 27th 2016 - > Give user an option to delete, or view the ViewTask page.
                        Navigation.PushAsync(new pageTask(Core.GetCore().GetScheduler().FindTaskById(Tasks.TaskID)));
                    }
                } 
            };
            */


            AddTask = new Button {
                Text = "Add Task",
                Style = MainCore.GetConfig().GenerateButtonStyle()
			};
			AddTask.Clicked += (sender, e) => {
				var pAddTask = new pageTask();
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
            // This callback gets trigger everytime the user comes to this screen. 
            base.OnAppearing();

            AppConfig appConfig = Core.GetCore().GetConfig();
            //DisplayAlert("Test TEst", "Hello World","ok");
            RefreshListViewSource();
            Style = appConfig.GeneratePageStyle();
            AddTask.Style = appConfig.GenerateButtonStyle(); 
            ConfigButton.Style = appConfig.GenerateButtonStyle();
        }


    };
}

