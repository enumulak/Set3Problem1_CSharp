using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Set3Problem1_CSharp;

namespace LengaburuTrafficTest
{
    [TestClass]
    public class LengaburuTrafficTests_Orbit
    {
        //Data that is required by this Class

        Orbit orbit1 = new Orbit("Orbit1", 18, 20);
        Orbit orbit2 = new Orbit("Orbit2", 20, 10);

        Weather weather1 = new Weather(WeatherType.rainy, 0.2);
        Weather weather2 = new Weather(WeatherType.sunny, -0.1);
        Weather weather3 = new Weather(WeatherType.windy, 0.0);

        //This method checks whether an Orbit is able to set its Traffic Speed
        [TestMethod]
        public void TestOrbitTrafficSpeedSet()
        {
            //ARRANGE
            int _trafficSpeed = 12; //This is our Expected value - this test is expected to return the same value when we specify it as the traffic speed for an Orbit

            //ACT
            orbit1.RequestTrafficSpeedSetting(12);

            //ASSERT
            int trafficSpeed = orbit1.GetOrbitTrafficSpeed();
            Assert.AreEqual(_trafficSpeed, trafficSpeed);
        }

        //This Method tests whether an Orbit is internally able to re-calculate its pothole number when we give it the pothole factor of a specific Weather
        //We are testing for the RAINY Weather in this Method. The Orbit Pothole Count is expected to rise by 20%
        [TestMethod]
        public void TestOrbitPotholeRecalculationForRainy()
        {
            //ARRANGE
            int _potHoleCount = 24; //This is our expected value. We are using Orbit1 for this test that has a pothole count of 20. When the weather is Rainy, the pothole count should increase to 24

            //ACT
            orbit1.RequestPotholeAssessment(weather1.GetPotholeFactor());   //weather1 is RAINY. We pass in the pothole factor of this weather to orbit1 and the orbit is supposed to internally re-calculate its pothole count

            //ASSERT
            int result = orbit1.GetOrbitPotholes();
            Assert.AreEqual(_potHoleCount, result);
        }

        //This Method tests whether an Orbit is internally able to re-calculate its pothole number when we give it the pothole factor of a specific Weather
        //We are testing for the SUNNY Weather in this Method. The Orbit Pothole Count is expected to decrease by 10%
        [TestMethod]
        public void TestOrbitPotholeRecalculationForSunny()
        {
            //ARRANGE
            int _potholeCount = 18; //This is our expected value. We are using Orbit1 for this test that has a pothole count of 20. When the weather is Sunny, the pothole count should decrease to 18

            //ACT
            orbit1.RequestPotholeAssessment(weather2.GetPotholeFactor());   //weather2 is SUNNY. We pass in the pothole factor of this weather to orbit1 and the orbit is supposed to internally re-calculate its pothole count

            //ASSERT
            int result = orbit1.GetOrbitPotholes();
            Assert.AreEqual(_potholeCount, result);
        }

        //This Method tests whether an Orbit is internally able to re-calculate its pothole number when we give it the pothole factor of a specific Weather
        //We are testing for the WINDY Weather in this Method. The Orbit Pothole Count is expected to remain the same
        [TestMethod]
        public void TestOrbitPotholeRecalculationForWindy()
        {
            //ARRANGE
            int _potholeCount = 20; //This is our expected value. We are using Orbit1 for this test that has a pothole count of 20. When the weather is Windy, the pothole count should remain the same

            //ACT
            orbit1.RequestPotholeAssessment(weather3.GetPotholeFactor());   //weather3 is WINDY. We pass in the pothole factor of this weather to orbit1 and the orbit is supposed to internally re-calculate its pothole count

            //ASSERT
            int result = orbit1.GetOrbitPotholes();
            Assert.AreEqual(_potholeCount, result);
        }
    }
}
