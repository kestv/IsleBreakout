using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class OtherTests
    {
        GameObject infoPanelPrefab;
        GameObject infoCanvas;
        GameObject item;
        ItemInfoPanelController itemInfoCtrl;
        DependencyManager manager;
        GameObject helmet;

        [UnityTest]
        public IEnumerator TriggerTest0()
        {
            //SceneManager.LoadScene(2);
            yield return new WaitForSeconds(3f);
            manager = GameObject.Find("Manager").GetComponent<DependencyManager>();
            item = GameObject.Instantiate(Resources.Load("Consumable/Coconut") as GameObject);
            infoPanelPrefab = GameObject.Instantiate(Resources.Load("UI Prefabs/UI_ItemInfo") as GameObject);
            infoCanvas = manager.getItemInfoCanvas();
            GameObject infoPanel = GameObject.Instantiate(infoPanelPrefab, infoCanvas.transform);
            ItemInfoPanelController panelCtrl = infoPanel.GetComponent<ItemInfoPanelController>();
            helmet = GameObject.Instantiate(Resources.Load("Equipment/TestEQP/Helmet") as GameObject);
            ItemConsumable consumable = item.GetComponent<ItemConsumable>();
            panelCtrl.InitPanel(item.GetComponent<ItemSettings>(), consumable);
            panelCtrl.InitPanel(helmet.GetComponent<ItemSettings>());
            yield return null;
            Assert.IsTrue(true);
        }
    }
}
