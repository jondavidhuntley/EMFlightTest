namespace SiGroup.Taa.PlaneSimulator
{
    public class FlightMonitor : IFlightMonitor
    {       
        /// <summary>
        /// Gets or Sets Distance To Travel in Miles
        /// </summary>
        public decimal DistanceToTravelMiles { get; set; }

        /// <summary>
        /// Gets or Sets Fuel Level
        /// </summary>
        public decimal FuelLevel { get; set; }

        /// <summary>
        /// Gets or Sets Fuel Consumption
        /// </summary>
        public decimal FuelConsumption { get; set; }

        /// <summary>
        /// Gets or Sets Range
        /// </summary>
        public decimal Range { get; set; }

        /// <summary>
        /// Gets or Sets Total Number of Passengers
        /// </summary>
        public int PassengerTotal { get; set; }

        /// <summary>
        /// Gets whether we will Take Off
        /// Only if enough Fuel
        /// </summary>
        public bool PlaneCanTakeOff
        {
            get
            {
                return EnoughFuel;               
                //return true;
            }
        }
             
        /// <summary>
        /// Gets whether we have enough fuel for the Journey
        /// </summary>
        public bool EnoughFuel
        {
            get
            {
                if (Range > DistanceToTravelMiles)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Additional Range -> Total Range - Distance left to Travel
        /// </summary>
        public decimal AdditionalRange
        {
            get
            {
                if (Range > DistanceToTravelMiles)
                {
                    return Range - DistanceToTravelMiles;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Gets or sets whether plane should make emergency landing
        /// </summary>
        public bool MakeEmergencyLanding { get; set; }
        
        /// <summary>
        /// Creates new instance of Flight Monitor
        /// </summary>
        public FlightMonitor()
        {
        }

        /// <summary>
        /// Prepare for Take Off
        /// </summary>
        public void PrepareForTakeOff()
        {           
            Range = CalculateRange(FuelLevel, FuelConsumption);
        }
        
        /// <summary>
        /// Set Fuel Consumption Change
        /// </summary>
        /// <param name="fuelConsumptionChangePercent">Change in Fuel Consumption</param>
        public void SetFuelConsumptionIncrease(decimal fuelConsumptionChangePercent)
        {            
            var newConsumption = GetPercentageReduction(FuelConsumption, fuelConsumptionChangePercent);

            Range = CalculateRange(FuelLevel, newConsumption);
            FuelConsumption = newConsumption;
        }

        /// <summary>
        /// Set In Flight Conditions
        /// </summary>
        /// <param name="fuel">Fuel Remaining</param>
        /// <param name="fuelConsumption">Consumption</param>
        /// <param name="distanceToTravel">Distance Left to Travel</param>
        public void SetInFlightConditions(decimal fuel, decimal fuelConsumption, decimal distanceToTravel)
        {
            FuelLevel = fuel;
            FuelConsumption = fuelConsumption;
            DistanceToTravelMiles = distanceToTravel;

            Range = CalculateRange(fuel, fuelConsumption);
        }

        /// <summary>
        /// Log Additional Range Request
        /// </summary>
        /// <param name="extraMiles">Additional Miles (maybe to make it to another airport)</param>
        public void LogAdditionalRangeRequest(decimal extraMiles)
        {
            if (AdditionalRange < extraMiles)
            {
                MakeEmergencyLanding = true;
            }
        }

        /// <summary>
        /// Calculate Range (Miles) - Gallons * Miles/Gallon
        /// </summary>
        /// <param name="fuelLevel">Fuel Level</param>
        /// <param name="consumption">Consumption</param>
        /// <returns>Range Miles</returns>
        private decimal CalculateRange(decimal fuelLevel, decimal consumption)
        {
            decimal range = (fuelLevel * consumption);
            decimal newConsumptionRate = 0;

            if (PassengerTotal > 0)
            {
                var consumptionIncreaseByPassenger = ((decimal)PassengerTotal * (decimal)0.45);

                // Apply Consumption Increase as %
                newConsumptionRate = GetPercentageReduction(consumption, consumptionIncreaseByPassenger);

                // Recalculate Range
                range = fuelLevel * newConsumptionRate;

                // Update Fuel consumption
                FuelConsumption = newConsumptionRate;
            }           

            return range;
        }

        /// <summary>
        /// Calculate % Reduction
        /// </summary>
        /// <param name="currentValue">Current Value</param>
        /// <param name="percent">Number of PErcent</param>
        /// <returns>Adjusted Value</returns>
        private decimal GetPercentageReduction(decimal currentValue, decimal percent)
        {
            return (currentValue - (currentValue * (percent / (decimal)100)));
        }
    }
}