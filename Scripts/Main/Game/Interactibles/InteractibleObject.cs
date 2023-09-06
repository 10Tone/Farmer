using Farmer.Scripts.Main.Game.Autoload;
using Farmer.Scripts.Main.Game.Characters;
using Farmer.Scripts.Main.Game.Equipment;
using Godot;

namespace Farmer.Scripts.Main.Game.Interactibles
{
    public class InteractibleObject: Area2D
    {
        protected bool playerCanInteract;
        protected InteractibleObjects thisInteractibleObject;

        public InteractibleObjects ThisInteractibleObject => thisInteractibleObject;

        protected PlayerEquipment neededEquipmentForInteraction;

        // [Signal]
        // private delegate void InteractWithObject();

        public override void _Ready()
        {
            base._Ready();
            AddToGroup("Interactibles");
        }

        void _on_InteractibleObject_body_entered(Node body)
        {
            if (body.IsInGroup("Player"))
            {
                playerCanInteract = true;
                // GD.Print("Player entered");
            }
        }

        void _on_InteractibleObject_body_exited(Node body)
        {
            if (body.IsInGroup("Player"))
            {
                playerCanInteract = false;
                // GD.Print("Player exited");
            }
        }
        

        // void _on_InteractibleObject_input_event(Object viewport, InputEvent @event, int shape_idx)
        // {
        //     if (@event is InputEventMouseButton eventMouseButton)
        //     {
        //         if (eventMouseButton.Pressed && eventMouseButton.ButtonIndex == 1)
        //         {
        //             if(!_playerCanInteract) return;
        //             if (Global.Instance.currentEquippedItem == neededEquipmentForInteraction)
        //             {
        //                 // GD.Print("interact with object");
        //                 EmitSignal(nameof(InteractWithObject), thisInteractibleObject);
        //             }
        //         }
        //     }
        // }
    }
}