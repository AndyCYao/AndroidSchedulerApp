﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScheduleApp;
using System.Xml;

/* I've been using the the nUnitTestSchedulerApp.test for unit testing Tasks, because I couldnt get Visual Studio
on my Mac. So i can't use the Visual Studio's test tools. 
i have not been updating this file
if no one is using it I suggest we delete it -AY
*/


namespace clsUnitTests
{
    [TestClass]
    public class UnitTestTasks
    {
        [TestMethod]
        public void CheckingIfNamesAndNotesAreLoaded()
        {
            string TestTaskName = "PickUpBook-HarryPotter";
            string TestTaskNote = "BooksAtLibrary";
            Task GetBooks = new Task(TestTaskName, TestTaskNote, 0);
            //Console.WriteLine(GetBooks.TaskNotes);
            Assert.AreEqual(TestTaskNote, GetBooks.TaskNotes);
            Assert.AreEqual(TestTaskName, GetBooks.TaskName);
        }
        [TestMethod]
        public void CheckingIfTaskNameLessThan40IsShortend()
        {
            string TestTaskName = "PickUpBook-HarryPotterAndTheChamberOfSecrets";
            string TestTaskNote = "BooksAtLibrary";
            Task GetBooks = new Task(TestTaskName, TestTaskNote, 0);
            //Console.WriteLine(GetBooks.TaskNotes);
            Assert.AreEqual(TestTaskNote, GetBooks.TaskNotes);
            Assert.AreEqual(TestTaskName.Substring(0,40), GetBooks.TaskName);
        }

        [TestMethod]
        public void SerializationTest()
        {
            string TestTaskName = "PickUpBook-HarryPotterAndTheChamberOfSecrets";
            string TestTaskNote = "BooksAtLibrary";
            Task GetBooks = new Task(TestTaskName, TestTaskNote, 0);
            GetBooks.RingTone = "Ringaling.wav";

            XmlWriter writer = XmlWriter.Create("SerializationTest.xml");
            writer.WriteStartDocument();
            GetBooks.WriteXML(writer);
            writer.WriteEndDocument();
            writer.Dispose();

            XmlReader reader = XmlReader.Create("SerializationTest.xml");
            reader.Read();
            reader.ReadStartElement("TestingXMLoutput");
            Assert.AreEqual(reader.ReadElementContentAsString(), GetBooks.TaskID.ToString());
            Assert.AreEqual(reader.ReadElementContentAsString(), GetBooks.TaskName);
            Assert.AreEqual(reader.ReadElementContentAsString(), GetBooks.TaskNotes);
            Assert.AreEqual(reader.ReadElementContentAsString(), GetBooks.ReminderEndDate.ToString());
            Assert.AreEqual(reader.ReadElementContentAsString(), GetBooks.RingTone);
            Assert.AreEqual(reader.ReadElementContentAsString(), GetBooks.Done.ToString());
            reader.ReadEndElement();

            reader.Dispose();
        }
    }
}