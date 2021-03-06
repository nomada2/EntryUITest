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
        readonly Query _myEntry, _myLabel;

        IApp _app;
        Platform _platform;

        public Tests(Platform platform)
        {
            _platform = platform;

            //Always initialize your UITest queries using "x.Marked" and referencing the UI ID
            //In Xamarin.Forms, you set the UI ID by setting the control's "AutomationId"
            //In Xamarin.Android, you set the UI ID by setting the control's "ContentDescription"
            //In Xamarin.iOS, you set the UI ID by setting the control's "AccessibilityIdentifiers"

            _myEntry = x => x.Marked(AutomationIdConstants.EntryAutomationID);
            _myLabel = x => x.Marked(AutomationIdConstants.LabelAutomationID);
        }

        [SetUp]
        public void BeforeEachTest()
        {
            string pageTitle = "Main Page";

            _app = AppInitializer.StartApp(_platform);

            _app.WaitForElement(pageTitle);
        }

        [Test]
        public void EnterText()
        {
            //Arrange
            string typedText = "Hello world!";
            string retrievedText;

            //Act
            _app.EnterText(_myEntry, typedText);
            _app.DismissKeyboard();
            _app.Screenshot($"Entered Text: {typedText}");

            //Assert
            retrievedText = _app.Query(_myLabel).FirstOrDefault()?.Text;
            Assert.AreEqual(typedText, retrievedText, "The typed text does not match the text displayed on the screen");
        }

        [Ignore("Repl for testing/development only")]
        [Test]
        public void Repl() => _app.Repl();
    }
}

