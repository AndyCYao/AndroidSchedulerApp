using NUnit.Framework;
using System;
using System.IO;
using ScheduleApp;
using System.Xml;
using System.Xml.Serialization;

namespace nUnitTestSchedulerApp
{
    [TestFixture]
    public class TestClass
    {
        [Test]
        public void TestCreatingTaskObject()
        {
            string TestTaskName = "PickUpBooks";
            string TestTaskNote = "BooksAtLibrary";
            Task GetBooks = new Task(TestTaskName, TestTaskNote, 0);
            Console.WriteLine(GetBooks.TaskNotes);
            Assert.AreEqual(TestTaskNote, GetBooks.TaskNotes);
            Assert.AreEqual(TestTaskName, GetBooks.TaskName);
        }

        [Test]
        public void TestTestTaskNameLength()
        {
            string TestTaskName = "PickUpBooks";
            string TestTaskNote = "BooksAtLibrary";
            double NameLength = TestTaskName.Length;
            Task GetBooks = new Task(TestTaskName, TestTaskNote, 0);
            Assert.AreEqual(NameLength, GetBooks.TaskName.Length);
        }
        
        //Testing the alternate struct constructor
        // does the struct have to be defined in the Scheduler class? 
        // it looks like i can't create a struct here and pass it to the Task class.
        // not like python where it can be a little vague. 
        [Test]
        public void TestStructConstructor()
        {
            TaskInfo testS;
            testS.TaskID = 1;
            testS.TaskName = "Create Struct";
            testS.TaskNotes = "update it, then test it";
            testS.Done = false;
            testS.ReminderEndDate = new DateTime(2015, 8, 20);
            testS.RingToneName = "Crazy Frog";

            Task x = new Task(ref testS);
            Assert.AreEqual(testS.TaskNotes, x.TaskNotes);
            Assert.AreEqual(testS.TaskName, x.TaskName);
            Assert.AreEqual(testS.Done, x.Done);
            Assert.AreEqual(testS.ReminderEndDate, x.ReminderEndDate);
            Assert.AreEqual(testS.RingToneName, x.RingTone);
            Assert.AreEqual(testS.TaskID, x.TaskID);

            //Just to see if there were any false positives. 
            //Assert.AreEqual(2, x.TaskID);
        }

        //Testing the XML writer, see if it outputs what we want to see. 
        //rewrite the test Aug 24th to the method Peter had updated. 
        [Test]
        public void TestXMLOutput()
        {
            TaskInfo testS;
            testS.TaskID = 1;
            testS.TaskName = "Create Struct";
            testS.TaskNotes = "update it, then test it";
            testS.Done = false;
            testS.ReminderEndDate = new DateTime(2015, 8, 20);
            testS.RingToneName = "CrazyFrog.wav";
            testS.Frequency = 5;
            testS.FrequencyUnit = "days";

            Task x = new Task(ref testS);

            /*
            //Console.WriteLine(x.WriteXML2());
            //System.Diagnostics.Debug.WriteLine(x.WriteXML2());
            XmlSerializer resultXML = new XmlSerializer(typeof(Task));
            MemoryStream resultStream = x.WriteXML2();
            resultStream.Position = 0;
            StreamReader reader = new StreamReader(resultStream);
            reader.ReadToEnd();

            System.Diagnostics.Debug.WriteLine(resultXML.Deserialize(resultStream));

            //Task y = new
            */

            XmlWriter writer = XmlWriter.Create("SerializationTest.xml");
            writer.WriteStartDocument();
            x.WriteXML(writer);
            writer.WriteEndDocument();
            writer.Dispose();

            XmlReader reader = XmlReader.Create("SerializationTest.xml");
            reader.Read();
            reader.ReadStartElement("TestingXMLoutput");
            Assert.AreEqual(reader.ReadElementContentAsString(), x.TaskID.ToString());
            Assert.AreEqual(reader.ReadElementContentAsString(), x.TaskName);
            Assert.AreEqual(reader.ReadElementContentAsString(), x.TaskNotes);
            Assert.AreEqual(reader.ReadElementContentAsString(), x.ReminderEndDate.ToString());
            Assert.AreEqual(reader.ReadElementContentAsString(), x.RingTone);
            Assert.AreEqual(reader.ReadElementContentAsString(), x.Done.ToString());
            reader.ReadEndElement();

            reader.Dispose();
        }


    }
}

