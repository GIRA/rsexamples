import math

RADIANS_PER_DEGREE = math.pi/180

def d2r(deg):
    return deg*RADIANS_PER_DEGREE

def r2d(rad):
    return rad/RADIANS_PER_DEGREE

def normalize(rad): 
    return rad % (math.pi*2)

def radians(rad):
    return normalize(rad)

def degrees(deg):
    return normalize(d2r(deg))

def opposite(rad):
    return normalize(rad + math.pi)

def diffClockwise(a, b):
    return normalize(a - b)

def diffCounterclockwise(a, b):
    return normalize(b - a)

def diff(a, b):
    return min(diffClockwise(a, b), 
            diffCounterclockwise(a, b))
            