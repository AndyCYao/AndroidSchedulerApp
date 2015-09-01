using System.Collections.Generic;
using System.Xml.Linq;
using PCLStorage;
using System.Threading.Tasks;

namespace ScheduleApp
{
    public class Phrase
    {
        private int m_nounPosition; //-1 if none
        private int m_verbPosition; //-1 if none
        private int m_namePosition; //-1 if none
        private string m_phrase;

        public Phrase(string inPhrase, int inNounPosition = -1, 
                    int inVerbPosition = -1, int inNamePosition = -1)
        {
            m_phrase = inPhrase;
            m_nounPosition = inNounPosition;
            m_verbPosition = inVerbPosition;
            m_namePosition = inNamePosition;
        }

        public string Text
        {
            get {
                return m_phrase;
            }

            set {
                m_phrase = value;
            }
        }

        public int NounPosition
        {
            get {
                return m_nounPosition;
            }

            set {
                m_nounPosition = value;
            }
        }

        public int VerbPosition
        {
            get
            {
                return m_verbPosition;
            }

            set
            {
                m_verbPosition = value;
            }
        }

        public int NamePosition
        {
            get
            {
                return m_namePosition;
            }

            set
            {
                m_namePosition = value;
            }
        }
    }

    public class PhraseManager
    {
        private List<Phrase> phrases; //a dictionary might make more sense

        public PhraseManager(string filePath = "")
        {
            phrases = new List<Phrase>();

            if (filePath.Length > 0)
            {
                System.Threading.Tasks.Task.Run(() => Load(filePath)).Wait();
            }
        }

        public int AddPhrase(Phrase input)
        {
            if (!phrases.Contains(input))
            {
                phrases.Add(input);

                return 0;
            }

            return -1;
        }

        public int RemovePhrase(Phrase input)
        {
            if (!phrases.Contains(input))
            {
                return -1;
            }

            phrases.Remove(input);

            return 0;
        }

        public int RemovePhraseAt(int index)
        {
            if (index >= 0 && index < phrases.Count)
            {
                phrases.RemoveAt(index);
                return 0;
            }

            return -1;            
        }

        public Phrase PhraseAt(int index)
        {
            if (index >= PhraseCount())
            {
                return null;
            }

            return (Phrase)phrases[index];
        }

        public int PhraseCount()
        {
            return phrases.Count;
        }

        public async Task<int> Load(string path)
        {
            IFile file = await FileSystem.Current.GetFileFromPathAsync(path);

            if (file != null)
            {
                XDocument document = XDocument.Load(await file.OpenAsync(FileAccess.Read));

                if (document.Root.Value == "Phrases")
                {
                    foreach (XElement child in document.Root.Descendants())
                    {
                        Phrase currentPhrase = new Phrase(child.Value, 
                                                    int.Parse(child.Attribute("nounPos").Value),
                                                    int.Parse(child.Attribute("verbPos").Value),
                                                    int.Parse(child.Attribute("namePos").Value));

                        AddPhrase(currentPhrase);
                    }
                }
            }
            else
            {
                //throw new System.IO.InvalidDataException("Error: Could not load phrases.");
            }

            return 0;
        }

        public async void Save(string path)
        {
            //save phrases into some base storage format
            IFile phraseFile = await FileSystem.Current.LocalStorage.CreateFileAsync(path, CreationCollisionOption.OpenIfExists);

            XDocument document = new XDocument();
            document.Root.Name = "Phrases";

            for (int i = 0; i < PhraseCount(); i++)
            {
                Phrase currentPhrase = PhraseAt(i);

                XElement phraseElement = new XElement("Phrase");
                phraseElement.Value = currentPhrase.Text;
                phraseElement.SetAttributeValue("nounPos", currentPhrase.NounPosition);
                phraseElement.SetAttributeValue("verbPos", currentPhrase.VerbPosition);
                phraseElement.SetAttributeValue("namePos", currentPhrase.NamePosition);
                document.Root.Add(phraseElement);
            }

            await phraseFile.WriteAllTextAsync(document.ToString());
        }
    }
}

