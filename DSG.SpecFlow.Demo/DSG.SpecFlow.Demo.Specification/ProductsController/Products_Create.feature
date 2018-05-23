Feature: Products_Create
	In order to...
	As a...
	I want to...

Scenario: Saving a new, valid product
	Given a new and valid product
	When I call POST api/product
	Then the result should be 201 Created
	And the result should contain the new id

Scenario: Saving a new, invalid product
	Given a product missing a name
	When I call POST api/product
	Then the result should be 400 BadRequest
	And the result should contain a validation message