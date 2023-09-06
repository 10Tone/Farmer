using Farmer.Scripts.Main.Game.Inventories;
using Godot;
using Godot.Collections;

namespace Farmer.Scripts.Main.Game.Interactibles
{
    public class Chest: Area2D
    {
        [Export()] private NodePath _staticBodyNodePath;
        [Export()] private NodePath _inventoryNodePath;

        private Inventory _inventory;

        public Inventory Inventory
        {
            get => _inventory;
            set => _inventory = value;
        }

        private bool _playerCanInteract;

        [Signal]
        private delegate void OpenChestInventory();
        

        public override void _Ready()
        {
            _inventory = GetNode<Node2D>(_inventoryNodePath) as Inventory;
           
        }

        public void ChestClicked()
        {
            if (!_playerCanInteract) return;
            // GD.Print("player entered");
            EmitSignal(nameof(OpenChestInventory), _inventory.InventoryId);
        }

        void _on_Chest_body_entered(Node body)
        {
            if (body.IsInGroup("Player"))
            {
                _playerCanInteract = true;
                
            }
        }

        void _on_Chest_body_exited(Node body)
        {
            if (body.IsInGroup("Player"))
            {
                _playerCanInteract = false;
            }
        }
        
    }
}