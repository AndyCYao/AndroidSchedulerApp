using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerApp
{
    class Core
    {
        private PhraseManager phraseManager;
        //private Scheduler scheduler;
        //private AppConfig configuration;

        public Core()
        {
            //initialize configuration
                //either the Core checks to see if a config exists to load 
                //or the configuration class does that

            phraseManager = new PhraseManager(/*Possible saved phrase location*/);
            //initialize scheduler instance
        }
    }
}
