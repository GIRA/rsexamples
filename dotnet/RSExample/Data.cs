using System;
using System.Collections.Generic;
using System.Text;

namespace RSProxy
{
    public class SnapshotData
    {
        public float time;
        public bool waiting_for_kickoff;
        public RobotData robot;
        public object[] team;
    }
    
    public class RobotData
    {
        public string name;
        public string color;
        public int index;
        public float[] gps;
        public float[] compass;
        public SonarData sonar;
    }

    public class SonarData
    {
        public float left;
        public float right;
        public float front;
        public float back;
    }

    public class ResponseData
    {
        public object[] team;
        public float L;
        public float R;
    }
}
