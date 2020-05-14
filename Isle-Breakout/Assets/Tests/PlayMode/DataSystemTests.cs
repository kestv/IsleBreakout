using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;

namespace Tests
{
    public class DataSystemTests
    {
        // A Test behaves as an ordinary method
        [Test]
        public void DeleteSaveTest()
        {
            int saveId = 0;
            DataSystem.DeleteSave(saveId);
            Assert.AreEqual(DataSystem.SaveExists(saveId), false);
        }

        [Test]
        public void SaveTest()
        {
            int saveId = 0;
            PlayerData player = new PlayerData("player", 1, 1, saveId, 1, 1);
            DataSystem.Save(player);
            Assert.AreEqual(DataSystem.SaveExists(0), true);
        }
    }
}
