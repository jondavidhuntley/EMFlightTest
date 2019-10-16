namespace SiGroup.Taa.PlaneSimulator
{
    public interface IFlightMonitor
    {
        /// <summary>
        /// Distance to Travel
        /// </summary>
        decimal DistanceToTravelMiles { get; set; }
        
        /// <summary>
        /// Fuel Consumptiom
        /// </summary>
        decimal FuelConsumption { get; set; }
        
        /// <summary>
        /// Current Fuel Level in Gallons
        /// </summary>
        decimal FuelLevel { get; set; }

        /// <summary>
        /// Range in miles 
        /// </summary>
        decimal Range { get; set; }

        /// <summary>
        ///  Passenger Total
        /// </summary>
        int PassengerTotal { get; set; }

        /// <summary>
        /// Indicated whether Plane Should Take Off
        /// </summary>
        bool PlaneCanTakeOff { get; }
        
        /// <summary>
        /// Additional Range in Addition to Trip Distance
        /// </summary>
        decimal AdditionalRange { get; }

        /// <summary>
        /// Indicates if we have enough fuel to make Trip Distance
        /// </summary>
        bool EnoughFuel { get; }

        /// <summary>
        /// Emergency Landing Indicator
        /// </summary>
        bool MakeEmergencyLanding { get; set; }
        
        /// <summary>
        /// Prepares for Take Off
        /// </summary>
        void PrepareForTakeOff();

        /// <summary>
        /// Set Inflight Conditions
        /// </summary>
        /// <param name="fuel">Current Fuel Level</param>
        /// <param name="fuelConsumption">Current Consumption</param>
        /// <param name="distanceToTravel">Distance to Tavel</param>
        void SetInFlightConditions(decimal fuel, decimal fuelConsumption, decimal distanceToTravel);

        /// <summary>
        /// Adjusts Fuel Consumption
        /// </summary>
        /// <param name="fuelConsumptionChangePercent">Adjustment in Percent</param>
        void SetFuelConsumptionIncrease(decimal fuelConsumptionChangePercent);

        /// <summary>
        /// Log Additional Range Request
        /// </summary>
        /// <param name="extraMiles"></param>
        void LogAdditionalRangeRequest(decimal extraMiles);
    }
}