const Server = require("./server");
const Robot = require("./robot");
const { BallFollower, Goalkeeper } = require("./roles");

let robots;

function setup() {
    robots = [
        new Robot(new Goalkeeper()),
        new Robot(new BallFollower()),
        new Robot(new BallFollower()),
    ];
}

function loop(snapshot) {
    let robot = robots[snapshot.robot.index];
    return robot.loop(snapshot);
}

const port = process.argv[2];
var server = new Server(setup, loop);
server.start(port);
