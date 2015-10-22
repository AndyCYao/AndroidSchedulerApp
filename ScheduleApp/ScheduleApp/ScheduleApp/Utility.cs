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

        //public List<String> GetRingTones(){
        public static string GetRingTones() {
            //The GetRingTones method will look through the Ringtone folder
            //loop through all the files that ends in .midi , retrieve their names. 
            
            //List<String> Results = new List<String>();
            String Results;
            Results = "Test";
            String fPath = "/RingTones/";
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            //IFolder folder = await rootFolder.GetFolderAsync(fPath,CreationCollisionOption.OpenIfExists);
            //Results = rootFolder;
            

            return Results;
        }
    }
}
