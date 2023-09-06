using Godot;

namespace Farmer.Scripts.Main.Game.FarmWorld.TileMaps
{
    public class FarmSoilTileMap: TileMap
    {
        public void AddSoilTile(Vector2 cellPos)
        {
            if(CheckForFarmSoil(cellPos)) return;
            // GD.Print("creating soil");
            SetCellv(cellPos,0);
        }

        public bool CheckForFarmSoil(Vector2 cellPos)
        {
            if (GetCellv(cellPos) >= 0)
                return true;
            return false;
        }
    }
}