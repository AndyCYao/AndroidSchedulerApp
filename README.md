# AndroidSchedulerApp
AnYoPe Jul 28th 2015 - Android Scheduler App in C#.
Andy's Task for Jul 28th-



# Overview:
AndroidSchedulerApp (ASA) is an android app that reminds users their tasks, it will send periodic reminders based on the user's preference. The app will be created using xamarin and C#.

# Scenario:
Scenario 1: Andy has a weekly list of events including playing sports, volunteering, and keeping up with the Kardarshians. To help him keep track of all these tasks, he inputs them into the ASA. and the ASA notifies him periodically through push notifications on his Android device.

# Flow Chart: 

# Screen By Screen Specification:
  
  Main Page:
    Contains a list box of current tasks. with two buttons that navigates to either "Add Task" or "Setting" respectively.

  Add Task:
    Allows users to add new tasks, contains all the relevant inputs required. At the time of writing, the inputs include
    Task Name,
    Task Notes,
    Reminder Begin Date,
    Reminder End Date,
    Ringtone Name,
    Frequency,
    Frequency Unit

  Setting:
    Contains all the settings related to the user preference. 
    Default Notification Sound,
    Background Colour,
    Font Picker,
    Font Colour,
    Font Size


# Technical Note:

local storage only, no users accounts.
android only-> SDK 2.3 this version covers 90% of user base. compatible with since 2013 apps.
http://developer.android.com/intl/zh-CN/about/dashboards/index.html
Depending on OS version, either create a system wide configuration or per profile configuration


Some classes to consider:
  Reminder container
  Topic to be reminded of
  Location
  Time

Code Architecture:

Phrase manager -> used to phrase the notification so users get a customized message. ie 
    from “read xxx book” to “hey!, don’t forget to -read xxx book’”. 
    Add phrase (will require some form of noun and verb placement)
    Remove phrase
    Phrase at
  
Task Class  ->  includes the following property:
  id - primary key  this will be given to the Scheduler instead.  
  Name -> main subject line ie: “Read books by x author”
  Notes -> more detail ie: “Book held at library”
  Done ->  a boolean whether the task is completed or not. 
  Details of notification-> 
  “reminder end date” and 
  “frequency of reminder before the end date.”   , choose unit of frequency (hrs, days, weeks) 
  with location tracking or not (i think its call GeoFencing in OSX)  etc. 
  ringtone track
  starting time. (ie begin, next week, next day, etc.) 
  The scheduler uses the task’s details of notification and make the notification.


  Scheduler
    Generates notifications
      Could potentially group simultaneous notifications
      Interacts with the Phrase Manager
      **Look up libraries which could generate sentences based on given input**
    Stores tasks
      Add tasks
      Remove tasks
      TaskAt
    Get current time
    
App Configuration
    Default notification sound
    Show completed tasks
    Max number of tasks to show per page
    Themes?
      Font
      Colour
      Size
      Background colour
    Read
    Write

Core class
    Is a singleton with a getter to be used by thing such as the UI
    Has a scheduler
    Has a phrase manager
    Contains an app configuration instance

  
  To Do list for -> York-> notifications and integrate with timed reminders

UI side Notification Class  -> properties:
    Intent (stub action after user taps notification)
    PendingIntent (action to take after user taps notification)
    BigTextStyles (use full text when notification expanded)
    BigPictureStyles (use large images when notification expanded)
    InboxStyle (whether multiple notifications stack)
    Priority (for when to best notify user)
    Visibility (for lockscreen notifications)
    Category (type of notification used in system settings)

-> methods::
init()
buildNotification()
buildCompatNotification()
-sound (bool)
-vibration (bool)
-contentIntent(Intent)
getNotificationManager()
publishNotification()
updateNotification()

Convention for writing code:  use hungarian notation.
Design

Backend
Peter - System which takes in user defined “interest” and interfaces with the notification system
