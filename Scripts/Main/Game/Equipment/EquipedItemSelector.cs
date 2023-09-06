using Farmer.Scripts.Main.Game.Autoload;
using Farmer.Scripts.Main.Game.Database;
using Godot;

namespace Farmer.Scripts.Main.Game.Equipment
{
    public class EquipedItemSelector
    {
        public void UpdateEquippedItem(ItemResource item)
        {
            if (item.itemName == null)
            {
                Global.Instance.currentEquippedItem = PlayerEquipment.Hand;
                return;
            }

            switch (item.itemType)
            {
                case ItemResourceType.Equipment:
                    SwitchEquipment(item);
                    break;
                case ItemResourceType.CraftingItem:
                    break;
                case ItemResourceType.Seed:
                    break;
                case ItemResourceType.Consumable:
                    break;
            }
        }

        void SwitchEquipment(ItemResource item)
        {
            switch (item.itemName)
            {
                case "Axe":
                    Global.Instance.currentEquippedItem = PlayerEquipment.Axe;
                    break;
                case "Shovel":
                    Global.Instance.currentEquippedItem = PlayerEquipment.Shovel;
                    break;
                case null:
                    Global.Instance.currentEquippedItem = PlayerEquipment.Hand;
                    break;
                default:
                    Global.Instance.currentEquippedItem = PlayerEquipment.Hand;
                    break;
                
            }
            
            // GD.Print(Global.Instance.currentEquippedItem);
        }

       
    }
}