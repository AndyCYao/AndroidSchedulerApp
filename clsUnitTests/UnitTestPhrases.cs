using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScheduleApp;

namespace clsUnitTests
{
    [TestClass]
    public class UnitTestPhrases
    {
        [TestMethod]
        public void PhraseTest()
        {
            Phrase phrase = new Phrase("<name>, I am testing this phrase.", 5, 3, 0);

            Assert.AreEqual(phrase.Text, "<name>, I am testing this phrase.");
            Assert.AreEqual(phrase.NounPosition, 5);
            Assert.AreEqual(phrase.VerbPosition, 3);
            Assert.AreEqual(phrase.NamePosition, 0);
        }

        [TestMethod]
        public void PhraseManagerTest()
        {
            PhraseManager manager = new PhraseManager("Blah");
            Phrase phrase1 = new Phrase("<name>, I am testing this phrase.", 5, 3, 0);
            Phrase phrase2 = new Phrase("So it's gonna be forever, or it's going to go down in flames!");
            Phrase phrase3 = new Phrase("And you love the game!", 4, 2);

            manager.AddPhrase(phrase1);
            manager.AddPhrase(phrase2);
            manager.AddPhrase(phrase3);

            Assert.AreEqual(manager.PhraseCount(), 3);
            Phrase phrase = manager.PhraseAt(1);
            Assert.AreEqual(phrase.Text, "So it's gonna be forever, or it's going to go down in flames!");

            phrase = manager.PhraseAt(2);
            Assert.AreEqual(phrase.VerbPosition, 2);
        }
    }
}
