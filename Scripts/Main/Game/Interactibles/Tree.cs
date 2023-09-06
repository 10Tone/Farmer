using Farmer.Scripts.Main.Game.Autoload;
using Farmer.Scripts.Main.Game.Characters;
using Farmer.Scripts.Main.Game.Collectibles;
using Farmer.Scripts.Main.Game.Equipment;
using Godot;

namespace Farmer.Scripts.Main.Game.Interactibles
{
    public class Tree: InteractibleObject
    {
        [Export()] private NodePath _animPlayerNodePath;
        private AnimationPlayer _animationPlayer;
        
        private int _health = 5;
        private int _woodSpawnAmount = 5;
        [Export()] private PackedScene _woodCollectiblePackedScene;
        
        public override void _Ready()
        {
            base._Ready();
            thisInteractibleObject = InteractibleObjects.Tree;
            neededEquipmentForInteraction = PlayerEquipment.Axe;

            _animationPlayer = GetNode<AnimationPlayer>(_animPlayerNodePath);
        }

        public void ChopDownTree()
        {
            if (!playerCanInteract) return;
            
            if (_health <= 1)
            {
                
                // start animation
                // start timer
                // instantiate wood
                // remove (free) tree

                SpawnWood();

                CallDeferred("queue_free");
            }
            --_health;
            _animationPlayer.Play("TreeShake");
            // GD.Print("Chop! Health is " + _health);
        }

        private void SpawnWood()
        {
            //TEMP
            int xPos = 0;
            int yPos = 0;

            for (int i = 0; i < _woodSpawnAmount; i++)
            {
                Collectible wood = _woodCollectiblePackedScene.Instance() as Collectible;
                Global.Instance.collectiblesManager.AddChild(wood);
                wood.Position = Position + new Vector2(xPos, yPos);
                xPos = xPos + 8;
                yPos = yPos + 4;
            }
        }
    }
}