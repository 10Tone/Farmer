using Farmer.Scripts.Main.Game.Interactibles;
using Godot;
using Godot.Collections;

namespace Farmer.Scripts.Main.Game
{
    public class MouseController: Node2D
    {
        [Export()] private Camera2D _camera2D;
        
        [Signal]
        private delegate void MouseMotionEvent();

        [Signal]
        delegate void LeftMouseButtonEvent();
        
        public override void _UnhandledInput(InputEvent @event)
        {
            if (@event is InputEventMouseMotion eventMouseMotion)
            {
                EmitSignal(nameof(MouseMotionEvent));
                
            }
            
            else if (@event is InputEventMouseButton eventMouseButton)
            {
                if (eventMouseButton.ButtonIndex == 1 && eventMouseButton.Pressed)
                {
                    EmitSignal(nameof(LeftMouseButtonEvent));
                }
            }
        }
    }
}