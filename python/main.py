# -*- coding: utf-8 -*-
from rsproxy.server import Server
from robots import Robot1, Robot2, Robot3

def setup():
    global robots
    robots = [Robot1(), Robot2(), Robot3()]

def loop(snapshot):
    robot = robots[snapshot["robot"]["index"]]
    return robot.loop(snapshot)

server = Server(setup, loop)
server.start()
