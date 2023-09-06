using Farmer.Scripts.Main.Game.Database;
using Godot;

namespace Farmer.Scripts.Main.Game.GameUI.InventoryUI
{
    public class InventoryUi: Control
    {
        [Export()] private NodePath _slotParentNodePath;
        // [Export()] private PackedScene _slotPackedScene;
        private GridContainer _slotParent;

        public InventorySlot[] uiSlots;

        [Signal]
        private delegate void MouseEnteredSlot();

        public override void _Ready()
        {
            _slotParent = GetNode<GridContainer>(_slotParentNodePath);
            uiSlots = new InventorySlot[_slotParent.GetChildren().Count];

            for (int i = 0; i < uiSlots.Length; i++)
            {
                var uiSlot = _slotParent.GetChildren()[i] as InventorySlot;
                uiSlot.Connect("MouseEnteredSlot", this, nameof(OnMouseEnteredSlot));
                uiSlot.slotIndex = i;
                uiSlots[i] = uiSlot;
            }
        }
        
        public void AddItemToUiSlot(int index, ItemResource item, int stackAmount)
        {
            // if (item.iconTexture == null) return;
            // GD.Print("add item");
            uiSlots[index].ChangeIcon(item.iconTexture);
            uiSlots[index].UpdateStackAmountLabel(stackAmount);
        }

        void OnMouseEnteredSlot(int index)
        {
            EmitSignal(nameof(MouseEnteredSlot), index);
        }

    }
}