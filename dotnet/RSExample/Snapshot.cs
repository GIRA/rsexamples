using RSExample.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSExample
{
    class RobotData
    {
        public RobotData(string name, int index, Point position, float rotation)
        {
            Name = name;
            Index = index;
            Position = position;
            Rotation = rotation;
        }

        public string Name { get; }
        public int Index { get; }
        public Point Position { get; }
        public float Rotation { get; }
    }

    class BallData
    {
        public BallData(Point position)
        {
            Position = position;
        }

        public Point Position { get; }
    }

    class Snapshot
    {
        public Snapshot(RSProxy.SnapshotData data)
        {
            Data = data;
            Color = data.robot.color;
            ProcessRobotSensors(data.robot);
            ProcessBallSignal(data.ball);
            MergeTeamData(data.team);
        }

        public RSProxy.SnapshotData Data { get; private set; }
        public string Color { get; private set; }
        public RobotData Robot { get; private set; }
        public BallData Ball { get; private set; }

        public bool IsBallDetected { get { return Data.ball != null; } }


        private void ProcessRobotSensors(RSProxy.RobotData robot_data)
        {
            var x = robot_data.gps[0];
            var y = robot_data.gps[1];
            var cx = robot_data.compass[0];
            var cy = robot_data.compass[1];
            Robot = new RobotData(
                name: robot_data.name,
                index: robot_data.index,
                position: new Point(x, y),
                rotation: Angle.Radians(MathF.Atan2(cx, cy) + (MathF.PI / 2)));
        }

        private void ProcessBallSignal(RSProxy.BallData ball_data)
        {
            if (ball_data == null) return;
            var dist = MathF.Sqrt(1 / ball_data.strength);
            var x = ball_data.direction[0];
            var y = ball_data.direction[1];
            var da = Angle.Radians(MathF.Atan2(y, x));
            var a = Angle.Radians(Robot.Rotation + da);
            var dx = MathF.Sin(a) * dist;
            var dy = MathF.Cos(a) * -1 * dist;
            var bx = Robot.Position.X + dx;
            var by = Robot.Position.Y + dy;
            Ball = new BallData(position: new Point(bx, by));
        }

        private void MergeTeamData(object[] team)
        {
            // TODO(Richo)
        }

    }
}
