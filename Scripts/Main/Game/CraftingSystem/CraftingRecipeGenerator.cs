using System.Collections.Generic;
using Farmer.Scripts.Main.Game.Database;
using Godot;

namespace Farmer.Scripts.Main.Game.CraftingSystem
{
    public class CraftingRecipeGenerator: Node2D
    {
        public CraftingRecipe chestRecipe;
        
        public override void _Ready()
        {
            List<ItemAmount> chestCraftingMaterials = new List<ItemAmount>();
            chestCraftingMaterials.Add(CreateItemAmount("Wood", 10));
            ItemResource chestResult = Database.ItemDatabase.Instance.GetItem("Chest");
            chestRecipe = new CraftingRecipe(chestCraftingMaterials, chestResult, true);
            
            GD.Print(chestRecipe.materials[0].item.itemName);
        }

        ItemAmount CreateItemAmount(string itemName, int amount)
        {
            ItemAmount itemAmount = new ItemAmount();
            itemAmount.item = Database.ItemDatabase.Instance.GetItem(itemName);
            itemAmount.amount = amount;

            return itemAmount;
        }

    }
}