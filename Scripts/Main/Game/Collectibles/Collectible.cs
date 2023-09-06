using Farmer.Scripts.Main.Game.Database;
using Godot;

namespace Farmer.Scripts.Main.Game.Collectibles
{
    public class Collectible: Area2D
    {
        [Export()] private string _resourceRefName;
        private ItemResource _itemResourceReference;
        
        [Signal]
        private delegate void PlayerPickedUpCollectible();

        public override void _Ready()
        {
            if (_resourceRefName != null)
            {
                _itemResourceReference = ItemDatabase.Instance.GetItem(_resourceRefName);
            }
            else
            {
                GD.PushWarning("Collectible: resource ref name missing!");
            }

        }
        

        void _on_Collectible_body_entered(Node body)
        {
            CollectiblesManager collectiblesManager = GetParent<Node2D>() as CollectiblesManager;
            if (collectiblesManager == null)
            {
                GD.PushWarning("Collectible: no collectiblesmanager parent found!");
                return;
            }
          
            Connect(nameof(PlayerPickedUpCollectible), collectiblesManager, "OnPlayerPickedUpCollectible");
            // GD.Print(body.Name + " entered " + _itemResourceReference.itemName + " collectible");
            EmitSignal(nameof(PlayerPickedUpCollectible), _itemResourceReference.itemId);
            CallDeferred("queue_free");
            
        }
    }
}