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
            Player player = new Player("player1", "Current input");
            Assert.IsInstanceOfType(player, typeof(Player));
            Assert.AreEqual("player1", player.Name);
            Assert.AreEqual("Current input", player.CurrentInput);
        }
    }
}
