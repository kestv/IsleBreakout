using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class InventoryTests
    {
        DependencyManager manager;
        PlayerInventory inventory;
        InventoryPanelController inventoryPanelCtrl;
        GameObject go;
        GameObject go2;

        //Load objects
        [UnityTest]
        public IEnumerator InventoryTest1()
        {
            //SceneManager.LoadScene(2);
            yield return new WaitForSeconds(5f);
            manager = GameObject.Find("Manager").GetComponent<DependencyManager>();
            inventory = manager.getPlayer().GetComponent<PlayerInventory>();
            inventoryPanelCtrl = manager.getInventoryPanel().GetComponent<InventoryPanelController>();
            go = GameObject.Instantiate(Resources.Load("Consumable/Coconut") as GameObject);
            go2 = GameObject.Instantiate(Resources.Load("Consumable/Blueberries") as GameObject);
            Assert.IsTrue(manager != null && inventory != null && inventoryPanelCtrl != null && go != null && go2 != null);
        }

        //Inventory size correct
        [UnityTest]
        public IEnumerator InventoryTest2()
        {
            yield return null;
            Assert.IsTrue(inventory.getInventory().Count == manager.getInventorySize());
        }

        //Adds item
        [UnityTest]
        public IEnumerator InventoryTest3()
        {
            yield return new WaitForSeconds(2f);
            bool added = false;
            for (int i = 0; i < manager.getInventorySize(); i++)
            {
                added = inventory.AddItem(go);
            }
            bool invFull = inventory.getInventoryFull();
            for (int i = 1; i < manager.getInventorySize(); i++)
            {
                inventory.Remove(i);
            }
            yield return null;
            Assert.IsTrue(invFull && added);
        }

        //Inserts item
        [UnityTest]
        public IEnumerator InventoryTest4()
        {
            inventory.InsertItem(go2, 1);
            bool added = false;
            if (inventory.getInventory()[1].Equals(go2))
            {
                added = true;
            }
            yield return null;
            Assert.IsTrue(added);
        }

        //Change item in position
        [UnityTest]
        public IEnumerator InventoryTest5()
        {
            inventory.ChangeItem(go2, 0);
            bool changed = false;
            if (inventory.getInventory()[0].Equals(go2))
            {
                changed = true;
            }
            inventory.ChangeItem(go, 0);
            yield return null;
            Assert.IsTrue(changed);
        }

        //Swap items
        [UnityTest]
        public IEnumerator InventoryTest6()
        {
            inventory.SwapItems(0, 1);
            bool swapped = false;
            if (inventory.getInventory()[0].Equals(go2))
            {
                swapped = true;
            }
            yield return null;
            Assert.IsTrue(swapped);
        }

        //Move item
        [UnityTest]
        public IEnumerator InventoryTest7()
        {
            inventory.MoveItem(1, 5);
            bool moved = false;
            if (inventory.getInventory()[1] == null && inventory.getInventory()[5] != null)
            {
                moved = true;
            }
            yield return null;
            Assert.IsTrue(moved);
        }

        //Drop item
        [UnityTest]
        public IEnumerator InventoryTest8()
        {
            inventory.DropItem(0);
            bool dropped = false;
            if (inventory.getInventory()[0] == null)
            {
                dropped = true;
            }
            yield return null;
            Assert.IsTrue(dropped);
        }

        //destroy item
        [UnityTest]
        public IEnumerator InventoryTest9()
        {
            inventory.RemoveItem(0);
            inventory.DestroyItem(5);
            yield return null;
            bool removed = false;
            if (inventory.getInventory()[0] == null)
            {
                removed = true;
            }
            bool destroyed = false;
            if (inventory.getInventory()[5] == null)
            {
                destroyed = true;
            }
            yield return null;
            Assert.IsTrue(removed && destroyed);
        }

        //Find item
        [UnityTest]
        public IEnumerator InventoryTestA()
        {
            go = GameObject.Instantiate(Resources.Load("Consumable/Coconut") as GameObject);
            go2 = GameObject.Instantiate(Resources.Load("Consumable/Blueberries") as GameObject);
            yield return null;
            inventory.AddItem(go);
            inventory.AddItem(go2);

            bool found = false;
            if(inventory.FindItemIndex(go) == 0)
            {
                found = true;
            }
            Assert.IsTrue(found);
        }

        //Find item
        [UnityTest]
        public IEnumerator InventoryTestB()
        {
            yield return null;
            Assert.IsTrue(inventory.Contains(0));
        }

        //Find item
        [UnityTest]
        public IEnumerator InventoryTestC()
        {
            bool contains = inventory.ContainsItem("Coconut");
            int containsID = inventory.ContainsItemID("Coconut");
            yield return null;
            Assert.IsTrue(contains && containsID == 0);
        }

        [UnityTest]
        public IEnumerator InventoryTestD()
        {
            bool removed = inventory.RemoveItem("Coconut");
            int itemCount = inventory.ItemCount("Coconut");
            yield return null;
            Assert.IsTrue(removed);
        }

        //Initiated slots count matches inventory size
        [UnityTest]
        public IEnumerator InventoryTestE()
        {            
            int slotCount = manager.getInventoryPanel().transform.childCount;
            int inventorySize = manager.getInventorySize();
            yield return null;
            Assert.IsTrue(slotCount == inventorySize);
        }
    }
}
