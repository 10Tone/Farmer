using Godot;

namespace Farmer.Scripts.Main.Game.FarmWorld.Farming.Crops
{
    public class CropsManager: Node2D
    {
        [Export] private PackedScene _cropPackedScene; // TEMP

        public bool PlantCrop(Vector2 cropPosition, PackedScene cropPackedScene)
        {
            if (cropPackedScene == null)
            {
                GD.Print("CropsManager: cropPackedScene is null");
                return false;
            }

            if (!CheckForCropAtPosition(cropPosition))
            {
                var crop = cropPackedScene.Instance() as Node2D;
                AddChild(crop);
                if(crop != null ) crop.Position = cropPosition;
                return true;
            }
            else
            {
                return false;
            }
           
        }

        public void UpdateCropsGrowth()
        {
            foreach (Node2D cropNode in GetChildren())
            {
                var crop = cropNode as Crop;
                crop?.GrowPlant();
            }
        }

        bool CheckForCropAtPosition(Vector2 pos)
        {
            foreach (Node2D child in GetChildren())
            {
                if (child.Position == pos)
                {
                    // GD.Print("crop already planted");
                    return true;
                }
            }
            return false;
        }
    }
}