using Farmer.Scripts.Main.Game.Autoload;
using Farmer.Scripts.Main.Game.Collectibles;
using Farmer.Scripts.Main.Game.Database;
using Farmer.Scripts.Main.Game.Equipment;
using Farmer.Scripts.Main.Game.FarmWorld.Farming.Crops;
using Farmer.Scripts.Main.Game.FarmWorld.TileMaps;
using Farmer.Scripts.Main.Game.Interactibles;
using Godot;

namespace Farmer.Scripts.Main.Game.FarmWorld
{
    public class FarmWorldManager: Node2D
    {
        [Export()] private NodePath _tileMapManagerNodePath;
        [Export()] private NodePath _cropsManagerNodePath; // temp, replace for class that keeps track of plants that are planted
        [Export()] private NodePath _collectiblesManagerNodePath;
        [Export()] private NodePath _interactiblesManagerNodePath;

        private InteractibleObjects _currentInteractibleObject;
        
        public TileMapManager tileMapManager;
        public CropsManager cropsManager;
        public CollectiblesManager collectiblesManager;
        public InteractiblesManager interactiblesManager;

        [Signal]
        private delegate void ItemUsed();

        public override void _Ready()
        {
            tileMapManager = GetNode<Node2D>(_tileMapManagerNodePath) as TileMapManager;
            cropsManager = GetNode<Node2D>(_cropsManagerNodePath) as CropsManager;
            collectiblesManager = GetNode<Node2D>(_collectiblesManagerNodePath) as CollectiblesManager;
            interactiblesManager = GetNode<Node2D>(_interactiblesManagerNodePath) as InteractiblesManager;

            // interactiblesManager.Connect("InteractWithObject", this, nameof(OnInteractWithObject));

            _currentInteractibleObject = InteractibleObjects.Stone;
        }

        public void CreateFarmSoil(Vector2 mousePos)
        {
            // GD.Print("Create soil");
            Vector2 mapPosition = tileMapManager.MouseToMapPosition(mousePos);
            tileMapManager.farmSoilTileMap.AddSoilTile(mapPosition);
        }

        public void PlantSeed(Vector2 mousePos, ItemResource seedItem)
        {
            // check if there is already a planted crop at the position
            
            Vector2 mapPosition = tileMapManager.MouseToMapPosition(mousePos);
            Vector2 gridPosition = tileMapManager.MouseToGridPosition(mousePos);
            if (tileMapManager.farmSoilTileMap.CheckForFarmSoil(mapPosition))
            {
                if (seedItem.stackAmount > 0)
                {
                    if (cropsManager.PlantCrop(gridPosition, seedItem.cropPackedScene))
                    {
                        EmitSignal(nameof(ItemUsed), seedItem);
                    }
                }
                else
                {
                    Global.Instance.currentEquippedItem = PlayerEquipment.Hand;
                }
            }
        }
        
        // TEMP: outside this class: should be usable in any world
        // public void InteractWithFarmWorld(Vector2 mousePos, ItemResource item)
        // {
        //     // check for tool or seed selected in inventory and for distance and select action: all done outside of this class
        //     Vector2 mapPosition = tileMapManager.MouseToMapPosition(mousePos);
        //     Vector2 gridPosition = tileMapManager.MouseToGridPosition(mousePos);
        //     // GD.Print("interact with world");
        //
        //     switch (item.itemName)
        //     {
        //         case "Shovel":
        //             // play shovel animation
        //             // tileMapManager.farmSoilTileMap.AddSoilTile(mapPosition);
        //             break;
        //         case "Axe":
        //             if (_currentInteractibleObject == InteractibleObjects.Tree)
        //             {
        //                 GD.Print("Chop Tree");
        //             }
        //             break;
        //         case "Seed":
        //             if (tileMapManager.farmSoilTileMap.CheckForFarmSoil(mapPosition))
        //             {
        //                 if (item.stackAmount > 0)
        //                 {
        //                     cropsManager.PlantCrop(gridPosition, item.cropPackedScene);
        //                     EmitSignal(nameof(ConsumeItem), item);
        //                 }
        //             }
        //             break;
        //     }
        //     
        // }

        // void OnInteractWithObject(InteractibleObjects currentObj)
        // {
        //     _currentInteractibleObject = currentObj;
        // }
    }
}