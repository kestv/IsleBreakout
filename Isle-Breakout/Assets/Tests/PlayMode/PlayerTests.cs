using System.Collections;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class PlayerTests
    {
        GameObject player;
        PlayerCombatController pc;
        GameObject target;
        PlayerMovementController mc;
        PlayerLevelController lc;
        PlayerStatsController sc;

        [UnityTest]
        public IEnumerator PlayerCombatTest1()
        {
            //SceneManager.LoadScene(2);
            yield return null;
            player = GameObject.Find("PlayerInstance");
            pc = player.GetComponent<PlayerCombatController>();
            pc.FindTarget();
            yield return null;
            Assert.AreEqual(null, pc.GetTarget());
        }

        [UnityTest]
        public IEnumerator PlayerCombatTest2()
        {
            target = GameObject.Instantiate(Resources.Load("Enemies/Enemy") as GameObject);
            target.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            yield return new WaitForSeconds(1f);
            pc.FindTarget();
            Assert.AreNotEqual(null, pc.GetTarget());
        }

        [UnityTest]
        public IEnumerator PlayerCombatTest22()
        {
            yield return new WaitForSeconds(0.1f);
            var bar = GameObject.Find("EnemyHealthbar");
            pc.ResetTarget();
            yield return new WaitForSeconds(0.1f);
            Assert.False(bar.activeSelf);
        }

        [UnityTest]
        public IEnumerator PlayerCombatTest3()
        {
            pc.AttackFromRange();
            yield return new WaitForSeconds(1f);
            var arrow = GameObject.Find("arrow(Clone)");
            Assert.AreNotEqual(null, arrow);
        }
        [UnityTest]
        public IEnumerator PlayerCombatTest4()
        {
            yield return null;
            Assert.AreNotEqual(true, pc.GetInCombat());
        }

        [UnityTest]
        public IEnumerator PlayerCombatTest5()
        {
            pc.AttackFromRange();
            pc.AttackFromRange();
            yield return new WaitForSeconds(2f);
            Assert.AreEqual(target.GetComponent<EnemyHealthController>().IsDead(), true);
        }

        [UnityTest]
        public IEnumerator PlayerMovementTest1()
        {
            mc = player.GetComponent<PlayerMovementController>();
            var startingPos = mc.transform.position;
            mc.MovePlayer(1, 0);
            yield return new WaitForSeconds(0.5f);
            Assert.IsTrue(pc.transform.position.x > startingPos.x);
        }

        [UnityTest]
        public IEnumerator PlayerMovementTest2()
        {
            var startingPos = mc.transform.position;
            mc.MovePlayer(-1, 0);
            yield return new WaitForSeconds(0.5f);
            Assert.IsTrue(pc.transform.position.x < startingPos.x);
        }

        [UnityTest]
        public IEnumerator PlayerMovementTest3()
        {
            var startingPos = mc.transform.position;
            mc.MovePlayer(0, 1);
            yield return new WaitForSeconds(0.5f);
            Assert.IsTrue(pc.transform.position.z > startingPos.z);
        }

        [UnityTest]
        public IEnumerator PlayerMovementTest4()
        {
            var startingPos = mc.transform.position;
            mc.MovePlayer(0, -1);
            yield return new WaitForSeconds(0.5f);
            Assert.IsTrue(pc.transform.position.z < startingPos.z);
        }

        [UnityTest]
        public IEnumerator PlayerExperienceTest1()
        {
            lc = player.GetComponent<PlayerLevelController>();
            var lvl = lc.GetLevel();
            lc.GetExperience(1000);
            yield return new WaitForSeconds(0.5f);
            Assert.IsTrue(lc.GetLevel() > lvl);
        }

        [UnityTest]
        public IEnumerator PlayerStatsTest1()
        {
            sc = player.GetComponent<PlayerStatsController>();
            var pts = sc.GetRemainingPoints();
            lc.GetExperience(1000);
            yield return new WaitForSeconds(0.5f);
            Assert.IsTrue(sc.GetRemainingPoints() > pts);
        }

        [UnityTest]
        public IEnumerator PlayerStatsTest2()
        {
            var strength = sc.GetStrength();
            sc.ImproveStrength(2);
            yield return new WaitForSeconds(0.5f);
            Assert.IsTrue(sc.GetStrength() > strength);
        }

        [UnityTest]
        public IEnumerator PlayerStatsTest3()
        {
            var speed = sc.GetSpeed();
            sc.ImproveSpeed(2);
            yield return new WaitForSeconds(0.5f);
            Assert.IsTrue(sc.GetSpeed() > speed);
        }

        [UnityTest]
        public IEnumerator PlayerStatsTest4()
        {
            var wisdom = sc.GetWisdom();
            sc.ImproveWisdom(2);
            yield return new WaitForSeconds(0.5f);
            Assert.IsTrue(sc.GetStrength() > wisdom);
        }

        [UnityTest]
        public IEnumerator PlayerStatsTest5()
        {
            var stats = sc.GetStrength() + sc.GetSpeed() + sc.GetWisdom();
            sc.AddBonuses(1,1,1);
            yield return new WaitForSeconds(0.5f);
            Assert.IsTrue(sc.GetStrength() + sc.GetSpeed() + sc.GetWisdom() > stats);
        }
    }
}
