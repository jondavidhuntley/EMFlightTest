using NUnit.Framework;
using SiGroup.Taa.PlaneSimulator;
using TechTalk.SpecFlow;

namespace SiGroup.Taa.FlightTest.Steps
{
    [Binding]
    public class FlightsSteps
    {
        /// <summary>
        /// Flight Monitor Instance
        /// </summary>
        private FlightMonitor _monitor;
        
        /// <summary>
        /// Arrange - SetFuel Level
        /// </summary>
        /// <param name="fuelLevel">Fuel Level</param>
        [Given(@"I have (.*) gallons of fuel into the tank")]
        public void GivenIHaveGallonsOfFuelIntoTheTank(decimal fuelLevel)
        {
            _monitor = new FlightMonitor();
            _monitor.FuelLevel = fuelLevel;
        }

        /// <summary>
        /// Arrange - Set Distance to travel
        /// </summary>
        /// <param name="miles">Distance in Miles</param>
        [Given(@"I have (.*) miles to travel")]
        public void GivenIHaveMilesToTravel(decimal miles)
        {
            _monitor.DistanceToTravelMiles = miles;
        }
        
        /// <summary>
        /// Arrange - Set Fuel Consumption
        /// </summary>
        /// <param name="consumption">Fuel Consumption miles/gallon</param>
        [Given(@"My Fuel Consumption is (.*) mpg")]
        public void GivenMyFuelConsumptionIsMpg(decimal consumption)
        {
            _monitor.FuelConsumption = consumption;           
        }               

        /// <summary>
        /// Act - Prepare for Take Off / Plot Route
        /// </summary>
        [When(@"I Plot my route")]
        public void WhenIPlotMyRoute()
        {
            _monitor.PrepareForTakeOff();
        }

        /// <summary>
        /// Assert - The Flight Monitor will advise me not to Take Off 
        /// </summary>
        [Then(@"The Flight System will advise me not to take off")]
        public void ThenTheFlightSystemWillAdviseMeNotToTakeOff()
        {
            // Shows Message on Test Failure
            Assert.False(_monitor.PlaneCanTakeOff, $"DEATH IS HIGHLY LIKELY! Plane takes off without enough fuel! Journey distance is {_monitor.DistanceToTravelMiles} miles but total flight range is only {_monitor.Range} miles!");

            // Show Pass Message
            Assert.Pass($"FLIGHT ADVICE IS CORRECT Not Enough Fuel for journey, journey distance is {_monitor.DistanceToTravelMiles} miles but total flight range is only {_monitor.Range} miles!");
        }        
    }
}