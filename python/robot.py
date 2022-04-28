import math_utils.angle as Angle
from math_utils.point import Point
from math_utils.utils import clamp
from snapshot import Snapshot

from collections import deque

MAX_SPEED = 10

class Robot:    
    leftVelocity = 0
    rightVelocity = 0
    teamMessages = deque()

    def __init__(self, role):
        self.role = role

    def getPosition(self):
        return self.snapshot.robot.position

    def getRotation(self):
        return self.snapshot.robot.rotation

    def loop(self, data):
        self.snapshot = Snapshot(data)
        self.run()
        teamMessages = list(self.teamMessages)
        self.teamMessages.clear()
        return {
            "team": teamMessages,
            "L": self.leftVelocity,
            "R": self.rightVelocity
        }

    def sendDataToTeam(self, data):
        self.teamMessages.append(data)

    def lookAtAngle(self, a):
        ra = self.getRotation()
        delta = min(Angle.diff(a, ra), Angle.diff(a, Angle.opposite(ra)))
        threshold = Angle.degrees(1)

        if delta < threshold:
            vl = 0
            vr = 0
        else:
            vel = clamp(delta / Angle.degrees(30), 0, 1)
            p = Point.fromAngle(Angle.radians(a - ra))
            if p.x < 0:
                vl = vel * -1
                vr = vel
            else:
                vl = vel
                vr = vel * -1
            if p.y > 0:
                vl = vl * -1
                vr = vr * -1
        
        self.leftVelocity = vl * MAX_SPEED
        self.rightVelocity = vr * MAX_SPEED

    def lookAtPoint(self, point):
        position = self.getPosition()
        rx = position.x
        ry = position.y
        px = point.x
        py = point.y
        self.lookAtAngle(Point(px - rx, py - ry).getAngle())

    def moveToPoint(self, point):
        position = self.getPosition()
        rx = position.x
        ry = position.y
        px = point.x
        py = point.y
        a = Point(px - rx, py - ry).getAngle()
        ra = self.getRotation()
        delta = min(Angle.diff(a, ra), Angle.diff(a, Angle.opposite(ra)))
        decrease = (Angle.r2d(delta) / 90) * 2
        p = Point.fromAngle(Angle.radians(a - ra))
        if p.x < 0:
            vl = 1 - decrease
            vr = 1
        else:
            vl = 1
            vr = 1 - decrease
        if p.y > 0:
            vl = vl * -1
            vr = vr * -1
        
        self.leftVelocity = vl * MAX_SPEED
        self.rightVelocity = vr * MAX_SPEED

    def moveToBall(self):
        self.moveToPoint(self.snapshot.ball.position)

    def run(self):
        if self.snapshot.isBallDetected():
            pos = self.snapshot.ball.position
            self.sendDataToTeam([pos.x, pos.y])

        self.role.applyOn(self, self.snapshot)