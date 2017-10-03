using System;
using FridaForte;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Console;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestProgramTestConnected()
        {
            // This blank test assures that test project is connected.
            // Assert.AreEqual(true, false);
        }

        [TestMethod]
        public void TestLocationClassInstantition()
        {
            Location pharmacy = new Location(
                "Pharmacy", 
                "Leaving the pharmacy....",
                "Open the door!",
                new string[] {"Open", "Close" });

            Assert.IsInstanceOfType(pharmacy, typeof (Location));
            Assert.AreEqual("Pharmacy", pharmacy.Name);
            Assert.AreEqual("Leaving the pharmacy....", pharmacy.Message);
            Assert.AreEqual("Open the door!", pharmacy.Hint);
            Assert.AreEqual("Open", pharmacy.Choices[0]);
            Assert.AreEqual("Close", pharmacy.Choices[1]);
        }

        [TestMethod]
        public void TestPlayerInstantiation()
        {
            Player player = new Player("Current input");
            Assert.IsInstanceOfType(player, typeof(Player));
            Assert.AreEqual("Frida", player.Name);
            Assert.AreEqual("Current input", player.CurrentInput);
        }

        [TestMethod]
        public void TestValidatorInstantiation()
        {
            string actual1 = Validator.ValidateString("abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxy");
            string actual2 = Validator.ValidateString("a");
            string actual3 = Validator.ValidateString("aa");

            string expected1 = "I don't understand that command.";
            string expected2 = string.Empty;

            Assert.IsInstanceOfType(actual1, typeof(string));
            Assert.AreEqual(expected1, actual1);
            //Assert.AreEqual(expected2, actual2);
            Assert.AreEqual(string.Empty, actual3);

        }
    }
}
