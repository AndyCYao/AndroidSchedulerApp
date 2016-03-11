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
        //Read through this git https://github.com/mhalkovitch/Xamarim/blob/dc595559c24c649135ccdc21b210f94aa2559634/Chapter%205%20-%20Lists/ListViewExample/ListViewExample/ListViewExample/HomePage.cs
        class ListItemCell: ViewCell{
            public ListItemCell()
            {
                var moreAction = new MenuItem { Text = "More" };
                moreAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
                moreAction.Clicked += (sender, e) =>
                {
                    var mi = ((MenuItem)sender);
                    var item = (AppTask)mi.CommandParameter;
                };
                var deleteAction = new MenuItem { Text = "Delete", IsDestructive = true };
                deleteAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
                deleteAction.Clicked += async (sender, e) =>
                {

                    var mi = ((MenuItem)sender);
                    var item = (AppTask)mi.CommandParameter;

                    bool willDelete = await App.Current.MainPage.DisplayAlert("Delete Confirmation", "Are you sure you want to remove this task?", "Yes", "No");

                    if (willDelete)
                    {

                        Core.GetCore().GetScheduler().RemoveTask(item.TaskID);
                        ((Main)((Xamarin.Forms.NavigationPage)App.Current.MainPage).CurrentPage).OnAppearing();
                    }   
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

            System.Collections.ObjectModel.ObservableCollection<AppTask> oTasksList = new System.Collections.ObjectModel.ObservableCollection<AppTask>(MainScheduler.GetTasks(false));
            listView.ItemTemplate = new DataTemplate(typeof(ListItemCell)); 
            listView.ItemsSource = oTasksList;
        }

        ListView listView; //This is to list all the current tasks in our XML memory.
        Button ConfigButton, AddTask;

        public Main(){
			Title = "Scheduler App 2016";
            Core MainCore = Core.GetCore();
			Scheduler MainScheduler = MainCore.GetScheduler();

            listView = new ListView ();
            listView.ItemSelected += (sender, e) => {

                AppTask x = (AppTask)e.SelectedItem;
                if (x is AppTask)
                {
                    Navigation.PushAsync(new pageTask(Core.GetCore().GetScheduler().FindTaskById(x.TaskID)));
                    ((ListView)sender).SelectedItem = null;
                }     
            };
        
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
               
                this.Navigation.PushAsync(pAppSettings);
            };

			Content = new StackLayout {
				VerticalOptions = LayoutOptions.Center,
				Children = {
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

            RefreshListViewSource();
            Style = appConfig.GeneratePageStyle();
            AddTask.Style = appConfig.GenerateButtonStyle(); 
            ConfigButton.Style = appConfig.GenerateButtonStyle();
        }
    };
}
