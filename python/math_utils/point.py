import math
from math_utils.angle import radians
from math_utils.utils import clamp

class Point():
    @classmethod
    def fromAngle(cls, angle, magnitude=1):
        x = magnitude * -1 * math.sin(angle)
        y = magnitude * math.cos(angle)
        return cls(x, y)

    @classmethod
    def average(cls, points):
        x = 0
        y = 0
        for p in points:
            x = x + p.x
            y = y + p.y
        c = len(points)
        return cls(x/c, y/c)

    def __init__(self, x, y):
        self.x = x
        self.y = y

    def dist(self, point):
        dx = point.x - self.x
        dy = point.y - self.y
        return math.sqrt(dx*dx + dy*dy)

    def getMagnitude(self):
        return self.dist(Point.ORIGIN)

    def getAngle(self):
        if self.x == 0 and self.y == 0:
            return radians(0)
        return radians(math.atan2(self.x * -1, self.y))

    def keepInsideRectangle(self, rect):
        x = clamp(self.x, rect.origin.x, rect.corner.x)
        y = clamp(self.y, rect.origin.y, rect.corner.y)
        return Point(x, y)

Point.ORIGIN = Point(0, 0)
