using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests
{
    public class PlayerHealthTests
    {
        GameObject player;
        PlayerHealthController phc;
        Slider slider;

        [PreTest]
        public void PrepareData()
        {
            player = new GameObject();
            phc = player.AddComponent<PlayerHealthController>();
            slider = player.AddComponent<Slider>();
            phc.enabled = false;
        }

        [UnityTest]
        public IEnumerator ReceiveDamageTest()
        {
            PrepareData();
            phc.AssignVariables(slider, 100);
            var startingHealth = phc.GetHealth();
            phc.DoDamage(10);
            Assert.IsTrue(phc.GetHealth() < startingHealth);
            yield return null;
        }

        [UnityTest]
        public IEnumerator ReceiveDamageTest2()
        {
            PrepareData();
            phc.AssignVariables(slider, 100);

            var startingHealth = phc.GetHealth();
            var damage = 50f;
            phc.DoDamage(damage);
            Assert.AreEqual(startingHealth - damage, phc.GetHealth());
            yield return null;
        }

        [UnityTest]
        public IEnumerator HealTest()
        {
            PrepareData();
            phc.AssignVariables(slider, 100);

            var startingHealth = phc.GetHealth();
            phc.Heal(10);
            Assert.AreEqual(startingHealth, phc.GetHealth());
            yield return null;
        }

        [UnityTest]
        public IEnumerator HealTest2()
        {
            PrepareData();
            phc.AssignVariables(slider, 100, 50);

            var startingHealth = phc.GetHealth();
            phc.Heal(10);
            Assert.IsTrue(startingHealth + 10 == phc.GetHealth());
            yield return null;
        }

        [UnityTest]
        public IEnumerator HungerTest()
        {
            PrepareData();
            phc.AssignVariables(slider, 50, 50);

            var startingHealth = phc.GetHealth();
            phc.Eat(20);
            Assert.AreEqual(startingHealth, phc.GetHealth());
            yield return null;
        }

        [UnityTest]
        public IEnumerator HungerTest2()
        {
            PrepareData();
            phc.AssignVariables(slider, 100, 50);

            var startingHealth = phc.GetHealth();
            phc.Eat(20);
            Assert.AreEqual(startingHealth + 20, phc.GetHealth());
            yield return null;
        }

        [UnityTest]
        public IEnumerator DeathTest()
        {
            PrepareData();
            phc.AssignVariables(slider, 100, 100);

            var startingHealth = phc.GetHealth();
            phc.DoDamage(100);
            Assert.IsTrue(phc.IsDead());
            yield return null;
        }

        [UnityTest]
        public IEnumerator DeathTest2()
        {
            PrepareData();
            phc.AssignVariables(slider, 100, 100);
            phc.DoDamage(90);
            Assert.IsFalse(phc.IsDead());
            yield return null;
        }

        [UnityTest]
        public IEnumerator MaxHealthIncreaseTest()
        {
            PrepareData();
            phc.AssignVariables(slider, 100, 100);
            var health = 120;
            phc.ChangeMaxHealth(health);
            phc.Heal(health - 100);
            Assert.AreEqual(phc.GetHealth(), health);
            yield return null;
        }

        [UnityTest]
        public IEnumerator MaxHealthIncreaseTest2()
        {
            PrepareData();
            phc.AssignVariables(slider, 100, 100);
            var increase = 50f;
            var startingHealth = phc.GetHealth();
            phc.IncreaseMaxHealth(increase);
            phc.Heal(increase);
            Assert.AreEqual(phc.GetHealth(), 100 + increase);
            yield return null;
        }
    }
}
