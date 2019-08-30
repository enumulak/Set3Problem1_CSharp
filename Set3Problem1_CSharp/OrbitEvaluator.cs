using System.Collections.Generic;
using System.Linq;

namespace Set3Problem1_CSharp
{
    public sealed class OrbitEvaluator
    {
        #region Private Variables

        private Orbit _orbit;
        
        //This list stores the Vehicles that can be used in the Current Weather - passed in by the Traffic Manager
        private List<Vehicle> _vehicles = new List<Vehicle>();

        private Weather _currentWeather;
        private List<OrbitRecommender> _orbitRecommenderList = new List<OrbitRecommender>();

        //Static method to provide access to Singleton Instance of this Class
        public static OrbitEvaluator Instance { get; } = new OrbitEvaluator();

        #endregion

        #region Public Methods

        public void EvaluateOrbit(Orbit orbit, Weather cWeather, List<Vehicle> vehicles)
        {
            if(orbit != null && cWeather != null && vehicles != null)
            {
                _orbit = orbit;
                _vehicles = vehicles;
                _currentWeather = cWeather;
            }

            //We first send the Current Weather's pothole factor to the current Orbit, so that it can re-calculate its pothole count
            _orbit.RequestPotholeAssessment(_currentWeather.GetPotholeFactor());

            //Then, Vehicles (that can be used in the current weather) are requested to set speed restrictions and calculate the time they take to traverse the current Orbit
            for(var i = 0; i < _vehicles.Count; i++)
            {
                _vehicles[i].RequestSpeedAndMinutesSettingForOrbit(_orbit);
            }

            //We can then initiate our recommendations to the Traffic Manager
            InitiateVehicleAndOrbitRecommendation();
        }

        #endregion

        #region Private Methods

        private void InitiateVehicleAndOrbitRecommendation()
        {
            //We need to get the Vehicle that has the Lowest value for 'MinutesToTraverseOrbit' (meaning that the Vehicle traverses the Orbit faster..)
            //We loop through the Vehicle List and get the Vehicle Object that has the 'lowest' Minutes
            Vehicle _fastestVehicleForOrbit = _vehicles.Aggregate((seed, f) => f.GetMinutesToTraverseOrbit() < seed.GetMinutesToTraverseOrbit() ? f : seed);

            if (_fastestVehicleForOrbit != null)
            {
                _orbitRecommenderList.Add(new OrbitRecommender(_fastestVehicleForOrbit.GetVehicleName(), _orbit.GetOrbitName(), _fastestVehicleForOrbit.GetMinutesToTraverseOrbit()));
            }

            //Once the Orbit Recommender List is ready for ALL given Orbits, we execute Logic to give a final recommendation to the Traffic Manager
            RecommendVehicleAndOrbit();
        }

        private void RecommendVehicleAndOrbit()
        {
            OrbitRecommender _finalRecommendation;

            if (_orbitRecommenderList.Count > 0)
            {
                _finalRecommendation = _orbitRecommenderList.Aggregate((seed, f) => f.GetVehicleMinutes() < seed.GetVehicleMinutes() ? f : seed);
                TrafficManager.Instance.recommendedVehicle = _finalRecommendation.GetFastestVehicleName();
                TrafficManager.Instance.recommendedOrbit = _finalRecommendation.GetOrbitName();
            }
        }

        #endregion
    }
}
