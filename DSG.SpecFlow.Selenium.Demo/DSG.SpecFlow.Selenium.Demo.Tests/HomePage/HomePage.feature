Feature: HomePage
	In order to...
	As a...
	I want to...

Scenario: The home page should have a welcome carousel
	Given I go to the home page
	Then it should have a welcome carousel
	And the carousel should have a "Learn More" button

Scenario: The home page should have an About link
	Given I go to the home page
	When I click on the About link
	Then I should be taken to the About page

Scenario: The home page should have an Contact link
	Given I go to the home page
	When I click on the Contact link
	Then I should be taken to the Contact page
