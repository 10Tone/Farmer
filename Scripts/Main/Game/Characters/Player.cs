using Farmer.Scripts.Main.Game.Autoload;
using Godot;

namespace Farmer.Scripts.Main.Game.Characters
{
    public class Player: KinematicBody2D
    {
        [Export()] private NodePath _animatedSpriteNodePath;
        private AnimatedSprite _animatedSprite;
        
        private float _maxSpeed = 70f;
        Vector2 _velocity = Vector2.Zero;
        string _direction = "down";

        private float _playerEnergy = 100;

        public override void _Ready()
        {
            _animatedSprite = GetNode<AnimatedSprite>(_animatedSpriteNodePath);
            Global.Instance.playerEnergy = _playerEnergy;
        }

        public override void _PhysicsProcess(float delta)
        {
            Movement(delta);
        }

        public override void _Process(float delta)
        {
            Animation();
        }

        public void UpdateEnergyAmount(float drainAmount, float boostAmount)
        {
            _playerEnergy -= drainAmount;
            _playerEnergy += boostAmount;
            // GD.Print(_playerEnergy);
            Global.Instance.playerEnergy = _playerEnergy;
        }
        

        private void Movement(float delta)
        {
            Vector2 inputVector = Vector2.Zero;
            inputVector.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
            inputVector.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
            inputVector = inputVector.Normalized();

            if (inputVector != Vector2.Zero)
            {
                _velocity = inputVector;
            }
            else _velocity = Vector2.Zero;

            MoveAndCollide(_velocity * delta * _maxSpeed);
            // GD.Print(_velocity);
        }

        private void Animation()
        {
            string mode = "idle_";
            
            if (_velocity == Vector2.Up)
            {
                _direction = "up";
            }
            else if (_velocity == Vector2.Down)
            {
                _direction = "down";
            }
            else if (_velocity == Vector2.Left)
            {
                _direction = "left";
            }
            else if (_velocity == Vector2.Right)
            {
                _direction = "right";
            }

            if (_velocity != Vector2.Zero)
            {
                mode = "walk_";
            }
            else mode = "idle_";
            
            _animatedSprite.Play(mode + _direction);
        }
    }
}