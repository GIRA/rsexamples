# -*- coding: utf-8 -*-
import math
from collections import deque

class Motor():
    def __init__(self) -> None:
        self.__velocity = 0
    def getVelocity(self):
        return self.__velocity
    def setVelocity(self, vel):
        self.__velocity = vel

class GPS():
    def __init__(self):
        self.__values = None
    def _update(self, values):
        self.__values = values
    def getValues(self):
        return self.__values

class Compass():
    def __init__(self):
        self.__values = None
    def _update(self, values):
        self.__values = values
    def getValues(self):
        return self.__values

class DistanceSensor():
    def __init__(self):
        self.__value = None
    def _update(self, value):
        self.__value = value
    def getValue(self):
        return self.__value

class BaseRobot:
    def __init__(self) -> None:
        self.leftMotor = Motor()
        self.rightMotor = Motor()
        self.gps = GPS()
        self.compass = Compass()
        self.sonar_left = DistanceSensor()
        self.sonar_right = DistanceSensor()
        self.sonar_front = DistanceSensor()
        self.sonar_back = DistanceSensor()
        self.__out_queue = deque()
        self.__in_queue = deque()

    def loop(self, snapshot):
        self.__snapshot = snapshot
        self.__in_queue.extend(snapshot.get("team") or [])
        self.__update_sensors()
        self.run()
        team_messages = list(self.__out_queue)
        self.__out_queue.clear()
        return {
            "team": team_messages,
            "L": self.leftMotor.getVelocity(), 
            "R": self.rightMotor.getVelocity()
        }

    def __update_sensors(self):
        robot_data = self.__snapshot["robot"]
        self.gps._update(robot_data["gps"])
        self.compass._update(robot_data["compass"])
        self.sonar_left._update(robot_data["sonar"]["left"])
        self.sonar_right._update(robot_data["sonar"]["right"])
        self.sonar_front._update(robot_data["sonar"]["front"])
        self.sonar_back._update(robot_data["sonar"]["back"])

    def getName(self):
        return self.__snapshot["robot"]["name"]

    def get_ball_data(self):
        return self.__snapshot.get("ball")

    def get_gps_coordinates(self):
        gps_values = self.gps.getValues()
        return [gps_values[0], gps_values[1]]

    def get_compass_heading(self):
        compass_values = self.compass.getValues()

        # Add math.pi/2 (90) so that the heading 0 is facing opponent's goal
        rad = math.atan2(compass_values[0], compass_values[1]) + (math.pi / 2)
        if rad < -math.pi:
            rad = rad + (2 * math.pi)

        return rad

    def get_sonar_values(self):
        return {
            "left": self.sonar_left.getValue(),
            "right": self.sonar_right.getValue(),
            "front": self.sonar_front.getValue(),
            "back": self.sonar_back.getValue(),
        }

    def send_data_to_team(self, data):
        self.__out_queue.append(data)

    def is_new_team_data(self):
        return len(self.__in_queue) > 0

    def get_new_team_data(self):
        return self.__in_queue.popleft()

    def run(self):
        raise NotImplementedError
        