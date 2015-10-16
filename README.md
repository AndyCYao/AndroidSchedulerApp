# AndroidSchedulerApp
AnYoPe Jul 28th 2015 - Android Scheduler App in C#.
Andy's Task for Jul 28th-


Implement the Task class as per discussion. things to test include: write out necessary setters and getters functions to test the task class, we expect the task class to return a xml snippet with the below info. Test so far as to confirm that it work, should be able to handle really long string, ensure the components of the Task class works. 

Sept 5th 2015-

  Created a Main Page and Add Task Page in xamarin.form. need to wire the add task page to the scheduler's add task method.
  
Sept 15th 2015 - AY
To Do List:
    -Flesh out the MainPage so that upon entering the page, it shows all the tasks in memory
      -connect it with the Core function, Static Class only one instance of this class.  ->  Scheduler Getter, 
    -Create a View Task page. 

Sept 24th 2015 --AY
    -Test to see if the connector works from pageMain.cs  to the scheduler.getActiveTask.
    -See if we can add task and it saves to the Scheduler AddTask. 

Oct 8th 2015- AY
     -Create validation in the pageAddTask so that there isn't empty fields after usesrs press Save. so that before it is   submitted to the scheduler there wouldn't be any null and crash.
     
Oct 15th 2015 -AY
    -in the Task class, implement a NextReminderDate property and StartDateTime property. this is used to calculate the         next interval to remind.
    -Tabled for now, Blackout period in a Day. 
    -need to enumerate the ringtones and get the default value from the config core. maybe get the ringtones from a ringtone 
    folder, 
    York To do-> Implement the active task list form in the pageMain
    Andy to do-> and the pageAddTask will need to loop through this folder, just like how Config configures it. -> Utility        class, a static instance that has a function that says get ringtone. 
