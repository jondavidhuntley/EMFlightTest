Feature: Reserve Fuel Advice
	In order to plan ahead for rough weather
	As a nervous pilot
	I want to know if my flight monitor can calculate additional fuel consumption during a storm with a safe 
	margin to travel to another airport

@mytag
Scenario: Fly into a head wind/storm
	Given I have 280 gallons of fuel, 3800 miles to travel and fuel consumption is 18 mpg
	And I have just flown into a storm where my consumption is up by 16%
	When I ask Flight Monitor if I have enough fuel plus a reserve enough to fly an extra 500 miles
	Then I want to Flight Monitor to advise me to Make Emergency Landing
