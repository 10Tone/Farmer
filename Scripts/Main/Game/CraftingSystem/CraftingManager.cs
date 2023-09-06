using System;
using Farmer.Scripts.Main.Game.Database;
using Godot;
using Godot.Collections;
using Array = Godot.Collections.Array;

namespace Farmer.Scripts.Main.Game.CraftingSystem
{
    public class CraftingManager: Node2D
    {
        private Node2D _jsonLoad;
        
        public override void _Ready()
        {
            _jsonLoad = (Node2D)GetNode("/root/JsonLoader");
        }

        Dictionary<ItemResource, int> GetRecipeMaterials(string name)
        {
            var materials = _jsonLoad.Call("get_recipe_materials", name) as Array;
            var recipeMaterialsResult = new Dictionary<ItemResource, int>();
            
            foreach (var material in materials)
            {
                var item = material as Dictionary;
                ItemResource materialItem = ItemDatabase.Instance.GetItem(item["MaterialItem"].ToString());
                int materialAmount = Int32.Parse(item["MaterialAmount"].ToString());
                recipeMaterialsResult.Add(materialItem, materialAmount);
            }
            return recipeMaterialsResult;
        }

        public ItemResource CraftItem(string name, Dictionary<int, ItemResource> inventory)
        {
            // GD.Print("Start crafting");
            var materials = GetRecipeMaterials(name);

            if (materials == null)
            {
                GD.Print("No materials in recipe found");
                return null;
            }

            foreach (var material in materials)
            {
                for (int i = 0; i < inventory.Count; i++)
                {
                    if (material.Key.itemName == inventory[i].itemName)
                    {
                        if (inventory[i].stackAmount >= material.Value)
                        {
                            // GD.Print(material.Key.itemName + " has enough amount in inventory");
                            inventory[i].stackAmount -= material.Value;

                            ItemResource craftedItem = ItemDatabase.Instance.GetItem("Chest");
                            return craftedItem;
                        }
                        else
                        {
                            GD.Print(material.Key.itemName + " has NOT enough amount in inventory");
                            return null;
                        }
                    }
                }
            }

            return null;
        }
    }
}