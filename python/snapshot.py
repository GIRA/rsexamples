from dataclasses import dataclass
from math_utils.angle import radians
from math_utils.point import Point
import math

@dataclass
class RobotData:
    name: str
    index: int
    position: Point
    rotation: float

@dataclass
class BallData:
    position: Point


class Snapshot:
    data = None
    color = None
    robot = None
    ball = None

    def __init__(self, data):
        self.data = data
        self.color = data["robot"]["color"]
        self.processRobotSensors(data["robot"])
        self.processBallSignal(data.get("ball"))
        self.mergeTeamData(data.get("team"))

    def isBallDetected(self):
        return self.data.get("ball") != None

    def processRobotSensors(self, robot_data):
        x, y, _ = robot_data["gps"]
        cx, cy, _ = robot_data["compass"]
        self.robot = RobotData(
            name=robot_data["name"],
            index=robot_data["index"],
            position=Point(x, y),
            rotation=radians(math.atan2(cx, cy) + math.pi/2))

    def processBallSignal(self, ball_data):
        if ball_data == None: return None
        dist = math.sqrt(1/ball_data["strength"])
        x, y, _ = ball_data["direction"]
        da = radians(math.atan2(y, x))
        a = radians(self.robot.rotation + da)
        dx = math.sin(a) * dist
        dy = math.cos(a) * -1 * dist
        bx = self.robot.position.x + dx
        by = self.robot.position.y + dy
        self.ball = BallData(position=Point(bx, by))

    def mergeTeamData(self, team_data):
        if self.ball == None:
            if team_data and len(team_data) > 0:
                x, y = team_data[0]
                self.ball = BallData(position=Point(x, y))

