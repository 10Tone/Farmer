using Farmer.Scripts.Main.Game.Characters;
using Godot;

namespace Farmer.Scripts.Main.Game.Interactibles
{
    public class InteractiblesManager: Node2D
    {
        // [Signal]
        // private delegate void InteractWithObject();
        //
        // public override void _Ready()
        // {
        //     base._Ready();
        //     foreach (var interactibleObj in GetChildren())
        //     {
        //         InteractibleObject obj = interactibleObj as InteractibleObject;
        //         obj.Connect("InteractWithObject", this, nameof(OnInteractWithObject));
        //     }
        // }
        //
        // void OnInteractWithObject(InteractibleObjects currentObj)
        // {
        //     // GD.Print("interacting! with " + currentObj);
        //     EmitSignal(nameof(InteractWithObject), currentObj);
        // }

        // public void FindAndInteractWithObject(PlayerEquipment currentEquipment)
        // {
        //     foreach (var interactibleObj in GetChildren())
        //     {
        //         InteractibleObject obj = interactibleObj as InteractibleObject;
        //         // obj.Interact(currentEquipment);
        //     }
        // }
    }
}