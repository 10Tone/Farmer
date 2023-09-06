using Farmer.Scripts.Main.Game;
using Godot;

namespace Farmer.Scripts.Main
{
	public class MainManager: Node2D
	{
		[Export()]private NodePath _gameManagerNodePath; // replace for packedscene and instantiate
		private GameManager _gameManager;

		public override void _Ready()
		{
			_gameManager = GetNode<Node2D>(_gameManagerNodePath) as GameManager;
		}
	}
}
