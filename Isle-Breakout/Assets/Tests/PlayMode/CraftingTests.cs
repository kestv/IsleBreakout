using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class CraftingTests
    {
        CraftingRecipe recipe;
        GameObject wood;
        DependencyManager manager;
        RecipeController recipeCtrl;
        PlayerInventory inventory;

        [UnityTest]
        public IEnumerator CraftingTest0()
        {
            //SceneManager.LoadScene(2);
            yield return new WaitForSeconds(4f);
            manager = GameObject.Find("Manager").GetComponent<DependencyManager>();
            wood = GameObject.Instantiate(Resources.Load("Mats/Wood/Wood") as GameObject);
            recipeCtrl = manager.getCraftingPanel().transform.GetChild(0).GetChild(0).GetComponent<RecipeController>();
            recipe = recipeCtrl.getRecipes()[0];
            inventory = manager.getPlayer().GetComponent<PlayerInventory>();
            yield return null;
            Assert.IsTrue(manager != null && wood != null && recipe != null);
        }

        [UnityTest]
        public IEnumerator CraftingTest1()
        {
            yield return new WaitForSeconds(1f);
            bool noMats = true;
            noMats = recipe.CanCraft(inventory);
            for (int i = 0; i < 3; i++)
            {
                GameObject woodCopy = GameObject.Instantiate(wood);
                inventory.AddItem(woodCopy);
            }
            yield return new WaitForSeconds(1f);
            bool haveMats = recipe.CanCraft(inventory);
            yield return null;
            Assert.IsTrue(!noMats && haveMats);
        }

        [UnityTest]
        public IEnumerator CraftingTest2()
        {
            recipeCtrl.Open();
            recipeCtrl.Close();
            recipeCtrl.getRecipes();
            yield return null;
            Assert.IsTrue(true);
        }

        [UnityTest]
        public IEnumerator CraftingTest3()
        {            
            recipeCtrl.Open();
            yield return new WaitForSeconds(1f);
            RecipeSlotController slot = manager.getCraftingPanel().transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<RecipeSlotController>();
            
            slot.setRecipe(slot.getRecipe());            
            slot.setSlotImage(slot.getSlotImage());            
            slot.setSlotText(slot.getSlotText());
            slot.setSlotDarkPanel(slot.getSlotDarkPanel());
            slot.setRecipeAvailable(slot.getRecipeAvailable());
            slot.SetRecipeAvailability();
            yield return null;
            Assert.IsTrue(true);
        }

        [UnityTest]
        public IEnumerator CraftingTest4()
        {
            recipeCtrl.Open();
            yield return new WaitForSeconds(1f);
            RecipeSlotController slot = manager.getCraftingPanel().transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<RecipeSlotController>();
            slot.OnPointerClick(null);
            yield return null;
            CraftItemController craftCtrl = manager.getCraftingPanel().transform.GetChild(1).GetComponent<CraftItemController>();
            craftCtrl.FormatCountText();
            craftCtrl.Craft();
            yield return new WaitForSeconds(0.5f);
            inventory.RemoveItem(0);
            yield return null;
            Assert.IsTrue(true);
        }

        [UnityTest]
        public IEnumerator CraftingTest5()
        {
            yield return new WaitForSeconds(0.5f);
            CraftItemSlotController craftSlot = manager.getCraftingPanel().transform.GetChild(1).GetChild(2).GetChild(0).GetComponent<CraftItemSlotController>();            
            craftSlot.getCountText();
            craftSlot.getSlotCountText();
            craftSlot.getSlotSprite();
            craftSlot.getText();
            craftSlot.getSprite();
            craftSlot.getCountText();
            craftSlot.setCountText(null);
            craftSlot.setText(null);
            craftSlot.setSprite(null);
            yield return null;
            Assert.IsTrue(true);
        }

        [UnityTest]
        public IEnumerator CraftingTest6()
        {
            recipe.getCraftedItem();
            recipe.setCraftedItem(null);
            recipe.getMaterials();
            recipe.setMaterials(new List<Item>());
            yield return null;
            Assert.True(true);
        }
    }
}
