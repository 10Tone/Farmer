using Godot;

namespace Farmer.Scripts
{
    public class AreaTest: Area2D
    {
        void _on_Area2D_body_entered(Node body)
        {
            GD.Print("hi");
        }
        
    }
}