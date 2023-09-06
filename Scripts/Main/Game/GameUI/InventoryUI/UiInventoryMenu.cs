using Farmer.Scripts.Main.Game.Database;
using Farmer.Scripts.Main.Game.GameUI.InventoryUI.CraftingMenu;
using Godot;

namespace Farmer.Scripts.Main.Game.GameUI.InventoryUI
{
    public class UiInventoryMenu: Control
    {
        [Export()] private NodePath _craftingItemContNodePath;
        [Export()] private NodePath _craftingMenuInfoNodePath;

        private GridContainer _craftingItemGroGridContainer;
        private CraftingMenuInfo _craftingMenuInfo;

        public override void _Ready()
        {
            base._Ready();
            _craftingItemGroGridContainer = GetNode<GridContainer>(_craftingItemContNodePath);
            _craftingMenuInfo = GetNode<TextureRect>(_craftingMenuInfoNodePath) as CraftingMenuInfo;

            foreach (var child in _craftingItemGroGridContainer.GetChildren())
            {
                CraftingItemButton itemSlot = child as CraftingItemButton;
                itemSlot.Connect("CraftingItemButtonPressed", this, nameof(OnCraftingItemButtonPressed));
            }
        }

        void _on_ExitTextureButton_pressed()
        {
            GD.Print("ExitButton Pressed");
        }

        void OnCraftingItemButtonPressed(ItemResource item)
        {
            _craftingMenuInfo.UpdateCraftingMenuInfo(item);
        }
    }
}