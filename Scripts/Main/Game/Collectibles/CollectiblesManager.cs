using Farmer.Scripts.Main.Game.Autoload;
using Farmer.Scripts.Main.Game.Database;
using Godot;
using Godot.Collections;

namespace Farmer.Scripts.Main.Game.Collectibles
{
    public class CollectiblesManager: Node2D
    {
        [Signal]
        private delegate void PlayerPickedUpCollectible();

        public override void _Ready()
        {
            base._Ready();
            Global.Instance.collectiblesManager = this;
        }

        void OnPlayerPickedUpCollectible(int id)
        {
            
            EmitSignal(nameof(PlayerPickedUpCollectible), id);
        }
        
    }
}