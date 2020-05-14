using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests
{
    public class UITests
    {
        UIHandler instance;
        [UnityTest]
        public IEnumerator UITests1()
        {
            instance = UIHandler.Instance;
            instance.DisplayMessage("Test");
            yield return new WaitForSeconds(0.1f);
            var obj = GameObject.Find("InfoMessage");
            Assert.IsTrue(obj.activeSelf);
        }

        [UnityTest]
        public IEnumerator UITests2()
        {
            var obj = GameObject.Find("InfoMessage");
            yield return new WaitForSeconds(0.1f);
            Assert.AreEqual("Test", obj.GetComponent<Text>().text);
        }

        [UnityTest]
        public IEnumerator UITests3()
        {
            var obj = GameObject.Find("InfoMessage");
            instance.DisplayMessage("Test1");
            yield return new WaitForSeconds(0.1f);
            Assert.AreEqual("Test", obj.GetComponent<Text>().text);
        }

        [UnityTest]
        public IEnumerator UITests4()
        {
            var obj = GameObject.Find("InfoMessage");
            instance.DisplayMessage("Test2", true);
            yield return new WaitForSeconds(0.1f);
            Assert.AreEqual("Test2", obj.GetComponent<Text>().text);
        }

        [UnityTest]
        public IEnumerator UITests5()
        {
            yield return new WaitForSeconds(5f);
            instance.DisplayDamage(10);
            var obj = GameObject.Find("DamageMessage");
            yield return new WaitForSeconds(1f);
            Assert.AreEqual("10", obj.GetComponent<Text>().text);
        }

        [UnityTest]
        public IEnumerator UITests6()
        {
            instance.DisplayReward("level", true);
            var obj = GameObject.Find("RewardInfo");
            yield return new WaitForSeconds(1f);
            Assert.AreEqual("level", obj.GetComponent<Text>().text);
        }
    }
}
