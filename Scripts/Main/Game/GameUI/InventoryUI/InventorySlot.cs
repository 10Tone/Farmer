using System;
using Farmer.Scripts.Main.Game.Database;
using Godot;

namespace Farmer.Scripts.Main.Game.GameUI.InventoryUI
{
    public class InventorySlot: TextureRect
    {
        [Export()]protected NodePath _stackAmountLabelNodePath;
        [Export()] protected NodePath _iconTextureNodePath;
        private Label _stackAmountLabel;
        private TextureRect _iconTexture;
        
        public int slotIndex;

        [Signal]
        private delegate void MouseEnteredSlot();

        public override void _Ready()
        {
            _stackAmountLabel = GetNode<Label>(_stackAmountLabelNodePath);
            _iconTexture = GetNode<TextureRect>(_iconTextureNodePath);
            _stackAmountLabel.Visible = false;
            // GD.Print(RectPosition);
        }

        public void ChangeIcon(Texture icon)
        {
           _iconTexture.Texture = icon;
        }

        public void UpdateStackAmountLabel(int stackAmount)
        {
            _stackAmountLabel.Visible = stackAmount >= 2;

            _stackAmountLabel.Text = stackAmount.ToString();
        }

        void _on_InventorySlot_mouse_entered()
        {
            // GD.Print("mouse entered");
            EmitSignal(nameof(MouseEnteredSlot), slotIndex);
        }
        
    }
}