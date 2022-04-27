const angle = require("./angle");
const { clamp } = require("./utils");

class Point {
    x; y;
    static ORIGIN = new Point(0, 0);

    static fromAngle(angle, magnitude = 1) {
        let x = magnitude * -1 * Math.sin(angle);
        let y = magnitude * Math.cos(angle);
        return new Point(x, y);
    }

    static average(points) {
        let x = 0;
        let y = 0;
        let c = points.length;
        for (let i = 0; i < c; i++) {
            x += points[i].x;
            y += points[i].y;
        }
        return new Point(x/c, y/c);
    }

    constructor(x, y) {
        this.x = x;
        this.y = y;
    }

    dist(point) {
        let dx = point.x - this.x;
        let dy = point.y - this.y;
        return Math.sqrt(dx*dx + dy*dy);
    }

    get magnitude() { return this.dist(Point.ORIGIN); }

    get angle() {
        if (this.x == 0 && this.y == 0) return angle.radians(0);
        return angle.radians(Math.atan2(this.x * -1, this.y));
    }
}

module.exports = Point;