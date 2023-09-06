using Godot;

namespace Farmer.Scripts.Main.Game.Database
{
    public class ItemResource : Resource
    {
        [Export] public string itemName;
        [Export] public int itemId; 
        [Export] public bool stackable;
        [Export] public int stackAmount = 1;
        [Export] public int maxStackSize = 1;
        [Export] public int energyBoostAmount;
        [Export] public ItemResourceType itemType;
        [Export] public Texture iconTexture;
        [Export] public PackedScene cropPackedScene;

    }
}