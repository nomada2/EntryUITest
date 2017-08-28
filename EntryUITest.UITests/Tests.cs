﻿using System.Linq;
using NUnit.Framework;

using Xamarin.UITest;

using EntryUITest.Constants;

using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace EntryUITest.UITests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
		readonly Query MyEntry, MyLabel;

        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;

            //Always initialize your UITest queries using "x.Marked" and referencing the UI ID
            //In Xamarin.Forms, you set the UI ID by setting the control's "AutomationId"
            //In Xamarin.Android, you set the UI ID by setting the control's "ContentDescription"
            //In Xamarin.iOS, you set the UI ID by setting the control's "AccessibilityIdentifiers"

            MyEntry = x => x.Marked(AutomationIdConstants.EntryAutomationID);
            MyLabel = x => x.Marked(AutomationIdConstants.LabelAutomationID);
        }

        [SetUp]
        public void BeforeEachTest()
        {
            string pageTitle = "Main Page";

            app = AppInitializer.StartApp(platform);

            app.WaitForElement(pageTitle);
        }

        [Test]
        public void EnterText()
        {
            //Arrange
            string typedText = "Hello world!";
            string retrievedText;

            //Act
            app.EnterText(MyEntry, typedText);
            app.DismissKeyboard();
            app.Screenshot($"Entered Text: {typedText}");

            //Assert
            retrievedText = app.Query(MyLabel).FirstOrDefault()?.Text;
            Assert.AreEqual(typedText, retrievedText, "The typed text does not match the text displayed on the screen");
        }

        [Ignore("Repl for testing/development only")]
        [Test]
        public void Repl()
        {
            app.Repl();
        }
    }
}

