from base_robot import BaseRobot

class Robot1(BaseRobot):
    def run(self):
        if self.is_new_team_data():
            while self.is_new_team_data():
                msg = self.get_new_team_data()
                print(msg)
            print("---")

        self.leftMotor.setVelocity(-10)
        self.rightMotor.setVelocity(10)


class Robot2(BaseRobot):
    def run(self):
        if self.is_new_team_data():
            while self.is_new_team_data():
                msg = self.get_new_team_data()
                print(msg)
            print("---")

        self.leftMotor.setVelocity(10)
        self.rightMotor.setVelocity(-10)


class Robot3(BaseRobot):
    def run(self):
        if self.is_new_team_data():
            while self.is_new_team_data():
                msg = self.get_new_team_data()
                print(msg)
            print("---")

        print(self.get_gps_coordinates(), self.get_compass_heading())
        #self.send_data_to_team("Hola, soy " + self.getName())
        self.leftMotor.setVelocity(0)
        self.rightMotor.setVelocity(0)

