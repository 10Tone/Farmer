using Godot;

namespace Farmer.Scripts.Main.Game
{
    public class MouseSelector: Node2D
    {
        // TileMap _tileMap = new TileMap();
        //
        // public void UpdateSelectorPosition()
        // {
        //     Vector2 tileMousePos = _tileMap.WorldToMap(GetGlobalMousePosition());
        //     Vector2 gridPosition = _tileMap.MapToWorld(tileMousePos);
        //
        //     Position = gridPosition;
        // }

        public override void _Process(float delta)
        {
            base._Process(delta);
        }
        // [Export()] private NodePath _navigationNodePath;
        // [Export()] private NodePath _farmSoilNodePath;
        // [Export()] private NodePath _playerNodePath;
        // [Export()] private NodePath _plantsNodePath;
        // [Export()] private PackedScene _plantPackedScene;
        // private TileMap _navigationTileMap;
        // private FarmSoilTileMap _farmSoilTileMap;
        // private KinematicBody2D _player;
        // private Sprite _selector;
        // private Node2D _plants;
        //
        // public override void _Ready()
        // {
        //     _navigationTileMap = GetNode<TileMap>(_navigationNodePath);
        //     _farmSoilTileMap = GetNode<TileMap>(_farmSoilNodePath) as FarmSoilTileMap;
        //     _player = GetNode<KinematicBody2D>(_playerNodePath);
        //     _selector = GetNode<Sprite>("Sprite");
        //     _plants = GetNode<Node2D>(_plantsNodePath);
        // }
        //
        //
        // public override void _UnhandledInput(InputEvent @event)
        // {
        //     if (@event is InputEventMouseMotion eventMouseMotion)
        //     {
        //         Vector2 tileMousePos = _navigationTileMap.WorldToMap(eventMouseMotion.Position);
        //         Vector2 gridPosition = _navigationTileMap.MapToWorld(tileMousePos) + new Vector2(8,8);
        //         Vector2 playerGridPosition = _navigationTileMap.MapToWorld(_navigationTileMap.WorldToMap(_player.Position)) + new Vector2(8,8);
        //
        //         var distance = playerGridPosition.DistanceTo(gridPosition);
        //         
        //         // GD.Print(gridPosition);
        //         // GD.Print(playerGridPosition);
        //         // GD.Print(distance);
        //
        //         Position = gridPosition;
        //
        //         if (distance <= 16)
        //         {
        //             _selector.Visible = true;
        //         }
        //         else _selector.Visible = false;
        //     }
        //     
        //     else if (@event is InputEventMouseButton eventMouseButton)
        //     {
        //         if (eventMouseButton.ButtonIndex == 1 && eventMouseButton.Pressed)
        //         {
        //             Vector2 tileMousePos = _navigationTileMap.WorldToMap(eventMouseButton.Position);
        //             Vector2 gridPosition = _navigationTileMap.MapToWorld(tileMousePos) + new Vector2(8,8);
        //             Vector2 playerGridPosition = _navigationTileMap.MapToWorld(_navigationTileMap.WorldToMap(_player.Position)) + new Vector2(8,8);
        //
        //             var distance = playerGridPosition.DistanceTo(gridPosition);
        //
        //             Position = gridPosition;
        //
        //             if (distance <= 18)
        //             {
        //                 if (!_farmSoilTileMap.CheckForFarmSoil(tileMousePos))
        //                 {
        //                     _farmSoilTileMap.AddSoilTile(tileMousePos);
        //                 }
        //                 else
        //                 {
        //                     // instantiate plant
        //                     Node2D plant = _plantPackedScene.Instance() as Node2D;
        //                     _plants.AddChild(plant);
        //                     plant.Position = gridPosition;
        //                 }
        //                 
        //             }
        //
        //         }
        //     }
        // }
        
    }
}