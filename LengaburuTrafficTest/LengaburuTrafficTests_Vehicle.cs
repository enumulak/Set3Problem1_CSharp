using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Set3Problem1_CSharp;

namespace LengaburuTrafficTest
{
    [TestClass]
    public class LengaburuTrafficTests_Vehicle
    {
        //Required Data that will be used by this Test Class

        Orbit orbit1 = new Orbit("Orbit1", 18, 20);
        Orbit orbit2 = new Orbit("Orbit2", 20, 10);

        Vehicle vehicle1 = new Vehicle("Bike", 10, 2, new List<WeatherType> { WeatherType.sunny, WeatherType.windy });
        Vehicle vehicle2 = new Vehicle("Tuk Tuk", 12, 1, new List<WeatherType> { WeatherType.sunny, WeatherType.rainy });
        Vehicle vehicle3 = new Vehicle("Car", 20, 3, new List<WeatherType> { WeatherType.sunny, WeatherType.rainy, WeatherType.windy });

        Weather weather1 = new Weather(WeatherType.rainy, 0.2);
        Weather weather2 = new Weather(WeatherType.sunny, -0.1);
        Weather weather3 = new Weather(WeatherType.windy, 0.0);

        //This Method tests whether a Vehicle is able to set its Weather Usage flag to True if it can be used in the Current Weather
        [TestMethod]
        public void TestVehicleUsageRequestForCurrentWeather()
        {
            //Arrange
            var flag = true;    //This is our expected value. If a vehicle can be used in the Current Weather, then its internal flag will be set to True

            //Act

            //We set the Current Weather as Windy
            var currentWeather = WeatherType.windy.ToString();

            //We select Vehicle3 as it can be used in the Windy Weather
            vehicle3.RequestUsageForCurrentWeather(true);

            //Assert
            Assert.AreEqual(flag, vehicle3.CanBeUsedInCurrentWeather());
        }

        //This Method tests if a Vehicle is internally able to set the Orbit Traffic Speed as its Restricted Speed, based on the following Condition:
        //IF ORBIT TRAFFIC SPEED IS LESS THAN VEHICLE MAX SPEED - WE USE A VEHICLE THAT HAS HIGEST MAX SPEED AS AN EXAMPLE FOR THIS TEST
        [TestMethod]
        public void TestVehicleRestrictedSpeedRequest1()
        {
            //Arrange
            int _restrictedSpeed = 14;  //This is our expected Value - When 14 is set as Orbit Traffic Speed and Vehicle Restriction is requested, the same value gets set as Restricted Speed of Vehicle

            //Act

            orbit1.RequestTrafficSpeedSetting(14);

            //For the purposes of this Test, we request Vehicle3 to be used in Current Weather
            vehicle3.RequestUsageForCurrentWeather(true);

            vehicle3.RequestSpeedAndMinutesSettingForOrbit(orbit1);

            //Arrange
            int rSpeed = vehicle3.GetVehicleRestrictedSpeed();
            Assert.AreEqual(_restrictedSpeed, rSpeed);
        }

        //This Method tests if a Vehicle is Internally able to set its own Max Speed as the Restricted Speed, based on the following condition:
        //IF ORBIT TRAFFIC SPEED IS GREATER THAN THE MAX SPEED OF THE VEHICLE - WE USE A VEHICLE THAT HAS A LOW MAX SPEED AND SPECIFY A LARGER TRAFFIC SPEED FOR ORBIT
        [TestMethod]
        public void TestVehicleRestrictedSpeedRequest2()
        {
            //Arrange
            int _restrictedSpeed = 10;  //This is our expected value. We are choosing a Vehicle that has Max Speed of 10 and specifying an Orbit Traffic Speed of 14

            //Act
            orbit1.RequestTrafficSpeedSetting(14);

            vehicle1.RequestUsageForCurrentWeather(true);

            vehicle1.RequestSpeedAndMinutesSettingForOrbit(orbit1);

            //Assert
            int rSpeed = vehicle1.GetVehicleRestrictedSpeed();
            Assert.AreEqual(_restrictedSpeed, rSpeed);
        }

        //This Method tests if a Vehicle is able to calculate the minutes it will take to Traverse a given Orbit, provided that the vehicle can be used in Current Weather and speed restrictions are set
        //Input Parameters - Orbit1, Bike, SUNNY Weather
        [TestMethod]
        public void TestVehicleMinutesToTraverseOrbit()
        {
            //ARRANGE
            int _minutesToTraverseOrbit = 80;   //This is our expected value. We are simulating a scenario where the minutes it will take a specific vehicle to traverse an Orbit is 80, the Weather being Sunny

            //ACT

            //We first set the traffic speed of an Orbit - we have taken Orbit1 for this test
            orbit1.RequestTrafficSpeedSetting(12);

            //Then Vehicle is requested for usage in Current Weather
            vehicle1.RequestUsageForCurrentWeather(true);

            //Then Orbit1 is requested to re-calculate its pothole count. POthole factor of SUNNY is provided
            orbit1.RequestPotholeAssessment(weather2.GetPotholeFactor());

            //Vehicle1 is then requested to set speed restrictions and calculate time taken for Orbit1 traversal
            vehicle1.RequestSpeedAndMinutesSettingForOrbit(orbit1);

            //ASSERT
            int minutes = vehicle1.GetMinutesToTraverseOrbit();
            Assert.AreEqual(_minutesToTraverseOrbit, minutes);
        }

    }
}
