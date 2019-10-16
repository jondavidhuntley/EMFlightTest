Feature: Flights
	In order to avoid silly mistakes
	As a novice co-pilot
	I want to take off with enough fuel to from Belgrade to London

@mytag
Scenario: Travel from Belgrade to London
	Given I have 500 gallons of fuel into the tank
	And I have 1250 miles to travel
	And My Fuel Consumption is 2 mpg
	When I Plot my route
	Then The Flight System will advise me not to take off 
