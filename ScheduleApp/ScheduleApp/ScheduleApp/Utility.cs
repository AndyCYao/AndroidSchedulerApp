using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCLStorage;

namespace ScheduleApp
{
    public static class Utility
    {
        /*Oct 19th 2015 This static class will contain at least a 
        "return list of ringtone" method. The method is used by pageConfig, and 
        pageAddTask for their ringtone picker.         
        */

        public static List<String> GetRingTones(){        
            //The GetRingTones method will look through the Ringtone folder
            //loop through all the files that ends in .midi , retrieve their names.             
            List<String> Results = new List<String>();
            
            String fPath = "/RingTones/";

            var ringToneFolder = NavigateToFolder(fPath);          

            return Results;
        }

        //Taken from 
        //https://mallibone.com/post/storing-files-from-the-portable-class-library-(pcl)

        //for some reason calling FileSystem.Current.LocalStorage returns a
        //...\AppData\Local\Microsoft Corporation\Microsoft (R) Windows (R) Operating System\10.0.10052.0

        public static async Task<IFolder> NavigateToFolder(string targetFolder) {
            /* Using PCL Storage */
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder folder = await rootFolder.GetFolderAsync(targetFolder);
  
            return folder;
        }
    }
}
