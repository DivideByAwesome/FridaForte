using System;
using FridaForte;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Console;
using System.IO;
using Newtonsoft.Json;

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
            // Make sure method doesn't change array
            pharmacy.ShowChoices();
            Assert.AreEqual("Open", pharmacy.Choices[0]);
            Assert.AreEqual("Close", pharmacy.Choices[1]);
        }

        [TestMethod]
        public void TestPlayerInstantiation()
        {
            Player player = new Player();
            Assert.IsInstanceOfType(player, typeof(Player));
            Assert.AreEqual("Frida", player.FirstName);
            Assert.AreEqual("Forte", player.LastName);
            //Assert.AreEqual("Current input", player.CurrentInput);
        }

        [TestMethod]
        public void TestProgramGetContent()
        {
            string actual = Directory.GetCurrentDirectory();
            Assert.IsNotNull(actual);

            actual += "../../../../FridaForte/GameContent.json";
            Assert.IsNotNull(actual);

            Location[] actuals = JsonConvert.DeserializeObject<Location[]>(File.ReadAllText(actual));
            Assert.IsNotNull(actuals);
            Assert.IsInstanceOfType(actuals, typeof(Location[]));
        }
    } // End class UnitTest1
}
