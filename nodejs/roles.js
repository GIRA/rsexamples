const Angle = require("./math/angle");
const Point = require("./math/point");

class BallFollower {
    applyOn(robot, snapshot) {
        if (snapshot.ball) {
            robot.moveToBall();
        } else {
            robot.moveToPoint(Point.ORIGIN);
        }
    }
}

class Goalkeeper {
    applyOn(robot, snapshot) {
        let ball = snapshot.ball ? snapshot.ball.position : Point.ORIGIN;
        let target = new Point(ball.x, -0.55);
        if (robot.position.dist(target) < 0.01) {
            robot.lookAtAngle(Angle.degrees(90));
        } else {
            robot.moveToPoint(target);
        }
    }
}

module.exports = {
    BallFollower: BallFollower,
    Goalkeeper: Goalkeeper
};