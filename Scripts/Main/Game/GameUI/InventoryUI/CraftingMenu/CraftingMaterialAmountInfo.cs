using Godot;

namespace Farmer.Scripts.Main.Game.GameUI.InventoryUI.CraftingMenu
{
    public class CraftingMaterialAmountInfo: HBoxContainer
    {
        [Export()] private NodePath _iconNodePath;
        [Export()] private NodePath _labelNeedAmountNodePath;
        [Export()] private NodePath _labelInventoryAmountNodePath;

        private TextureRect _iconTextureRect;
        private Label _needAmountLabel;
        private Label _inventoryAmountLabel;

        public override void _Ready()
        {
            base._Ready();
            _iconTextureRect = GetNode<TextureRect>(_iconNodePath);
            _needAmountLabel = GetNode<Label>(_labelNeedAmountNodePath);
            _inventoryAmountLabel = GetNode<Label>(_labelInventoryAmountNodePath);
        }

        public void UpdateCraftingMaterialInfo(Texture icon, string needAmount, string inventoryAmount)
        {
            _iconTextureRect.Texture = icon;
            _needAmountLabel.Text = needAmount;
            _inventoryAmountLabel.Text = inventoryAmount;
        }
    }
}