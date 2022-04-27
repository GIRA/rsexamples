const Server = require("./server");

const port = process.argv[2];

function setup() {
    console.log("SETUP");
}

function loop(snapshot) {
    console.log(snapshot);
    let vl = -10;
    let vr = 10;
    if (snapshot.robot.index == 0) {
      vl *= -1;
      vr *= -1;
    }
    return {
      team: ["Hola, soy " + snapshot.robot.name],
      L: vl,
      R: vr,
    };
  }

var server = new Server(setup, loop);
server.start(port);
