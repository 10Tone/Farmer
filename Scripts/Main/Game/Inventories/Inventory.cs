using Farmer.Scripts.Main.Game.Database;
using Farmer.Scripts.Main.Game.GameUI.InventoryUI;
using Godot;
using Godot.Collections;

namespace Farmer.Scripts.Main.Game.Inventories
{
    public class Inventory : Node2D
    {
        [Export()] protected int slotAmount = 30;
        [Export()] public InventoryType inventoryType;
        [Export()] private PackedScene _inventoryUiScene;
        
        protected InventoryUi inventoryUi;
        protected ItemResource selectedSlotItem;

        public Dictionary<int, ItemResource> inventory = new Dictionary<int, ItemResource>();
        public ItemResource SelectedSlotItem => selectedSlotItem;
        public int InventoryId { get; set; }

        private bool _inventoryUiLoaded = false;
        public bool InventoryUiLoaded => _inventoryUiLoaded;

        [Signal]
        protected delegate void InventoryChanged();

        [Signal]
        protected delegate void MouseEnteredUiSlot();
        
        
        
        public override void _Ready()
        {
            for (int i = 0; i < (slotAmount); i++)
            {
                inventory[i] = new ItemResource();
            }

            selectedSlotItem = inventory[0];
        }

        public override void _Process(float delta)
        {
            if (_inventoryUiLoaded)
            {
                UpdateInventoryUi();
                // GD.Print("update ui");
            }
        }

        public void LoadInventoryUi(Node parent)
        {
            inventoryUi = _inventoryUiScene.Instance() as InventoryUi;
            parent.AddChild(inventoryUi);
            inventoryUi.Connect("MouseEnteredSlot", this, nameof(OnMouseEnteredUiSlot));
            _inventoryUiLoaded = true;
        }

        public void FreeInventoryUi()
        {
            _inventoryUiLoaded = false;
            inventoryUi.Disconnect("MouseEnteredSlot", this, nameof(OnMouseEnteredUiSlot));
            inventoryUi.CallDeferred("queue_free");
        }

        protected void OnMouseEnteredUiSlot(int slotIndex)
        {
            if (inventory[slotIndex].itemName != null)
            {
                selectedSlotItem = inventory[slotIndex]; 
                // GD.Print("slot has item");
            }
            else
            {
                selectedSlotItem = new ItemResource(); 
                // GD.Print("slot is empty");
            }
            EmitSignal(nameof(MouseEnteredUiSlot), slotIndex, InventoryId, inventoryType);
        }

        // update when ui loaded and when item moved
        public void UpdateInventoryUi()
        {
            if(inventoryUi == null) return;
            // GD.Print("trying to update ui");
            
            for (int j = 0; j < inventoryUi.uiSlots.Length; j++)
            {
                inventoryUi.AddItemToUiSlot(j, inventory[j], inventory[j].stackAmount);
            }
        }
        
        public void MoveItem(ItemResource item, int index)
        {
            inventory[index] = item;
            selectedSlotItem = item;
            // GD.Print(inventory[index].itemName);
        }

        public bool ConsumeItem(ItemResource item)
        {
            bool consumed = false;
            
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].itemId == item.itemId)
                {
                    if (inventory[i].stackable)
                    {
                        if (inventory[i].stackAmount > 0)
                        {
                            --inventory[i].stackAmount;
                            consumed = true;
                            if (inventory[i].stackAmount <= 0)
                            {
                                inventory[i] = new ItemResource();
                            }
                        }
                        else
                        {
                            inventory[i] = new ItemResource();
                        }
                        
                    }
                    else
                    {
                        inventory[i] = new ItemResource();
                        consumed = true;
                    }
                    
                    break;
                }
            }

            return consumed;
        }

        public void RemoveItem(int index)
        {
            inventory[index] = new ItemResource();
        }

        protected bool InventoryContains(int id)
        {
            bool result = false;
            for (int i = 0; i < inventory.Count; i++)
            {
                result = inventory[i].itemId == id;
                if (result) break;
            }

            return result;
        }
    }
}