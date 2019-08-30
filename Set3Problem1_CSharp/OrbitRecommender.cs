namespace Set3Problem1_CSharp
{
    public sealed class OrbitRecommender
    {
        private readonly string _vehicleName;
        private readonly string _orbitName;
        private readonly int _minutesForVehicle;

        public OrbitRecommender(string vehicleName, string orbitName, int minutesForVehicle)
        {
            _vehicleName = vehicleName;
            _orbitName = orbitName;
            _minutesForVehicle = minutesForVehicle;
        }

        #region Data Readers

        public string GetFastestVehicleName() => _vehicleName;
        public string GetOrbitName() => _orbitName;
        public int GetVehicleMinutes() => _minutesForVehicle;

        #endregion
    }
}
