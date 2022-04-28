# -*- coding: utf-8 -*-
from rsproxy.server import Server
from robot import Robot
from roles import (BallFollower, Goalkeeper)

def setup():
    global robots
    robots = [Robot(Goalkeeper()),
            Robot(BallFollower()),
            Robot(BallFollower())]

def loop(snapshot):
    robot = robots[snapshot["robot"]["index"]]
    return robot.loop(snapshot)

server = Server(setup, loop)
server.start()
