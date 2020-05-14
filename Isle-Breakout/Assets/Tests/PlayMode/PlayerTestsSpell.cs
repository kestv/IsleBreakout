using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PlayerTestsSpell
    {
        GameObject player;
        PlayerCombatController pc;
        GameObject enemy;
        SpellController sc;
        SpellHolder sh;
        EnemyHealthController ehc;
        SpellInfo si;
        float enemyHp;

        [UnityTest]
        public IEnumerator PlayerSpellTest1()
        {
            player = GameObject.Find("PlayerInstance");
            sc = player.GetComponent<SpellController>();
            pc = player.GetComponent<PlayerCombatController>();
            pc.FindTarget();
            yield return new WaitForSeconds(0.1f);
            sh = GameObject.Find("Slot1").GetComponent<SpellHolder>();
            si = sh.GetSpell().GetComponent<SpellInfo>();
            sc.SetSpell(Resources.Load("ASpells/recall") as GameObject, 1);
            sc.CastSpell(enemy, sh);
            yield return new WaitForSeconds(0.1f);
            Assert.AreEqual(player.transform.position, sc.GetRecallPos());
            yield return null;
        }

        [UnityTest]
        public IEnumerator PlayerSpellTest2()
        {
            player.transform.position = new Vector3(player.transform.position.x + 10, player.transform.position.y, player.transform.position.z);
            yield return new WaitForSeconds(0.1f);
            sc.CastSpell(enemy, sh);
            Assert.AreEqual(sc.GetRecallPos(), player.transform.position);
        }

        [UnityTest]
        public IEnumerator PlayerSpellTest3()
        {
            sh.SetOnCooldown(false);
            enemy = GameObject.Instantiate(Resources.Load("Enemies/Enemy") as GameObject);
            enemy.transform.position = new Vector3(player.transform.position.x + 10, player.transform.position.y, player.transform.position.z);
            ehc = enemy.GetComponent<EnemyHealthController>();
            yield return null;
            sh.SetSpell(Resources.Load("ASpells/energy_ball") as GameObject);
            enemyHp = ehc.GetHealth();
            yield return new WaitForSeconds(0.1f);
            pc.FindTarget();
            sc.CastSpell(enemy, sh);
            yield return new WaitForSeconds(3f);
            var actualSpell = GameObject.Find("vfx_Muzzle_SpinBlue(Clone)");
            Assert.AreNotEqual(null, actualSpell);
        }

        [UnityTest]
        public IEnumerator PlayerSpellTest4()
        {
            yield return new WaitForSeconds(3f);
            Assert.IsTrue(ehc.GetHealth() < enemyHp);
            GameObject.Destroy(enemy);
        }

        [UnityTest]
        public IEnumerator PlayerSpellTest5()
        {
            yield return new WaitForSeconds(0.1f);
            Assert.AreEqual(sh.GetSpell().GetComponent<SpellInfo>().getName(), sc.GetSpellName(1));
        }

        [UnityTest]
        public IEnumerator PlayerSpellTest6()
        {
            yield return new WaitForSeconds(0.1f);
            Assert.IsTrue(sc.IsSlotTaken(1));
        }
    }
}
