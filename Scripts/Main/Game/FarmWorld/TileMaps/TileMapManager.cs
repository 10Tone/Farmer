using Godot;

namespace Farmer.Scripts.Main.Game.FarmWorld.TileMaps
{
    public class TileMapManager: Node2D
    {
        [Export()] private NodePath _navigationTileMapNodePath;
        [Export()] private NodePath _farmSoilTileMapNodePath;
        [Export()] private NodePath _groundTileMapNodePath;

        public TileMap navigationTileMap;
        public FarmSoilTileMap farmSoilTileMap;
        public TileMap groundTileMap;

        public override void _Ready()
        {
            navigationTileMap = GetNode<TileMap>(_navigationTileMapNodePath);
            farmSoilTileMap = GetNode<TileMap>(_farmSoilTileMapNodePath) as FarmSoilTileMap;
            groundTileMap = GetNode<TileMap>(_groundTileMapNodePath);
        }

        public Vector2 MouseToGridPosition(Vector2 mousePos)
        {
            Vector2 tileMousePos = navigationTileMap.WorldToMap(mousePos);
            Vector2 gridPosition = navigationTileMap.MapToWorld(tileMousePos) + new Vector2(8,8);

            return gridPosition;
        }

        public Vector2 MouseToMapPosition(Vector2 mousePos)
        {
            return navigationTileMap.WorldToMap(mousePos);
        }

        public float MouseToPlayerPixelDistance(Vector2 mousePos, Vector2 playerPos)
        {
            Vector2 gridPosition = navigationTileMap.MapToWorld(navigationTileMap.WorldToMap(mousePos)) + new Vector2(8,8);
            
            Vector2 playerGridPosition = navigationTileMap.MapToWorld(navigationTileMap.WorldToMap(playerPos)) + new Vector2(8,8);
            
            var distance = playerGridPosition.DistanceTo(gridPosition);

            return distance;

        }
        
        // TEMP
        // public void TurnToFarmSoil(Vector2 tilePosition)
        // {
        //     FarmSoilTileMap.AddSoilTile(MouseToGridPosition(tilePosition));
        // }
        
    }
}