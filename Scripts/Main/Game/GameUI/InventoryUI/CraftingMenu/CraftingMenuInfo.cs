using Farmer.Scripts.Main.Game.Database;
using Godot;

namespace Farmer.Scripts.Main.Game.GameUI.InventoryUI.CraftingMenu
{
    public class CraftingMenuInfo: TextureRect
    {
        [Export()] private NodePath _itemNameLabelNodePath;
        [Export()] private NodePath _materialOneNodePath;
        [Export()] private NodePath _materialTwoNodePath;
        [Export()] private NodePath _materialThreeNodePath;
        [Export()] private NodePath _materialFourNodePath;

        private CraftingMaterialAmountInfo _materialOne;
        private CraftingMaterialAmountInfo _materialTwo;
        private CraftingMaterialAmountInfo _materialThree;
        private CraftingMaterialAmountInfo _materialFour;
        

        private Label _itemNameLabel;

        public override void _Ready()
        {
            base._Ready();
            _itemNameLabel = GetNode<Label>(_itemNameLabelNodePath);
            _materialOne = GetNode<HBoxContainer>(_materialOneNodePath) as CraftingMaterialAmountInfo;
            _materialTwo = GetNode<HBoxContainer>(_materialTwoNodePath) as CraftingMaterialAmountInfo;
            _materialThree = GetNode<HBoxContainer>(_materialThreeNodePath) as CraftingMaterialAmountInfo;
            _materialFour = GetNode<HBoxContainer>(_materialFourNodePath) as CraftingMaterialAmountInfo;
        }

        public void UpdateCraftingMenuInfo(ItemResource item)
        {
            if (item.itemName == null) return;

            _itemNameLabel.Text = item.itemName;
        }
    }
}