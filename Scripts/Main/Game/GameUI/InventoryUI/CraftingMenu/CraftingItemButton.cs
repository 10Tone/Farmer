using Farmer.Scripts.Main.Game.Database;
using Godot;

namespace Farmer.Scripts.Main.Game.GameUI.InventoryUI.CraftingMenu
{
    public class CraftingItemButton: TextureButton
    {
        [Export()] private NodePath _itemIconNodePath;

        private TextureRect _itemIcon;
        [Export()]private ItemResource _itemRef;

        [Signal]
        private delegate void CraftingItemButtonPressed(ItemResource itemResource);

        public override void _Ready()
        {
            _itemIcon = GetNode<TextureRect>(_itemIconNodePath);

            if (_itemRef.itemName != null)
            {
                _itemIcon.Texture = _itemRef.iconTexture;
            }
            
        }

        void _on_CraftingItemButton_pressed()
        {
            EmitSignal(nameof(CraftingItemButtonPressed), _itemRef);
        }
    }
}