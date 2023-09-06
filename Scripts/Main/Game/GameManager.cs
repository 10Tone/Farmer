using Farmer.Scripts.Main.Game.Autoload;
using Farmer.Scripts.Main.Game.Characters;
using Farmer.Scripts.Main.Game.CraftingSystem;
using Farmer.Scripts.Main.Game.Database;
using Farmer.Scripts.Main.Game.Equipment;
using Farmer.Scripts.Main.Game.FarmWorld;
using Farmer.Scripts.Main.Game.Interactibles;
using Farmer.Scripts.Main.Game.Inventories;
using Farmer.Scripts.Main.Game.TimeManagement;
using Godot;
using Godot.Collections;

namespace Farmer.Scripts.Main.Game
{
    public class GameManager: Node2D
    {
        [Export()] private NodePath _farmWorldManagerNodePath; // replace for packedscene and instantiate
        [Export()] private NodePath _playerNodePath;
        [Export()] private NodePath _mouseSelectorNodePath;
        [Export()] private NodePath _mouseControllerNodePath;
        [Export()] private NodePath _inventoryManagerNodePath;
        [Export()] private NodePath _dayCycleNodePath;

        [Export()] private NodePath _craftingManagerNodePath;
        // [Export()] private NodePath _equipmentManagerNodePath;

        private FarmWorldManager _farmWorldManager;
        private Player _player;
        public MouseSelector mouseSelector;
        private MouseController _mouseController;
        private EquipedItemSelector _equipedItemSelector;
        private DayCycle _dayCycle;
        private InventoryManager _inventoryManager;
        private CraftingManager _craftingManager;

        private InteractibleObjects _currentInteractibleObject;

        public override void _Ready()
        {
            _farmWorldManager = GetNode<Node2D>(_farmWorldManagerNodePath) as FarmWorldManager;
            _player = GetNode<KinematicBody2D>(_playerNodePath) as Player;
            // mouseSelector = GetNode<Node2D>(_mouseSelectorNodePath) as MouseSelector;
            _mouseController = GetNode<Node2D>(_mouseControllerNodePath) as MouseController;
            _inventoryManager = GetNode<Node2D>(_inventoryManagerNodePath) as InventoryManager;
            _dayCycle = GetNode<Node2D>(_dayCycleNodePath) as DayCycle;
            _equipedItemSelector = new EquipedItemSelector();
            _craftingManager = GetNode<Node2D>(_craftingManagerNodePath) as CraftingManager;

            ConnectToChildSignals();
            _equipedItemSelector.UpdateEquippedItem(_inventoryManager.playerInventory.hotbarSlotItem);
        }

        private void ConnectToChildSignals()
        {
            _mouseController.Connect("MouseMotionEvent", this, nameof(OnMouseMotionEvent));
            _mouseController.Connect("LeftMouseButtonEvent", this, nameof(OnLeftMouseButtonEvent));
            _inventoryManager.Connect("OpeningInventory", this, nameof(OnOpeningInventory));
            _dayCycle.Connect("DayPassedEvent", this, nameof(OnDayPassedEvent));
            _farmWorldManager.collectiblesManager.Connect("PlayerPickedUpCollectible", this,
                nameof(OnPlayerPickedUpCollectible));
            _farmWorldManager.Connect("ItemUsed", this, nameof(OnItemUsed));
            _inventoryManager.playerInventory.Connect("UpdateHotBarSelected", this, nameof(OnUpdateHotBarSelected));
        }

        void OnMouseMotionEvent()
        {
            // GD.Print("gamemanager: on mouse motion");
            // GD.Print(FarmWorldManager.TileMapManager.MouseToPlayerPixelDistance(mousePos, Player.Position));
        }

        public override void _Process(float delta)
        {
            base._Process(delta);
           

            if (Input.IsActionJustPressed("ui_consume"))
            {
                if (_inventoryManager.playerInventory.hotbarSlotItem.itemType == ItemResourceType.Consumable)
                {
                    if (_inventoryManager.playerInventory.ConsumeItem(_inventoryManager.playerInventory.hotbarSlotItem))
                    {
                        // GD.Print("Consume item");
                        _player.UpdateEnergyAmount(0, _inventoryManager.playerInventory.hotbarSlotItem.energyBoostAmount);
                    }
                    // update 
                }
            }

            if (Input.IsActionJustPressed("ui_craft"))
            {
                ItemResource craftItem =
                    _craftingManager.CraftItem("ChestRecipe", _inventoryManager.playerInventory.inventory);
                if (craftItem != null)
                {
                    _inventoryManager.playerInventory.AddItem(craftItem.itemId);
                }
                
                
            }
        }
        
        //TEMP: move to keyboardinput controller script
        

        void OnLeftMouseButtonEvent()
        {
            Vector2 mousePos = GetGlobalMousePosition();

            var currentInteractibleBody = ScanForInteractibleObjects();

            // Move to interactible manager?
            switch (_currentInteractibleObject)
            {
                case InteractibleObjects.Null:
                    return;
                
                case InteractibleObjects.Chest:
                    
                    Chest chest = currentInteractibleBody.GetParent() as Chest;
                    if (chest == null) return;
                    chest.ChestClicked();
                    break;
                
                case InteractibleObjects.Tree:

                    Farmer.Scripts.Main.Game.Interactibles.Tree tree = currentInteractibleBody.GetParent() as Farmer.Scripts.Main.Game.Interactibles.Tree;
                    if (tree == null) return;

                    if (Global.Instance.currentEquippedItem == PlayerEquipment.Axe)
                    {
                        tree.ChopDownTree();
                        _player.UpdateEnergyAmount(1, 0);
                    }
                    break;
                
                case InteractibleObjects.Stone:
                    break;
                
                case InteractibleObjects.Soil:
                    
                    if (_inventoryManager.playerInventory.hotbarSlotItem.itemType == ItemResourceType.Seed)
                    {
                        if (_farmWorldManager.tileMapManager.MouseToPlayerPixelDistance(mousePos, _player.Position) <= 23)
                        {
                            _farmWorldManager.PlantSeed(mousePos, _inventoryManager.playerInventory.hotbarSlotItem);
                            _player.UpdateEnergyAmount(0.5f, 0);
                        }

                        break;
                    }

                    if (Global.Instance.currentEquippedItem == PlayerEquipment.Shovel)
                    {
                        _farmWorldManager.CreateFarmSoil(mousePos);
                        _player.UpdateEnergyAmount(1, 0);
                    }

                    break;

            }
        }

        
        void OnDayPassedEvent()
        {
            _farmWorldManager.cropsManager.UpdateCropsGrowth();
        }

        void OnPlayerPickedUpCollectible(int id)
        {
            _inventoryManager.AddToPlayerInventory(id);
        }

        void OnItemUsed(ItemResource item)
        {
            _inventoryManager.playerInventory.ConsumeItem(item);
        }

        void OnOpeningInventory(bool opening)
        {
            GetTree().Paused = opening;
        }

        void OnUpdateHotBarSelected(ItemResource item)
        {
            if (item.itemType == null) return;
            _equipedItemSelector.UpdateEquippedItem(item);
        }
        private StaticBody2D ScanForInteractibleObjects()
        {
            var mousePos = GetGlobalMousePosition();
            var spaceState = GetWorld2d().DirectSpaceState;
            var result = spaceState.IntersectRay(mousePos, mousePos * 200, new Array());
        
            StaticBody2D interactibleObject;
            if (result.Count > 0)
            {
                interactibleObject = result["collider"] as StaticBody2D;
                if (interactibleObject != null)
                {
                    InteractibleObjFlag objFlag = interactibleObject as InteractibleObjFlag;
                    _currentInteractibleObject = objFlag.interactibleObjectFlag;
                    return interactibleObject;
                }
            }

            _currentInteractibleObject = InteractibleObjects.Soil;
            return null;
        }
    }
}