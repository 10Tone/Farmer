using Farmer.Scripts.Main.Game.Database;
using Godot;

namespace Farmer.Scripts.Main.Game.GameUI.InventoryUI
{
    public class HotbarUi: Control
    {
        [Export()] private NodePath _slotParentNodePath;

        private HBoxContainer _slotParent;
        
        public HotbarSlot[] uiHotbarSlots;
        
        [Signal]
        private delegate void HotbarSlotSelected();

        public override void _Ready()
        {
            _slotParent = GetNode<HBoxContainer>(_slotParentNodePath);
        }
       

        void _on_PlayerInventory_ready()
        {
            uiHotbarSlots = new HotbarSlot[_slotParent.GetChildCount()];

            for (int i = 0; i < uiHotbarSlots.Length; i++)
            {
                uiHotbarSlots[i] = _slotParent.GetChild(i) as HotbarSlot;
                // uiHotbarSlots[i].Connect("MouseEnteredSlot", this, nameof(OnMouseEnteredSlot));
                uiHotbarSlots[i].Connect("HotbarSlotSelected", this, nameof(OnHotbarSlotSelected));
                uiHotbarSlots[i].slotIndex = i;
            }
            uiHotbarSlots[0].ShowSelectedHotbar(0);
        }
        
        public void AddItemToUiSlot(int index, ItemResource item, int stackAmount)
        {
            // if (item.iconTexture == null) return;
            uiHotbarSlots[index].ChangeIcon(item.iconTexture);
            uiHotbarSlots[index].UpdateStackAmountLabel(stackAmount);
        }

        void OnHotbarSlotSelected(int slotIndex)
        {
            for (int i = 0; i < uiHotbarSlots.Length; i++)
            {
                uiHotbarSlots[i].ShowSelectedHotbar(slotIndex);
            }
            EmitSignal(nameof(HotbarSlotSelected), slotIndex);
        }
    }
}