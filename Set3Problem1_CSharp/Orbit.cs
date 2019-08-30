using System;

namespace Set3Problem1_CSharp
{
    public class Orbit
    {
        #region Private Variables

        //Stores the name of a given Orbit
        private readonly string Name;

        //Stores the Distance of the Orbit
        private readonly int Distance;

        //Specifies the number of Potholes for an Orbit
        private int Potholes;

        //Specifies the maximum traffic speed of an Orbit
        private int TrafficSpeed;

        #endregion

        //Public Constructor with Arguments - the only one allowed to instantiate Orbits
        public Orbit(string name, int distance, int potholes)
        {
            Name = name;
            Distance = distance;
            Potholes = potholes;
            TrafficSpeed = 0;
        }

        #region Public Methods

        public void RequestTrafficSpeedSetting(int speed)
        {
            //Pre-Checks and other required Logic can be added here if needed..
            if (speed > 0)
                SetTrafficSpeed(speed);
        }

        public void RequestPotholeAssessment(double phFactor)
        {
            int result = Convert.ToInt32(Potholes * phFactor);

            SetNumberOfPotholes(result);
        }

        #endregion

        #region Private Methods

        private void SetTrafficSpeed(int speed)
        {
            TrafficSpeed = speed;
        }

        private void SetNumberOfPotholes(int potholes)
        {
            Potholes += potholes;
        }

        #endregion

        #region Data Readers

        public string GetOrbitName() => Name;
        public int GetOrbitDistance() => Distance;
        public int GetOrbitPotholes() => Potholes;
        public int GetOrbitTrafficSpeed() => TrafficSpeed;

        #endregion
    }
}
