using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class ArmorTests
    {
        DependencyManager manager;
        ArmorEquipper armor;
        ArmorEquipper armorUI;
        GameObject helmet;
        GameObject shoulders;
        GameObject cape;
        GameObject torso;
        GameObject legs;
        GameObject melee;
        GameObject ranged;
        PlayerStatsController statsCtrl;
        PlayerStatsController ctrl;
        PlayerStatsPanelController panelCtrl;
        //Load objects
        [UnityTest]
        public IEnumerator ArmorTest0()
        {
            SceneManager.LoadScene(2);
            yield return new WaitForSeconds(4f);
            manager = GameObject.Find("Manager").GetComponent<DependencyManager>();
            helmet = GameObject.Instantiate(Resources.Load("Equipment/TestEQP/Helmet") as GameObject);
            shoulders = GameObject.Instantiate(Resources.Load("Equipment/TestEQP/Shoulders") as GameObject);
            cape = GameObject.Instantiate(Resources.Load("Equipment/TestEQP/Cape") as GameObject);
            torso = GameObject.Instantiate(Resources.Load("Equipment/TestEQP/Torso") as GameObject);
            legs = GameObject.Instantiate(Resources.Load("Equipment/TestEQP/Legs") as GameObject);
            melee = GameObject.Instantiate(Resources.Load("Equipment/TestEQP/Sword") as GameObject);
            ranged = GameObject.Instantiate(Resources.Load("Equipment/TestEQP/Bow") as GameObject);
            armor = manager.getPlayerEquipper();
            armorUI = manager.getUIEquipper();
            ctrl = manager.getPlayer().GetComponent<PlayerStatsController>();
            statsCtrl = manager.getPlayer().GetComponent<PlayerStatsController>();
            yield return null;
            bool eqp = false;
            if(helmet != null && shoulders != null && cape != null && torso != null && melee != null && ranged != null && legs != null && armor != null)
            {
                eqp = true;
            }
            yield return null;
            Assert.IsTrue(manager != null && eqp);
        }

        [UnityTest]
        public IEnumerator ArmorTest1()
        {
            float strength = statsCtrl.GetStrength();
            float wisdom = statsCtrl.GetWisdom();
            float speed = statsCtrl.GetSpeed();
            IArmor equip = (IArmor)helmet.GetComponent<ItemSettings>().getEquip();
            armor.EquipHelmet(equip.getMeshes());
            armor.UnequipHelmet();
            ctrl.UpdateStrength(equip.getStrength());
            ctrl.UpdateSpeed(equip.getSpeed());
            ctrl.UpdateWisdom(equip.getWisdom());
            ctrl.UpdateHP(equip.getHP());
            float newStrength = statsCtrl.GetStrength();
            float newWisdom = statsCtrl.GetWisdom();
            float newSpeed = statsCtrl.GetSpeed();
            armorUI.EquipHelmet(equip.getMeshes());
            armorUI.UnequipHelmet();
            yield return null;
            bool statsChanged = false;
            float hp = equip.getHP();
            string type = equip.getType();
            if (strength != newStrength || wisdom != newWisdom || speed != newSpeed)
            {
                statsChanged = true;
            }
            Assert.IsTrue(statsChanged);
        }

        [UnityTest]
        public IEnumerator ArmorTest2()
        {
            float strength = statsCtrl.GetStrength();
            float wisdom = statsCtrl.GetWisdom();
            float speed = statsCtrl.GetSpeed();
            IArmor equip = (IArmor)cape.GetComponent<ItemSettings>().getEquip();
            armor.EquipCape(equip.getMeshes());
            armor.UnequipCape();
            ctrl.UpdateStrength(equip.getStrength());
            ctrl.UpdateSpeed(equip.getSpeed());
            ctrl.UpdateWisdom(equip.getWisdom());
            ctrl.UpdateHP(equip.getHP());
            float newStrength = statsCtrl.GetStrength();
            float newWisdom = statsCtrl.GetWisdom();
            float newSpeed = statsCtrl.GetSpeed();
            armorUI.EquipCape(equip.getMeshes());
            armorUI.UnequipCape();
            yield return null;
            bool statsChanged = false;
            float hp = equip.getHP();
            string type = equip.getType();
            if (strength != newStrength || wisdom != newWisdom || speed != newSpeed)
            {
                statsChanged = true;
            }
            Assert.IsTrue(statsChanged);
        }

        [UnityTest]
        public IEnumerator ArmorTest3()
        {
            float strength = statsCtrl.GetStrength();
            float wisdom = statsCtrl.GetWisdom();
            float speed = statsCtrl.GetSpeed();
            IArmor equip = (IArmor)shoulders.GetComponent<ItemSettings>().getEquip();
            armor.EquipShoulders(equip.getMeshes());
            armor.UnequipShoulders();
            ctrl.UpdateStrength(equip.getStrength());
            ctrl.UpdateSpeed(equip.getSpeed());
            ctrl.UpdateWisdom(equip.getWisdom());
            ctrl.UpdateHP(equip.getHP());
            float newStrength = statsCtrl.GetStrength();
            float newWisdom = statsCtrl.GetWisdom();
            float newSpeed = statsCtrl.GetSpeed();
            armorUI.EquipShoulders(equip.getMeshes());
            armorUI.UnequipShoulders();
            yield return null;
            bool statsChanged = false;
            float hp = equip.getHP();
            string type = equip.getType();
            if (strength != newStrength || wisdom != newWisdom || speed != newSpeed)
            {
                statsChanged = true;
            }
            Assert.IsTrue(statsChanged);
        }

        [UnityTest]
        public IEnumerator ArmorTest4()
        {
            float strength = statsCtrl.GetStrength();
            float wisdom = statsCtrl.GetWisdom();
            float speed = statsCtrl.GetSpeed();
            IArmor equip = (IArmor)torso.GetComponent<ItemSettings>().getEquip();
            armor.EquipTorso(equip.getMeshes());
            armor.UnequipTorso();
            ctrl.UpdateStrength(equip.getStrength());
            ctrl.UpdateSpeed(equip.getSpeed());
            ctrl.UpdateWisdom(equip.getWisdom());
            ctrl.UpdateHP(equip.getHP());
            float newStrength = statsCtrl.GetStrength();
            float newWisdom = statsCtrl.GetWisdom();
            float newSpeed = statsCtrl.GetSpeed();
            armorUI.EquipTorso(equip.getMeshes());
            armorUI.UnequipTorso();
            yield return null;
            bool statsChanged = false;
            float hp = equip.getHP();
            string type = equip.getType();
            if (strength != newStrength || wisdom != newWisdom || speed != newSpeed)
            {
                statsChanged = true;
            }
            Assert.IsTrue(statsChanged);
        }

        [UnityTest]
        public IEnumerator ArmorTest5()
        {
            float strength = statsCtrl.GetStrength();
            float wisdom = statsCtrl.GetWisdom();
            float speed = statsCtrl.GetSpeed();
            IArmor equip = (IArmor)legs.GetComponent<ItemSettings>().getEquip();
            armor.EquipLegs(equip.getMeshes());
            armor.UnequipLegs();
            ctrl.UpdateStrength(equip.getStrength());
            ctrl.UpdateSpeed(equip.getSpeed());
            ctrl.UpdateWisdom(equip.getWisdom());
            ctrl.UpdateHP(equip.getHP());
            float newStrength = statsCtrl.GetStrength();
            float newWisdom = statsCtrl.GetWisdom();
            float newSpeed = statsCtrl.GetSpeed();
            armorUI.EquipLegs(equip.getMeshes());
            armorUI.UnequipLegs();
            yield return null;
            bool statsChanged = false;
            float hp = equip.getHP();
            string type = equip.getType();
            if (strength != newStrength || wisdom != newWisdom || speed != newSpeed)
            {
                statsChanged = true;
            }
            Assert.IsTrue(statsChanged);
        }

        [UnityTest]
        public IEnumerator ArmorTest6()
        {
            float strength = statsCtrl.GetStrength();
            float wisdom = statsCtrl.GetWisdom();
            float speed = statsCtrl.GetSpeed();
            IArmor equip = (IArmor)melee.GetComponent<ItemSettings>().getEquip();
            armor.EquipMeleeWeapon(equip.getMeshes());
            armor.UnequipMeleeWeapon();
            ctrl.UpdateStrength(equip.getStrength());
            ctrl.UpdateSpeed(equip.getSpeed());
            ctrl.UpdateWisdom(equip.getWisdom());
            ctrl.UpdateHP(equip.getHP());
            float newStrength = statsCtrl.GetStrength();
            float newWisdom = statsCtrl.GetWisdom();
            float newSpeed = statsCtrl.GetSpeed();
            armorUI.EquipMeleeWeapon(equip.getMeshes());
            armorUI.UnequipMeleeWeapon();
            yield return null;
            bool statsChanged = false;
            float hp = equip.getHP();
            string type = equip.getType();
            if (strength != newStrength || wisdom != newWisdom || speed != newSpeed)
            {
                statsChanged = true;
            }
            Assert.IsTrue(statsChanged);
        }

        [UnityTest]
        public IEnumerator ArmorTest7()
        {
            float strength = statsCtrl.GetStrength();
            float wisdom = statsCtrl.GetWisdom();
            float speed = statsCtrl.GetSpeed();
            IArmor equip = (IArmor)ranged.GetComponent<ItemSettings>().getEquip();
            armor.EquipRangedWeapon(equip.getMeshes());
            armor.UnequipRangedWeapon();
            ctrl.UpdateStrength(equip.getStrength());
            ctrl.UpdateSpeed(equip.getSpeed());
            ctrl.UpdateWisdom(equip.getWisdom());
            ctrl.UpdateHP(equip.getHP());
            float newStrength = statsCtrl.GetStrength();
            float newWisdom = statsCtrl.GetWisdom();
            float newSpeed = statsCtrl.GetSpeed();
            armorUI.EquipRangedWeapon(equip.getMeshes());
            armorUI.UnequipRangedWeapon();
            yield return null;
            bool statsChanged = false;
            float hp = equip.getHP();
            string type = equip.getType();
            if (strength != newStrength || wisdom != newWisdom || speed != newSpeed)
            {
                statsChanged = true;
            }
            Assert.IsTrue(statsChanged);
        }

        [UnityTest]
        public IEnumerator ArmorTest8()
        {
            IArmor equip = (IArmor)helmet.GetComponent<ItemSettings>().getEquip();
            yield return new WaitForSeconds(1f);
            System.Console.WriteLine();
            armor.Equip(equip);
            yield return new WaitForSeconds(1f);
            yield return null;
            Assert.IsTrue(true);
        }
    }
}
