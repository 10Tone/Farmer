using System.Collections.Generic;
using Farmer.Scripts.Main.Game.Database;
using Farmer.Scripts.Main.Game.GameUI.InventoryUI;
using Godot;

namespace Farmer.Scripts.Main.Game.Inventories
{
    public class InventoryManager : Node2D
    {
        [Export] private PackedScene _mouseMoveItemIconPackedScene;
        [Export()] private PackedScene _inventoryMenuUiPackedScene;
        [Export()] private NodePath _playerInventoryNodePath;
        [Export()] private NodePath _chestsManagerNodePath;

        private List<Inventory> _inventories = new List<Inventory>();
        private InventoryType _inventoryType;
        private ItemResource _itemToMove;
        private MouseMoveItem _mouseMoveItemIcon;
        private CanvasLayer _mouseMoveItemLayer;
        public PlayerInventory playerInventory;
        private Node _inventoriesUiParent;
        private CanvasLayer _inventoryMenuUi;
        private ChestsManager _chestsManager;
        
        private bool _itemPickedUpForMove;
        private bool _leftMouseButtonPressed;
        private bool _playerInventoryLoaded;
        private bool _mouseMoveItemIconLoaded;
        private bool _inventoryLoaded;
        
        private int _mouseOnSlotIndex;
        private int _selectedInventory;
        private int _swapIndex;

        [Signal]
        private delegate void OpeningInventory();

        public override void _Ready()
        {
            playerInventory = GetNode<Node2D>(_playerInventoryNodePath) as PlayerInventory;
            _chestsManager = GetNode<Node2D>(_chestsManagerNodePath) as ChestsManager;
            _chestsManager.Connect("OpenChestInventory", this, nameof(OnOpenChestInventory));

            if (playerInventory != null)
            {
                _inventories.Add(playerInventory);
                playerInventory.InventoryId = 0;
                
            }
            
            //TEMP
            _inventories.Add(_chestsManager.AddChestToWorld(_inventories.Count, new Vector2(70,70)));
            _inventories.Add(_chestsManager.AddChestToWorld(_inventories.Count, new Vector2(100,70)));
        }
        
        public override void _Process(float delta)
        {
            if (Input.IsActionJustPressed("ui_inventory"))
            {
                if (!_inventoryLoaded)
                {
                    OpenInventory(0);
                }
                else
                {
                    CloseInventory();
                }
            }
            
            // else if opening chest or crafting table, do stuff

            if (_mouseMoveItemIconLoaded)
            {
                if (_itemPickedUpForMove)
                    _mouseMoveItemIcon.ShowItemMouseIcon(_itemToMove.iconTexture, _itemToMove.stackAmount);
                else
                    _mouseMoveItemIcon.HideItemMouseIcon();
            }
            
            // GD.Print(_selectedInventory);
            
        }

        public override void _Input(InputEvent @event)
        {
            if (!_inventoryLoaded) return;

            if (@event is InputEventMouseButton eventMouseButton)
            {
                if (eventMouseButton.ButtonIndex == 1) // TODO move 1 item
                {
                    if (eventMouseButton.Pressed)
                    {
                        _leftMouseButtonPressed = true;

                        if(_itemPickedUpForMove) // if there is an item picked up for move
                        {
                            if(ItemInSlot())  // if there is already an item in the selected slot
                            {
                                var itemToSwap = _inventories[_selectedInventory].SelectedSlotItem;
                                

                                if (itemToSwap.itemId != _itemToMove.itemId) // if it is not the same item
                                {
                                    _inventories[_selectedInventory].MoveItem(_itemToMove, _mouseOnSlotIndex);
                                    _itemToMove = itemToSwap;
                                    _itemPickedUpForMove = true;
                                }
                                else if (_itemToMove.stackable) // if the item is stackable
                                {
                                    if (_swapIndex != _mouseOnSlotIndex)
                                    {
                                        GD.Print("item comes from other index");
                                        _inventories[_selectedInventory].SelectedSlotItem.stackAmount =
                                            _itemToMove.stackAmount + _inventories[_selectedInventory]
                                                .SelectedSlotItem.stackAmount;
                                        _itemToMove = new ItemResource();
                                        _itemPickedUpForMove = false;
                                    }
                                    else
                                    {
                                        GD.Print("item comes from same index");
                                        // return;
                                    }
                                    
                                }
                            }
                            else // if there is no item in selected slot
                            {
                                _inventories[_selectedInventory].MoveItem(_itemToMove, _mouseOnSlotIndex);
                                _itemToMove = new ItemResource();
                                _itemPickedUpForMove = false;
                                
                                // GD.Print("Item moved to " + _mouseOnSlotIndex);
                                
                            }
                           
                        }
                        
                        else // if no item is picked up for move
                        {
                            if (ItemInSlot()) // if there is already an item in the selected slot
                            {
                                _itemToMove = _inventories[_selectedInventory].SelectedSlotItem;
                                
                                
                                if(_itemToMove.stackable )
                                {
                                    if (_itemToMove.stackAmount >= 2)
                                    {
                                        --_inventories[_selectedInventory].SelectedSlotItem.stackAmount;
                                        _itemToMove = new ItemResource();
                                        _itemToMove.stackable = true;
                                        _itemToMove.iconTexture = _inventories[_selectedInventory].SelectedSlotItem
                                            .iconTexture;
                                        _itemToMove.itemId = _inventories[_selectedInventory].SelectedSlotItem.itemId;
                                        _itemToMove.itemName =
                                            _inventories[_selectedInventory].SelectedSlotItem.itemName;
                                        _itemToMove.itemType =
                                            _inventories[_selectedInventory].SelectedSlotItem.itemType;
                                        _itemToMove.stackAmount = 1;
                                        _itemToMove.maxStackSize = _inventories[_selectedInventory].SelectedSlotItem
                                            .maxStackSize;
                                        _itemToMove.energyBoostAmount = _inventories[_selectedInventory]
                                            .SelectedSlotItem.energyBoostAmount;
                                        _itemToMove.cropPackedScene = _inventories[_selectedInventory].SelectedSlotItem
                                            .cropPackedScene;
                                    }
                                    else
                                    {
                                        _inventories[_selectedInventory].RemoveItem(_mouseOnSlotIndex);
                                        
                                    }
                                    
                                }
                                else 
                                {
                                    _inventories[_selectedInventory].RemoveItem(_mouseOnSlotIndex);
                                    
                                }
                                
                                _itemPickedUpForMove = true;
                                _swapIndex = _mouseOnSlotIndex;
                            }
                        }

                       
                    }
                    else
                    {
                        _leftMouseButtonPressed = false;
                    }
                }

                else if (eventMouseButton.ButtonIndex == 2) // TODO move full stack
                    if (eventMouseButton.Pressed)
                        
                        if (!_itemPickedUpForMove)
                        {
                            if (ItemInSlot()) // if there is already an item in the selected slot
                            {
                                _itemToMove = _inventories[_selectedInventory].SelectedSlotItem;


                                if (_itemToMove.stackable)
                                {
                                    _inventories[_selectedInventory].RemoveItem(_mouseOnSlotIndex);
                                    _itemPickedUpForMove = true;
                                    _swapIndex = _mouseOnSlotIndex;
                                }
                            }
                        }
            }

            // playerInventory.UpdateHotbarUi();
            // _inventories[_selectedInventory].UpdateInventoryUi();
        }

        void OnOpenChestInventory(int inventoryId)
        {
            OpenInventory(inventoryId);
        }
        
        void OpenInventory(int inventoryIndex)
        {
            // can be player inventory, chest or craftingtable
            
            // emit signal to gamemanager to pause game
            EmitSignal(nameof(OpeningInventory), true);
            // load inventory menu ui
            
            _inventoryMenuUi = _inventoryMenuUiPackedScene.Instance() as CanvasLayer;
            AddChild(_inventoryMenuUi);

            if (inventoryIndex == 0)
            {
                playerInventory.LoadInventoryUi(_inventoryMenuUi.GetChild(0));
                playerInventory.Connect("MouseEnteredUiSlot", this, nameof(OnMouseEnteredUiSlot));
            }
            else
            {
                playerInventory.LoadInventoryUi(_inventoryMenuUi.GetChild(0));
                playerInventory.Connect("MouseEnteredUiSlot", this, nameof(OnMouseEnteredUiSlot));
                _inventories[inventoryIndex].LoadInventoryUi(_inventoryMenuUi.GetChild(0));
                _inventories[inventoryIndex].Connect("MouseEnteredUiSlot", this, nameof(OnMouseEnteredUiSlot));
            }
            // load player inventory + other inventory
            
            AddItemMouseIcon();
            
            // _itemToMove = new ItemResource();
            _inventoryLoaded = true;
        }

        void CloseInventory()
        {
            // can be player inventory, chest or craftingtable
            FreeItemMouseIcon();
            _itemToMove = new ItemResource();
            // free player inventory + other inventory

            for (int i = 0; i < _inventories.Count; i++)
            {
                if (_inventories[i].InventoryUiLoaded)
                {
                    _inventories[i].Disconnect("MouseEnteredUiSlot", this, nameof(OnMouseEnteredUiSlot));
                    _inventories[i].FreeInventoryUi();
                }
                
            }
            // free inventory menu ui
            
            _inventoryMenuUi.CallDeferred("queue_free");
            
            // emit signal to gamemanager to un-pause game
            EmitSignal(nameof(OpeningInventory), false);
            _inventoryLoaded = false;
            
            

        }
        
        private void AddItemMouseIcon()
        {
            _mouseMoveItemLayer = new CanvasLayer();
            _mouseMoveItemLayer.Layer = 2;
            AddChild(_mouseMoveItemLayer);
            _mouseMoveItemIcon = _mouseMoveItemIconPackedScene.Instance() as MouseMoveItem;
            _mouseMoveItemLayer.AddChild(_mouseMoveItemIcon);
            _mouseMoveItemIconLoaded = true;
        }

        void FreeItemMouseIcon()
        {
            _mouseMoveItemIcon.CallDeferred("queue_free");
            _mouseMoveItemLayer.CallDeferred("queue_free");
            _mouseMoveItemIconLoaded = false;
        }

        private bool ItemInSlot()
        {
            return _inventories[_selectedInventory].SelectedSlotItem.itemName != null;
        }

        private void OnMouseEnteredUiSlot(int slotIndex, int inventoryId, InventoryType inventoryType)
        {
            // GD.Print("mouseEntered");
            _mouseOnSlotIndex = slotIndex;
            
            //TODO change this to using the index of inventories
            _selectedInventory = inventoryId;
            
            _inventoryType = inventoryType;
            // GD.Print("Mouse entered slot: " + slotIndex + " in inventory " + inventoryType);
        }
        
        public void AddToPlayerInventory(int id)
        {
            playerInventory.AddItem(id);
        }
    }
}