using RSExample.Math;
using RSProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSExample
{
    class Robot
    {
        const float MAX_SPEED = 10;

        IRole role;
        Snapshot snapshot;
        float leftVelocity = 0;
        float rightVelocity = 0;
        List<object> teamMessages = new List<object>();

        public Robot(IRole role)
        {
            this.role = role;
        }

        public Point Position
        {
            get { return snapshot.Robot.Position; }
        }

        public float Rotation
        {
            get { return snapshot.Robot.Rotation; }
        }

        public ResponseData Loop(SnapshotData data)
        {
            snapshot = new Snapshot(data);
            Run();
            var teamMessages = this.teamMessages.ToArray();
            this.teamMessages.Clear();
            return new ResponseData()
            {
                team = teamMessages,
                L = leftVelocity,
                R = rightVelocity,
            };
        }

        public void SendDataToTeam(object data)
        {
            teamMessages.Add(data);
        }

        public void LookAtAngle(float a)
        {
            float vl, vr;
            var ra = Rotation;
            var delta = MathF.Min(Angle.Diff(a, ra), Angle.Diff(a, Angle.Opposite(ra)));
            var threshold = Angle.Degrees(1);

            if (delta < threshold)
            {
                vl = vr = 0;
            }
            else
            {
                var vel = Utils.Clamp(delta / Angle.Degrees(30), 0, 1);
                var p = Point.FromAngle(Angle.Radians(a - ra));
                if (p.X < 0)
                {
                    vl = vel * -1;
                    vr = vel;
                }
                else
                {
                    vl = vel;
                    vr = vel * -1;
                }
                if (p.Y > 0)
                {
                    vl *= -1;
                    vr *= -1;
                }
            }

            leftVelocity = vl * MAX_SPEED;
            rightVelocity = vr * MAX_SPEED;
        }

        public void LookAtPoint(Point point)
        {
            var rx = Position.X;
            var ry = Position.Y;
            var px = point.X;
            var py = point.Y;
            LookAtAngle(new Point(px - rx, py - ry).Angle);
        }

        public void MoveToPoint(Point point)
        {
            float vl, vr;
            var rx = Position.X;
            var ry = Position.Y;
            var px = point.X;
            var py = point.Y;
            var a = new Point(px - rx, py - ry).Angle;
            var ra = Rotation;
            var delta = MathF.Min(Angle.Diff(a, ra), Angle.Diff(a, Angle.Opposite(ra)));
            var decrease = (Angle.R2D(delta) / 90) * 2;
            var p = Point.FromAngle(Angle.Radians(a - ra));
            if (p.X < 0)
            {
                vl = 1 - decrease;
                vr = 1;
            }
            else
            {
                vl = 1;
                vr = 1 - decrease;
            }
            if (p.Y > 0)
            {
                vl *= -1;
                vr *= -1;
            }

            leftVelocity = vl * MAX_SPEED;
            rightVelocity = vr * MAX_SPEED;
        }

        public void MoveToBall()
        {
            MoveToPoint(snapshot.Ball.Position);
        }

        private void Run()
        {
            if (snapshot.IsBallDetected)
            {
                SendDataToTeam(snapshot.Ball.Position);
            }

            role.ApplyOn(this, snapshot);
        }
    }
}
