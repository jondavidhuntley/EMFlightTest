using NUnit.Framework;
using SiGroup.Taa.PlaneSimulator;
using TechTalk.SpecFlow;

namespace SiGroup.Taa.FlightTest.Steps
{
    [Binding]
    public class StormReserveSteps
    {
        /// <summary>
        /// Flight Monitor Instance
        /// </summary>
        private FlightMonitor _monitor;
        private decimal _requestedExtraMiles = 0;

        /// <summary>
        /// Arrange - Set Flight Conditions
        /// </summary>
        /// <param name="fuel">Fuel Level</param>
        /// <param name="miles">Miles to Travel</param>
        /// <param name="consumption">Fuel Consumption</param>
        [Given(@"I have (.*) gallons of fuel, (.*) miles to travel and fuel consumption is (.*) mpg")]
        public void GivenIHaveGallonsOfFuelMilesToTravelAndFuelConsumptionIsMpg(decimal fuel, decimal miles, decimal consumption)
        {
            _monitor = new FlightMonitor();
            _monitor.SetInFlightConditions(fuel, consumption, miles);
        }
        
        /// <summary>
        /// Arrange - Set Fuel Consumption Increase
        /// </summary>
        /// <param name="consumptionChange"></param>
        [Given(@"I have just flown into a storm where my consumption is up by (.*)%")]
        public void GivenIHaveJustFlownIntoAStormWhereMyConsumptionIsUpBy(decimal consumptionChange)
        {
            _monitor.SetFuelConsumptionIncrease(consumptionChange);
        }
                      
        /// <summary>
        /// Act - Test whether we have enough Fuel to make the trip with some extra range
        /// </summary>
        /// <param name="extraMiles"></param>
        [When(@"I ask Flight Monitor if I have enough fuel plus a reserve enough to fly an extra (.*) miles")]
        public void WhenIAskFlightMonitorIfIHaveEnoughFuelPlusAReserveEnoughToFlyAnExtraMiles(decimal extraMiles)
        {
            _monitor.LogAdditionalRangeRequest(extraMiles);
            _requestedExtraMiles = extraMiles;
        }

        /// <summary>
        /// Assert - Make Assertions
        /// </summary>
        [Then(@"I want to Flight Monitor to advise me to Make Emergency Landing")]
        public void ThenIWantToFlightMonitorToAdviseMeToMakeEmergencyLanding()
        {
            // Assert we need to make emergency landing
            Assert.True(_monitor.MakeEmergencyLanding);

            // Show Pass Message
            Assert.Pass($"FLIGHT MONITOR ADVICES EMERGENCY LANDING... Not Enough range! Requested additional range: {_requestedExtraMiles} Actual: {_monitor.AdditionalRange} miles");
        }   
    }
}