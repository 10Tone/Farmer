using Farmer.Scripts.Main.Game.Autoload;
using Farmer.Scripts.Main.Game.Collectibles;
using Godot;

namespace Farmer.Scripts.Main.Game.FarmWorld.Farming.Crops
{
    public class Crop: Node2D
    {
        [Export] private PackedScene _plantCollectiblePackedScene;
        [Export()] private NodePath _cropSpriteNodePath;
        private Sprite _cropSprite;

        private float _growStages = 6;

        private float _currentGrowStage = 0;
        private bool finalStageReached = false;

        public override void _Ready()
        {
            _cropSprite = GetNode<Sprite>(_cropSpriteNodePath);
        }

        public void GrowPlant()
        {
            if (finalStageReached) return;
            
            if (_currentGrowStage >= (_growStages - 1) * 16)
            {
                // GD.Print(_currentGrowStage);
                finalStageReached = true;
                SpawnCollectible();
                CallDeferred("queue_free");
            }
            
            if (finalStageReached) return;

            _currentGrowStage = _currentGrowStage + 16;
            _cropSprite.RegionRect = new Rect2(new Vector2(_currentGrowStage,0), new Vector2(16,16) );
        }

        void SpawnCollectible()
        {
            Collectible plant = _plantCollectiblePackedScene.Instance() as Collectible;
            Global.Instance.collectiblesManager.AddChild(plant);
            plant.Position = Position;
        }
    }
}