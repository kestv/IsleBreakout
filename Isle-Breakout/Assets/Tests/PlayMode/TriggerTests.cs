using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class TriggerTests
    {
        GameObject item;
        GameObject chest;
        GameObject craftBench;
        GameObject resource;
        GameObject interactable;
        DependencyManager manager;
        GameObject player;
        Vector3 playerPos;
        PlayerTriggerHandler triggerHandler;

        [UnityTest]
        public IEnumerator TriggerTest0()
        {
            //SceneManager.LoadScene(2);
            yield return new WaitForSeconds(3f);
            item = GameObject.Instantiate(Resources.Load("Mats/Compass") as GameObject);
            chest = GameObject.Find("Chest1");
            craftBench = GameObject.Instantiate(Resources.Load("Crafting Recipes/CraftingBench") as GameObject);
            resource = GameObject.Instantiate(Resources.Load("GatherResources/Blueberry Bush") as GameObject);
            interactable = GameObject.Instantiate(Resources.Load("Mats/Wood/BrokenBoat") as GameObject);
            manager = GameObject.Find("Manager").GetComponent<DependencyManager>();
            player = manager.getPlayer();
            playerPos = player.transform.position;
            triggerHandler = player.GetComponent<PlayerTriggerHandler>();
            yield return null;
            Assert.IsTrue(true);
        }

        [UnityTest]
        public IEnumerator TriggerTest1()
        {
            yield return null;
            item.transform.position = playerPos;
            yield return new WaitForSeconds(0.5f);
            triggerHandler.PickUpItem();
            yield return null;
            item.transform.position = new Vector3(999, 999, 999);
            yield return null;
            Assert.IsTrue(true);
        }

        [UnityTest]
        public IEnumerator TriggerTest2()
        {
            yield return null;
            chest.transform.position = playerPos;
            yield return new WaitForSeconds(0.5f);
            List<GameObject> triggers = triggerHandler.getTriggers();
            ChestSettings chestSettings = triggers[triggers.Count - 1].GetComponent<ChestSettings>();
            manager.getCanvasController().DisableAllPanelsExcept(chestSettings.getChestPanel().transform);
            chestSettings.getChestPanel().SetActive(true);
            yield return null;
            chest.transform.position = new Vector3(999, 999, 999);
            yield return null;
            Assert.IsTrue(true);
        }

        [UnityTest]
        public IEnumerator TriggerTest3()
        {
            yield return null;
            resource.transform.position = playerPos;
            yield return new WaitForSeconds(0.5f);
            List<GameObject> triggers = triggerHandler.getTriggers();
            triggers[triggers.Count - 1].GetComponent<ResourceGatherer>().Gather();
            yield return new WaitForSeconds(6f);
            item.transform.position = new Vector3(999, 999, 999);
            yield return null;
            Assert.IsTrue(true);
        }

        [UnityTest]
        public IEnumerator TriggerTest4()
        {
            yield return null;
            interactable.transform.position = playerPos;
            yield return new WaitForSeconds(0.5f);
            List<GameObject> triggers = triggerHandler.getTriggers();
            triggers[triggers.Count - 1].GetComponent<InteractableObject>().TransformToNewObject();
            triggers.RemoveAt(triggers.Count - 1);
            triggerHandler.UpdateTriggerMessage();
            yield return new WaitForSeconds(6f);
            interactable.transform.position = new Vector3(999, 999, 999);
            yield return null;
            Assert.IsTrue(true);
        }


    }
}
