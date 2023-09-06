using Godot;

namespace Farmer.Scripts.Main.Game.GameUI.InventoryUI
{
    public class MouseMoveItem: Control
    {
        [Export()]private NodePath _iconNodePath, _labelNodePath;
        private TextureRect _icon;
        private Label _label;

        public override void _Ready()
        {
            Visible = false;
            _icon = GetNode<TextureRect>(_iconNodePath);
            _label = GetNode<Label>(_labelNodePath);
        }

        public void ShowItemMouseIcon(Texture icon, int amount)
        {
            RectGlobalPosition = GetGlobalMousePosition();
            Visible = true;
            _label.Visible = amount >= 2;
            _icon.Texture = icon;
            _label.Text = amount.ToString();
            
        }

        public void HideItemMouseIcon()
        {
            Visible = false;
        }
            
    }
}