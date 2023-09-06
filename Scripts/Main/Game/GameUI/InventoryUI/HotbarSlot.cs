using Godot;

namespace Farmer.Scripts.Main.Game.GameUI.InventoryUI
{
    public class HotbarSlot: InventorySlot
    {
        [Export()] private Texture _normalTexture, _selectedTexture;

        [Signal]
        private delegate void HotbarSlotSelected();
        

        void _on_HotbarSlot_gui_input(InputEvent @event)
        {
            if (@event is InputEventMouseButton eventMouseButton)
            {
                if (eventMouseButton.Pressed && eventMouseButton.ButtonIndex == 1)
                {
                    // GD.Print(slotIndex + " clicked");
                    EmitSignal(nameof(HotbarSlotSelected), slotIndex);
                }
            }
        }

        public void ShowSelectedHotbar(int index)
        {
            if (index == slotIndex)
            {
                Texture = _selectedTexture;
            }
            else Texture = _normalTexture;
        }
    }
}