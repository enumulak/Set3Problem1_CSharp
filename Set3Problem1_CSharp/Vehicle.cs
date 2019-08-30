using System;
using System.Collections.Generic;

namespace Set3Problem1_CSharp
{
    public class Vehicle
    {
        #region Private Variables

        //Stores Name of the Vehicle
        private readonly string Name;

        //Stores the Maximum Speed that a Vehicle can travel
        private readonly int maxSpeed;

        //Specifies the time it takes for a Vehicle to cross One Pothole
        private readonly int potholeTime;

        //Stores the Type of Weather(s) that a Vehicle can operate under
        private readonly List<string> weatherList = new List<string>();

        //Stores the actual speed with which a Vehicle can traverse a given Orbit (is based on the Orbit Speed)
        private int restrictedSpeed;

        private int minutesToTraverseOrbit;

        private bool canBeUsedInCurrentWeather;

        #endregion

        //Public Constructor with Arguments - the only one allowed to instantiate Vehicle Objects
        public Vehicle(string name, int mSpeed, int pTime, List<WeatherType> fWeather)
        {
            Name = name;
            maxSpeed = mSpeed;
            potholeTime = pTime;
            restrictedSpeed = 0;
            minutesToTraverseOrbit = 0;
            canBeUsedInCurrentWeather = false;

            if (fWeather != null && fWeather.Count > 0)
            {
                for(var i = 0; i < fWeather.Count; i++)
                {
                    weatherList.Add(fWeather[i].ToString());
                }
            }
            else
            {
                Console.WriteLine("There was a problem.. Insufficent Data!");
            }
        }

        #region Public Methods

        public void RequestUsageForCurrentWeather(bool flag)
        {
            if(flag)
            {
                SetUsageForCurrentWeather(flag);
            }
        }

        public void RequestSpeedAndMinutesSettingForOrbit(Orbit o)
        {
            if(o != null && canBeUsedInCurrentWeather)
            {
                int orbitSpeed = o.GetOrbitTrafficSpeed();

                if(orbitSpeed < maxSpeed)
                {
                    SetSpeedRestriction(orbitSpeed);
                }
                else
                {
                    SetSpeedRestriction(maxSpeed);
                }
            }

            if(restrictedSpeed > 0)
            {
                CalculateMinutesToTraverseOrbit(o);
            }
        }

        #endregion

        #region Private Methods

        private void SetSpeedRestriction(int rSpeed)
        {
            restrictedSpeed = rSpeed;
        }

        private void SetMinutesToTraverseOrbit(int min)
        {
            minutesToTraverseOrbit = min;
        }

        private void SetUsageForCurrentWeather(bool flag)
        {
            canBeUsedInCurrentWeather = flag;
        }

        private void CalculateMinutesToTraverseOrbit(Orbit o)
        {
            int result = 0;

            result = Convert.ToInt32((o.GetOrbitDistance() / restrictedSpeed * 60) + o.GetOrbitPotholes() + potholeTime);

            SetMinutesToTraverseOrbit(result);
        }

        #endregion

        #region Data Readers

        public string GetVehicleName() => Name;
        public int GetVehicleMaxSpeed() => maxSpeed;
        public int GetVehiclePotholeTime() => potholeTime;
        public List<string> GetVehicleWeatherList() => weatherList;
        public string GetFromVehicleWeatherList(int index) => weatherList[index];
        public int GetVehicleRestrictedSpeed() => restrictedSpeed;
        public int GetMinutesToTraverseOrbit() => minutesToTraverseOrbit;
        public bool CanBeUsedInCurrentWeather() => canBeUsedInCurrentWeather;
        
        #endregion
    }
}
