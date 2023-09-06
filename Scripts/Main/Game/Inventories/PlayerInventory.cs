using Farmer.Scripts.Main.Game.Autoload;
using Farmer.Scripts.Main.Game.Database;
using Farmer.Scripts.Main.Game.Equipment;
using Farmer.Scripts.Main.Game.GameUI.InventoryUI;
using Godot;

namespace Farmer.Scripts.Main.Game.Inventories
{
    public class PlayerInventory: Inventory
    {
        [Export()]private NodePath _hotbarUiNodePath;
        private HotbarUi _hotbarUi;

        public ItemResource hotbarSlotItem;

        [Signal]
        private delegate void UpdateHotBarSelected();

        public override void _Ready()
        {
            base._Ready();
            
            _hotbarUi = GetNode<Control>(_hotbarUiNodePath) as HotbarUi;
            _hotbarUi.Connect("HotbarSlotSelected", this, nameof(OnHotbarSlotSelected));

            // TEMP
            for (int i = 0; i < ItemDatabase.Instance.items.Count; i++)
            {
                if (ItemDatabase.Instance.items[i].itemName == "Shovel")
                {
                    AddItem(ItemDatabase.Instance.items[i].itemId);
                }
                if (ItemDatabase.Instance.items[i].itemName == "Axe")
                {
                    AddItem(ItemDatabase.Instance.items[i].itemId);
                }
                if (ItemDatabase.Instance.items[i].itemName == "Plant")
                {
                    AddItem(ItemDatabase.Instance.items[i].itemId);
                }

                if (ItemDatabase.Instance.items[i].itemName == "Wood")
                {
                    for (int j = 0; j < 40; j++)
                    {
                        AddItem(ItemDatabase.Instance.items[i].itemId);
                    }
                }
                
            }

            hotbarSlotItem = inventory[0];
        }

        public override void _Process(float delta)
        {
            base._Process(delta);
            
            UpdateHotbarUi();
        }

        // update when picking up item
        public void UpdateHotbarUi()
        {
            if (_hotbarUi == null) return;
            for (int i = 0; i < _hotbarUi.uiHotbarSlots.Length; i++)
            {
                _hotbarUi.AddItemToUiSlot(i, inventory[i],inventory[i].stackAmount);
            }
        }

        public void AddItem(int id)
        {
            ItemResource itemToAdd = new ItemResource();

            while (itemToAdd.itemName == null)
            {
                for (int k = 0; k < ItemDatabase.Instance.items.Count; k++)
                {
                    if (ItemDatabase.Instance.items[k].itemId == id) // find the item in the database 
                    {
                        itemToAdd = new ItemResource();
                        itemToAdd.itemId = ItemDatabase.Instance.items[k].itemId;
                        itemToAdd.itemName = ItemDatabase.Instance.items[k].itemName;
                        itemToAdd.stackable = ItemDatabase.Instance.items[k].stackable;
                        itemToAdd.iconTexture = ItemDatabase.Instance.items[k].iconTexture;
                        itemToAdd.energyBoostAmount = ItemDatabase.Instance.items[k].energyBoostAmount;
                        itemToAdd.itemType = ItemDatabase.Instance.items[k].itemType;
                        itemToAdd.stackAmount = ItemDatabase.Instance.items[k].stackAmount;
                        itemToAdd.maxStackSize = ItemDatabase.Instance.items[k].maxStackSize;
                        itemToAdd.cropPackedScene = ItemDatabase.Instance.items[k].cropPackedScene;
                    }
                }

                break;
            }
            

            for (var i = 0; i < inventory.Count; i++)
            {
                if (i > inventory.Count)
                {
                    GD.Print("Inventory full");
                    return;
                }

                if (inventory[i].itemId != itemToAdd.itemId || !itemToAdd.stackable) continue;
                if (inventory[i].stackAmount >= inventory[i].maxStackSize) continue;
                ++inventory[i].stackAmount;
                EmitSignal(nameof(InventoryChanged));
                return;
            }

            for (var i = 0; i < inventory.Count; i++)
            {
                if (i > inventory.Count)
                {
                    GD.Print("Inventory full");
                    return;
                }


                if (inventory[i].itemName != null) continue;
                // GD.Print(  itemToAdd.itemName+ " not found in inventory and added as new");
                inventory[i] = itemToAdd; // add to inventory
                EmitSignal(nameof(Inventory.InventoryChanged));
                return;
            }
            
        }

        void OnHotbarSlotSelected(int index)
        {
            if (inventory[index].itemName == null)
            {
                hotbarSlotItem = new ItemResource();
                EmitSignal(nameof(UpdateHotBarSelected), hotbarSlotItem);
                return;
            }
            
            hotbarSlotItem = inventory[index];
            // GD.Print(hotbarSlotItem.energyBoostAmount);
            EmitSignal(nameof(UpdateHotBarSelected), hotbarSlotItem);
            // GD.Print("Selected item in hotbar is " + hotbarSlotItem.itemName);
        }
        
    }
}