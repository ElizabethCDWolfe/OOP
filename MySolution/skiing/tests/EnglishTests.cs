using System;
using EnglishNS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnglishTestsNS
{
    [TestClass]
    public class EnglishTests
    {
        [TestMethod]
        public void SayHello_ShouldOutputHelloToConsole()
        {
            // Arrange
            var english = new English();

            // Redirect console output to a StringWriter
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                english.SayHello();

                // Assert
                Assert.AreEqual("Hello\n", sw.ToString());
            }
        }
    }
}
