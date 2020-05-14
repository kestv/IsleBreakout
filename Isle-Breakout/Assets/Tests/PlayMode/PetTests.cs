using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class PetTests
    {
        PetController pc;
        GameObject player;
        GameObject pet;
        SpellHolder sh;
        [UnityTest]
        public IEnumerator PetTest1()
        {
            SceneManager.LoadScene(2);
            yield return null;
            player = GameObject.Find("PlayerInstance");
            pet = GameObject.Find("Horse");
            pet.GetComponent<EnemyWander>().enabled = false;
            pc = pet.GetComponent<PetController>();
            yield return new WaitForSeconds(0.5f);
            Assert.IsFalse(pc.IsTamed());
        }

        [UnityTest]
        public IEnumerator PetTest2()
        {
            sh = GameObject.Find("Slot1").GetComponent<SpellHolder>();
            player.GetComponent<SpellController>().SetSpell(Resources.Load("ASpells/pet_tame") as GameObject, 1);
            
            var sc = player.GetComponent<PlayerStatsController>();
            var speed = sc.GetSpeed();
            pet.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            player.GetComponent<SpellController>().AddItemForPet();
            yield return new WaitForSeconds(0.1f);
            player.GetComponent<SpellController>().TamePet(sh, true);
            yield return new WaitForSeconds(3f);
            Assert.IsTrue(speed < sc.GetSpeed());
        }

        [UnityTest]
        public IEnumerator PetTest3()
        {
            yield return null;
            Assert.IsTrue(pc.IsTamed());
        }

        [UnityTest]
        public IEnumerator PetTest4()
        {
            pet.transform.Translate(20, 0, 0);
            var startPos = pet.transform.position;
            yield return new WaitForSeconds(2f);
            Assert.AreNotEqual(startPos, pet.transform.position);
        }
    }
}
