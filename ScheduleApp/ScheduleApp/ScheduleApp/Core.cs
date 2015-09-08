namespace ScheduleApp
{
    public class Core
    {
        string SCHEDULEAPP_PHRASE_FILE = "Phrases.xml";
        string SCHEDULEAPP_CONFIG_FILE = "Config.xml";
        private PhraseManager m_phraseManager;
        private Scheduler m_scheduler;
        private AppConfig m_configuration;
        private Core m_core;

        public static Core GetCore()
        {
            if (!m_core)
            {
                m_core = new Core();
            }

            return m_core;
        }

        public Core()
        {
            m_configuration = new AppConfig(SCHEDULEAPP_CONFIG_FILE);
            m_phraseManager = new PhraseManager(SCHEDULEAPP_PHRASE_FILE);
            m_scheduler = new Scheduler();
        }

        public PhraseManager GetPhraseManager()
        {
            return m_phraseManager;
        }

        public Scheduler GetScheduler()
        {
            return m_scheduler;
        }

        public AppConfig GetConfig()
        {
            return m_configuration;
        }
    }
}
