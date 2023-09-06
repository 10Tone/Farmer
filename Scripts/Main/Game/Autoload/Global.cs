using Farmer.Scripts.Main.Game.Characters;
using Farmer.Scripts.Main.Game.Collectibles;
using Farmer.Scripts.Main.Game.Equipment;
using Godot;

namespace Farmer.Scripts.Main.Game.Autoload
{
    public class Global: Node
    {
        private static Global _intstance;

        public static Global Instance
        {
            get { return _intstance; }
        }

        Global()
        {
            _intstance = this;
        }

        public PlayerEquipment currentEquippedItem;
        public CollectiblesManager collectiblesManager;
        public float playerEnergy;
        
    }
}