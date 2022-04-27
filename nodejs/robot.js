const angle = require("./math/angle");
const Point = require("./math/point");
const Snapshot = require("./snapshot");
const { clamp } = require("./math/utils");

const MAX_SPEED = 10;

class Robot {
    role;
    snapshot;
    leftVelocity = 0;
    rightVelocity = 0;
    teamMessages = [];

    constructor(role) {
        this.role = role;
    }

    get rotation() { return this.snapshot.robot.rotation; }
    get position() { return this.snapshot.robot.position; }

    loop(data) {
        this.snapshot = new Snapshot(data);
        this.run();
        let teamMessages = this.teamMessages.slice();
        this.teamMessages.length = 0;
        return {
            team: teamMessages,
            L: this.leftVelocity,
            R: this.rightVelocity,
        };
    }

    sendDataToTeam(data) {
        this.teamMessages.push(data);
    }

    lookAtAngle(a) {
        let vl, vr;
        let ra = this.rotation;
        let delta = Math.min(angle.dist(a, ra), angle.dist(a, angle.opposite(ra)));
        let threshold = angle.degrees(1);

        if (delta < threshold) {
            vl = 0;
            vr = 0;
        } else {
            let vel = clamp(delta / angle.degrees(30), 0, 1);
            let p = Point.fromAngle(angle.radians(a - ra));
            if (p.x < 0) {
                vl = vel * -1;
                vr = vel;
            } else {
                vl = vel;
                vr = vel * -1;
            }
            if (p.y > 0) {
                vl *= -1;
                vr *= -1;
            }
        }
        
        this.leftVelocity = vl * MAX_SPEED;
        this.rightVelocity = vr * MAX_SPEED;
    }

    lookAtPoint(point) {
        let rx = this.position.x;
        let ry = this.position.y;
        let px = point.x;
        let py = point.y;
        this.lookAtAngle(new Point(px - rx, py - ry).angle);
    }

    moveToPoint(point) {
        let vl, vr;
        let rx = this.position.x;
        let ry = this.position.y;
        let px = point.x;
        let py = point.y;
        let a = new Point(px - rx, py - ry).angle;
        let ra = this.rotation;
        let delta = Math.min(angle.dist(a, ra), angle.dist(a, angle.opposite(ra)));
        let decrease = (angle.r2d(delta) / 90) * 2;
        let p = Point.fromAngle(angle.radians(a - ra));
        if (p.x < 0) {
            vl = 1 - decrease;
            vr = 1;
        } else {
            vl = 1;
            vr = 1 - decrease;
        }
        if (p.y > 0) {
            vl *= -1;
            vr *= -1;
        }
        
        this.leftVelocity = vl * MAX_SPEED;
        this.rightVelocity = vr * MAX_SPEED;
    }

    moveToBall() {
        this.moveToPoint(this.snapshot.ball.position);
    }

    run() {
        if (this.snapshot.isBallDetected()) {
            this.sendDataToTeam(this.snapshot.ball);
        }
        this.role.applyOn(this, this.snapshot);
    }
}

module.exports = Robot;