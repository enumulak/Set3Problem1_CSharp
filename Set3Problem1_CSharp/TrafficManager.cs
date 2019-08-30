using System;
using System.Collections.Generic;

namespace Set3Problem1_CSharp
{
    public enum WeatherType { sunny, rainy, windy }

    public sealed class TrafficManager
    {
        //Private Variables
        private List<Vehicle> Vehicles = new List<Vehicle>();
        private List<Orbit> Orbits = new List<Orbit>();
        private List<Weather> Weathers = new List<Weather>();

        private List<Vehicle> _vehiclesForCurrentWeather = new List<Vehicle>();

        private Weather CurrentWeather;
        private bool requirementsInitialized = false;

        public string recommendedOrbit = "";
        public string recommendedVehicle = "";

        //Static method to provide access to Singleton Instance of this Class
        public static TrafficManager Instance { get; } = new TrafficManager();

        #region Public Methods

        public void Initialize()
        {
            //Get the Inputs
            InitializeRequirements();

            if(requirementsInitialized)
            {
                for(var i = 0; i < Orbits.Count; i++)
                {
                    OrbitEvaluator.Instance.EvaluateOrbit(Orbits[i], CurrentWeather, _vehiclesForCurrentWeather);
                }
            }
            else
            {
                Console.WriteLine('\n');
                Console.WriteLine("There seems to be Insufficient Or Incorrect Data.. Cannot proceed..");
            }

            //Display Output
            Console.WriteLine('\n');
            
            if(recommendedOrbit != "" && recommendedVehicle != "")
            {
                Console.WriteLine("King Shan and Queen Anga should use a {0} and take the {1} Route", recommendedVehicle, recommendedOrbit);
            }
        }

        #endregion

        #region Private Methods

        private void InitializeRequirements()
        {
            //First we initialize the Data we require - Weathers, Vehicles and Orbits
            Weathers = new List<Weather>
            {
                new Weather(WeatherType.rainy, 0.2),
                new Weather(WeatherType.sunny, -0.1),
                new Weather(WeatherType.windy, 0.0)
            };

            Vehicles = new List<Vehicle>
            {
                new Vehicle("Bike", 10, 2, new List<WeatherType>{ WeatherType.sunny, WeatherType.windy }),
                new Vehicle("Tuk Tuk", 12, 1, new List<WeatherType>{ WeatherType.sunny, WeatherType.rainy }),
                new Vehicle("Car", 20, 3, new List<WeatherType>{ WeatherType.sunny, WeatherType.rainy, WeatherType.windy })
            };

            Orbits = new List<Orbit>
            {
                new Orbit("Orbit 1", 18, 20),
                new Orbit("Orbit 2", 20, 10)
            };

            //We now proceed to get the Current Weather through User Input
            GetCurrentWeather();
        }

        private void GetCurrentWeather()
        {
            //We ask User to specify the Current Weather of Lengaburu, then check the Input against all current Weather Types. Current Weather is stored when Input matches a weather type in the system
            Console.WriteLine("What is the Current Weather of Lengaburu? (Sunny, Rainy or Windy)");
            var input = Console.ReadLine();
            input.ToLower();

            for (var i = 0; i < Weathers.Count; i++)
            {
                if (input == Weathers[i].GetWeatherType().ToLower())
                {
                    CurrentWeather = Weathers[i];
                }
            }

            //Once Current Weather is set, we proceed to set the Traffic Speeds for all given Orbits
            SetTrafficSpeedForOrbits(); 
        }

        private void SetTrafficSpeedForOrbits()
        {
            Console.WriteLine('\n');

            //We ask user to give us the Traffic Speed for each Orbit
            for (var i = 0; i < Orbits.Count; i++)
            {
                Console.WriteLine("Please enter the Traffic Speed for {0}", Orbits[i].GetOrbitName());
                var tsInput = Console.ReadLine();

                if (int.TryParse(tsInput, out int result))
                {
                    Orbits[i].RequestTrafficSpeedSetting(result);
                }
                else
                {
                    Console.WriteLine("Invalid Input detected for Traffic Speed...");
                }
            }

            //After the Traffic Speeds are obtained, the Vehicles that can be used for the Current Weather are requested to set their Weather Usage Flags to True
            SetVehiclesForCurrentWeather();
        }

        private void SetVehiclesForCurrentWeather()
        {
            bool flag = false;

            for(var i = 0; i < Vehicles.Count; i++)
            {
                for(var j = 0; j < Vehicles[i].GetVehicleWeatherList().Count; j++)
                {
                    if(Vehicles[i].GetFromVehicleWeatherList(j).ToLower() == CurrentWeather.GetWeatherType().ToLower())
                    {
                        flag = true;
                        Vehicles[i].RequestUsageForCurrentWeather(flag);

                        if(Vehicles[i].CanBeUsedInCurrentWeather())
                        {
                            _vehiclesForCurrentWeather.Add(Vehicles[i]);
                        }
                    }
                }
            }

            //Once Vehicles for current weather have been obtained, we finalize our requirements
            FinalizeRequirements();
        }

        private void FinalizeRequirements()
        {
            //The 'RequirementsInitialized' variable is set based on the condition that all given Orbits have been set with valid traffic speeds (Going forward we can add more conditions if required)
            for (var j = 0; j < Orbits.Count; j++)
            {
                if (Orbits[j].GetOrbitTrafficSpeed() == 0)
                {
                    requirementsInitialized = false;
                    break;
                }
                else
                {
                    if (CurrentWeather != null && _vehiclesForCurrentWeather.Count > 0)
                    { requirementsInitialized = true; }
                }
            }
        }

        #endregion
    }
}
