using System;
using FridaForte;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Console;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

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
                 new string[] { "Open the door!","close door" },
                 new string[] { "Open", "Close" }, 
                 "correct", 
                 "dead",
                 new string[] { "go", "help" },
                 new string[] { "stay", "remain" });

            Assert.IsInstanceOfType(pharmacy, typeof(Location));
            Assert.AreEqual("Pharmacy", pharmacy.Name);
            Assert.AreEqual("Leaving the pharmacy....", pharmacy.Message);
            Assert.AreEqual("Open the door!", pharmacy.ChoiceContext[0]);
            Assert.AreEqual("Open", pharmacy.Choices[0]);
            Assert.AreEqual("Close", pharmacy.Choices[1]);
            Assert.AreEqual("correct", pharmacy.CorrectChoice);
            Assert.AreEqual("dead", pharmacy.Danger);
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

            Assert.IsInstanceOfType(actuals[0], typeof(Location));
            Assert.IsInstanceOfType(actuals[0].CorrectUniqueWords, typeof(Array));
            Assert.IsInstanceOfType(actuals[0].DangerUniqueWords, typeof(Array));
            Assert.IsNotNull(actuals);
            Assert.IsInstanceOfType(actuals, typeof(Location[]));
        }

        // *** This method requires user input
        // How do we test a method with user input?
        //[TestMethod]
        //public void TestProgramCanContinue()
        //{
        //    Location location = new Location(
        //        "Pharmacy",
        //        "Leaving the pharmacy....",
        //         new string[] { "Open the door!", "close door" },
        //         new string[] { "open", "close" },
        //         "correct",
        //         "dead",
        //         new string[] { "go", "help" },
        //         new string[] { "stay", "remain" });

        //    Assert.IsTrue(Program.CanContinue(location, true));
        //}

        [TestMethod]
        public void TestRunGame()
        {
            Location pharmacy = new Location(
                "Pharmacy",
                "Leaving the pharmacy....",
                 new string[] { "Open the door!", "close door" },
                 new string[] { "Open", "Close" },
                 "correct",
                 "dead",
                 new string[] { "go", "help" },
                 new string[] { "stay", "remain" });
            Location meadow = new Location(
               "Meadow",
               "Leaving the meadow....",
               new string[] { "Open the door!" },
               new string[] { "leave", "stay" }, 
               "correct", 
               "dead",
               new string[] { "go", "help" },
               new string[] { "stay", "remain" });

            Location[] locations = { pharmacy, meadow };

            int index = 0;

            for (int i = 0; i < locations.Length; i++)
            {
                index = i;
            }

            bool canContinue = true;
            for (int i = 0; i < 10 || canContinue; i++)
            {
                canContinue = false;
            }

            Assert.AreEqual(locations.Length - 1, index);
            Assert.AreEqual(false, canContinue);
        }

        [TestMethod]
        public void TestInitGameWorld()
        {
            Location pharmacy = new Location(
                "Pharmacy",
                "Leaving the pharmacy....",
                 new string[] { "Open the door!", "close door" },
                 new string[] { "Open", "Close" },
                 "correct",
                 "dead",
                 new string[] { "go", "help" },
                 new string[] { "stay", "remain" });
            Location meadow = new Location(
               "Meadow",
               "Leaving the meadow....",
               new string[] { "Open the door!" },
               new string[] { "leave", "stay" },
               "correct",
               "dead",
               new string[] { "go", "help" },
               new string[] { "stay", "remain" });
            Location[] locations = { pharmacy, meadow };
            GameWorld gameWorld = Program.InitGameWorld(locations);

            Assert.IsInstanceOfType(gameWorld.AllCorrectUniqueWords, typeof(HashSet<string>));
            Assert.IsTrue(gameWorld.AllCorrectUniqueWords.Contains("go"));
            Assert.IsTrue(gameWorld.AllCorrectUniqueWords.Contains("help"));
            Assert.IsInstanceOfType(gameWorld.AllDangerUniqueWords, typeof(HashSet<string>));
            Assert.IsTrue(gameWorld.AllDangerUniqueWords.Contains("stay"));
            Assert.IsTrue(gameWorld.AllDangerUniqueWords.Contains("remain"));
        }
    } // End class UnitTest1
}
