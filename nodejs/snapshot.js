const angle = require("./math/angle");
const Point = require("./math/point");

class Snapshot {
    data;
    color;
    robot;
    ball;

    constructor(data) {
        this.data = data;
        this.color = data.robot.color;
        this.processRobotSensors(data.robot);
        this.processBallSignal(data.ball);
        this.mergeTeamData(data.team);
    }

    isBallDetected() {
        return this.data.ball != null;
    }

    processRobotSensors(robot_data) {
        let x, y, cx, cy;
        [x, y] = robot_data.gps;
        [cx, cy] = robot_data.compass;
        this.robot = {
            name: robot_data.name,
            index: robot_data.index,
            position: new Point(x, y),
            rotation: angle.radians(Math.atan2(cx, cy) + Math.PI/2)
        };
    }

    processBallSignal(ball_data) {
        if (!ball_data) return null;
        let dist = Math.sqrt(1/ball_data.strength);
        let x, y;
        [x, y] = ball_data.direction;
        let da = angle.radians(Math.atan2(y, x));
        let a = angle.radians(this.robot.rotation + da);
        let dx = Math.sin(a) * dist;
        let dy = Math.cos(a) * -1 * dist;
        let bx = this.robot.position.x + dx;
        let by = this.robot.position.y + dy;
        this.ball = {
            position: new Point(bx, by)
        }
    }

    mergeTeamData(team_data) {
        if (!this.ball) {
            if (team_data && team_data.length > 0) {
                this.ball = team_data[0];
            }
        }
    }
}

module.exports = Snapshot;