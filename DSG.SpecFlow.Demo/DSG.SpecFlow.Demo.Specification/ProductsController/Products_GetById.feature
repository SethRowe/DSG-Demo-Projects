Feature: Products_GetById
	In order to...
	As a...
	I want to...

Scenario Outline: Getting a product by id
	Given A product id of <id>
	When I call GET api/product/id
	Then the result should have an id of <id>
	And the result should have a name of '<name>'
	And the result should have a description of '<desc>'

	Examples: 
	  | id  | name             | desc                    |
	  | 123 | product name 123 | product description 123 |
	  | 456 | product name 456 | product description 456 |
	  | 789 | product name 789 | product description 789 |

Scenario: Getting an invalid product by id
	Given an invalid product id
	When I call GET api/product/id
	Then the result should be 404 NotFound