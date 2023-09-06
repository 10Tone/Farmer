using Farmer.Scripts.Main.Game.Autoload;
using Godot;

namespace Farmer.Scripts.Main.Game.GameUI.GameStatsUi
{
    public class EnergyProgressBar: ProgressBar
    {
        public override void _Process(float delta)
        {
            // Set("Range/value", Global.Instance.playerEnergy);
            Value = Global.Instance.playerEnergy;
        }
    }
}