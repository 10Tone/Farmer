using System.Collections.Generic;
using Farmer.Scripts.Main.Game.Database;
using Godot;

namespace Farmer.Scripts.Main.Game.CraftingSystem
{
    public struct ItemAmount
    {
        public ItemResource item;
        public int amount;
    }
    
    
    public class CraftingRecipe
    {
        public List<ItemAmount> materials;
        public ItemResource result;
        public bool unlocked;

        public CraftingRecipe(List<ItemAmount> craftingMaterials, ItemResource craftingResult, bool recipeUnlocked)
        {
            materials = craftingMaterials;
            result = craftingResult;
            unlocked = recipeUnlocked;
        }
        
    }
}