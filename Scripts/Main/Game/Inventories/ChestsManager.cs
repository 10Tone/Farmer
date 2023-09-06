using Godot.Collections;
using Farmer.Scripts.Main.Game.Interactibles;
using Godot;

namespace Farmer.Scripts.Main.Game.Inventories
{
    public class ChestsManager: Node2D
    {
        [Export()] private PackedScene _chestPackedScene;

        [Signal]
        private delegate void OpenChestInventory();

        public Inventory AddChestToWorld(int inventoryIndexId, Vector2 pos)
        {
            Chest chest = _chestPackedScene.Instance() as Chest;
            AddChild(chest);
            chest.Position = pos;
            chest.Inventory.InventoryId = inventoryIndexId;

            chest.Connect("OpenChestInventory", this, nameof(OnOpenChestInventory));

            return chest.Inventory;
        }

        void OnOpenChestInventory(int inventoryId)
        {
            // GD.Print("open chest with id " + chestId);
            EmitSignal(nameof(OpenChestInventory), inventoryId);
        }
    }
}