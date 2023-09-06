using Godot;

namespace Farmer.Scripts.Main.Game.TimeManagement
{
    public class DayCycle: Node2D
    {
        // 1 sec is 2 minutes
        // 12 minutes is 24 hours
        
        // for testing purposes:
        // 1 sec is 10 minutes, 6 sec is 1 hour
        // 96 sec is 24 hours

        private float _hourSeconds = 0.1f;

        [Signal]
        private delegate void DayPassedEvent();

        public int minutes = 0;
        public int hours = 0;
        public int days;
        
        private Timer _timer;

        public override void _Ready()
        {
            _timer = new Timer();
            AddChild(_timer);
            HourCounter();
        }

        async void HourCounter()
        {
            _timer.Start(_hourSeconds);
            await ToSignal(_timer, "timeout");
            ++hours;
            // GD.Print("Hours passed: " + Hours);
            if (hours == 24)
            {
                hours = 0;
                ++days;
                // GD.Print("Days passed: " + Days);
                EmitSignal(nameof(DayPassedEvent));
            }
            
            HourCounter();
            
        }
        
    }
}