using RSExample.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSExample
{
    interface IRole
    {
        void ApplyOn(Robot robot, Snapshot snapshot);
    }


    class BallFollower : IRole
    {
        public void ApplyOn(Robot robot, Snapshot snapshot)
        {
            if (snapshot.Ball != null)
            {
                robot.MoveToBall();
            }
            else
            {
                robot.MoveToPoint(Point.ORIGIN);
            }
        }
    }

    class Goalkeeper : IRole
    {
        public void ApplyOn(Robot robot, Snapshot snapshot)
        {
            var ball = snapshot.Ball != null ? snapshot.Ball.Position : Point.ORIGIN;
            var target = new Point(ball.X, -0.55f);

            if (robot.Position.Dist(target) < 0.01)
            {
                robot.LookAtAngle(Angle.Degrees(90));
            }
            else
            {
                robot.MoveToPoint(target);
            }
        }
    }
}
